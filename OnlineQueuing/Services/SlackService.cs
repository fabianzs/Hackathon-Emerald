using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineQueuing.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateSlackReminder(string email, string notificationBody, string time)
        {
            Int32 unixTimestamp = (Int32)(new DateTime(2019, 5, 24).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            var responseObject = await EmailLookup(email);

            var reminderRequest = new HttpRequestMessage(HttpMethod.Post, "https://slack.com/api/reminders.add");

            reminderRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", configuration["SlackOAuthAccessToken"]);

            var reminderRequestBody = new List<KeyValuePair<string, string>>();
            reminderRequestBody.Add(new KeyValuePair<string, string>("text", notificationBody));
            reminderRequestBody.Add(new KeyValuePair<string, string>("time", unixTimestamp.ToString()));
            reminderRequestBody.Add(new KeyValuePair<string, string>("user", responseObject.user.id));

            reminderRequest.Content = new FormUrlEncodedContent(reminderRequestBody);

            await httpClient.SendAsync(reminderRequest);
        }
    }
}