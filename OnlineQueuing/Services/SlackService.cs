using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineQueuing.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{

    public class SlackService : ISlackService
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient httpClient;

        public SlackService(IConfiguration config, HttpClient hc)
        {
            this.configuration = config;
            this.httpClient = hc;
        }

        public async Task<EmailLookupResponse> EmailLookup(string email)
        {
            //Create an email user lookup request:
            var emailLookupRequest = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/users.lookupByEmail");

            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("email", email));

            emailLookupRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration["SlackOAuthAccessToken"]);
            emailLookupRequest.Content = new FormUrlEncodedContent(list);
            var response = await httpClient.SendAsync(emailLookupRequest);

            EmailLookupResponse responseObject = new EmailLookupResponse();

            //Deserialize the response (first create a string from the request's content, 
            //then deserialize it.
            responseObject = JsonConvert.DeserializeObject<EmailLookupResponse>(await response.Content.ReadAsStringAsync());
            return responseObject;
        }

        public async Task SendSlackMessage(string email, string messageToSend)
        {
            var responseObject = await EmailLookup(email);
            //Create the post message request:
            var postMessageRequest = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/chat.postMessage");
            postMessageRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration["SlackBotToken"]);
            var messageRequestBody = new List<KeyValuePair<string, string>>();
            messageRequestBody.Add(new KeyValuePair<string, string>("channel", responseObject.user.id));
            messageRequestBody.Add(new KeyValuePair<string, string>("text", messageToSend));

            postMessageRequest.Content = new FormUrlEncodedContent(messageRequestBody);

            await httpClient.SendAsync(postMessageRequest);
        }

        public async Task CreateSlackReminder(string email, string notificationBody, int timeSlot, string day)
        {
            int timeStamp = CreateDateTimeForReminder(day, timeSlot);

            var responseObject = await EmailLookup(email);

            var reminderRequest = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/reminders.add");

            reminderRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration["SlackOAuthAccessToken"]);

            var reminderRequestBody = new List<KeyValuePair<string, string>>();
            reminderRequestBody.Add(new KeyValuePair<string, string>("text", notificationBody));
            reminderRequestBody.Add(new KeyValuePair<string, string>("time", timeStamp.ToString()));
            reminderRequestBody.Add(new KeyValuePair<string, string>("user", responseObject.user.id));

            reminderRequest.Content = new FormUrlEncodedContent(reminderRequestBody);

            await httpClient.SendAsync(reminderRequest);
        }

        public int CreateDateTimeForReminder(string date, int timeSlot)
        {
            string hour = string.Empty;
            switch (timeSlot)
            {
                case 1:
                    hour = date + " 10:00:00,000";
                    break;
                case 2:
                    hour = date + " 11:00:00,000";
                    break;
                case 3:
                    hour = date + " 12:00:00,000";
                    break;
                case 4:
                    hour = date + " 13:00:00,000";
                    break;
                case 5:
                    hour = date + " 14:00:00,000";
                    break;
                case 6:
                    hour = date + " 15:00:00,000";
                    break;
                case 7:
                    hour = date + " 16:00:00,000";
                    break;
                case 8:
                    hour = date + " 17:00:00,000";
                    break;
            }
            DateTime dateTime = DateTime.ParseExact(hour, "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime earlierDateTime = dateTime.Add(new TimeSpan(-2, -30, 0));
            Int32 output = (Int32)(earlierDateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            return output;
        }
    }
}