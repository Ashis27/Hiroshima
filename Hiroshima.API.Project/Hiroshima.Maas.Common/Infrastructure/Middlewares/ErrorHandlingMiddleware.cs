using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Hiroshima.Maas.Common.Infrastructure.Logger;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Hiroshima.Maas.Common.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private static ILoggerManager _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            this.next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                // must be awaited
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // if it's not one of the expected exception, set it to 500
            var code = HttpStatusCode.InternalServerError;

            //TODO:Mapping if (exception is NotFoundExe) code = HttpStatusCode.NotFound;
            if (exception is ArgumentNullException) code = HttpStatusCode.BadRequest;
            else if (exception is HttpRequestException) code = HttpStatusCode.BadRequest;
            else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;

            return WriteExceptionAsync(context, exception, code);
        }

        private static Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;
            _logger.LogError("----------------- Exception Occuired -----------------");
            _logger.LogError("Error Message =" + exception.Message);
            _logger.LogError("Error Code =" + (int)code);
            _logger.LogError("Exception =" + exception.GetType().Name);
            _logger.LogError("API Route Path =" + context.Request.Path);
            _logger.LogError("----------------- ****************** -----------------");
            return response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = new CustomResponseModel
                {
                    Code = (int)code,
                    Message = exception.Message,
                    Exception = exception.GetType().Name,
                    APIRoute = context.Request.Path
                }
            }));
        }
    }
    public class CustomResponseModel
    {

        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Exception { get; set; }
        public string APIRoute { get; set; }
        // other fields

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
