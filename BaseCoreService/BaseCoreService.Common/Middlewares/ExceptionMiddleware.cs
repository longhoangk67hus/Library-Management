using BaseCoreService.Entities.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BaseCoreService.Entities.Enums;
using NLog;
using Microsoft.Extensions.Configuration;

namespace BaseCoreService.Common.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NLog.ILogger _logger;
        private readonly IConfiguration _configuration;
        public ExceptionMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.Error(new LogEventInfo()
            {
                Properties = { { "AppCode", _configuration["AppSettings:AppCode"] } },
                Message = exception.Message,
                Exception = exception
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ServiceResponse();
            errorResponse.OnError(new() { ErrorCode = 99, Data = ErrorCode.ServerException, ErrorMessage = "Catch exception!" });

            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }

}
