using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static OnlineQueuing.Helpers.CustomException;

namespace OnlineQueuing.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            CustomErrorMessage errorMessage = new CustomErrorMessage();
            int statusCode = 0;
            if (exception is MissingClaimsException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorMessage.Error = "Missing user data, something went wrong during authentication.";
            }
            else if (exception is MissingUsernameException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorMessage.Error = "Missing username.";
            }
            else if (exception is MissingInformationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorMessage.Error = "User cannot be saved into the database, username or email is missing.";
            }
            else if (exception is UserNotExistException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorMessage.Error = "User does not exist in the database.";
            }
            else if (exception is TokenGenerationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorMessage.Error = "Token generation for user failed.";
            }
            else if (exception is MissingUserEmailException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                errorMessage.Error = "Missing email.";
            }
            else
            {
                throw exception;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorMessage));
        }
    }
}
