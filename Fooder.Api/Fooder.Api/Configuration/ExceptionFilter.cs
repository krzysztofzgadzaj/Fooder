using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Fooder.Api.Configuration
{
    internal sealed class ExceptionFilter : ExceptionFilterAttribute
    {
        private const string DefaultExceptionMessage = "Wystąpił nieprzewidziany błąd serwera.";
        private const string ResponseContentType = "application/json";

        public override void OnException(ExceptionContext context)
        {
            var response = context.HttpContext.Response;
            HandleResponse(
                CreateExceptionMessage(context.Exception.Message),
                response,
                HttpStatusCode.InternalServerError);
        }

        private static void HandleResponse(string errorMessage, HttpResponse response, HttpStatusCode statusCode)
        {
            const bool isError = true;
            var result = JsonConvert.SerializeObject(
                new
                {
                    message = errorMessage,
                    errorCode = (int)statusCode,
                    isError,
                });

            response.StatusCode = (int)statusCode;
            response.ContentType = ResponseContentType;
            response.WriteAsync(result);
        }

        private static string CreateExceptionMessage(string exceptionMessage) =>
            $"{DefaultExceptionMessage} {exceptionMessage}";
    }
}
