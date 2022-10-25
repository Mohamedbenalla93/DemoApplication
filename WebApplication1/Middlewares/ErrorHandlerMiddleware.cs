using DemoAppliaction.Core.Application.Exceptions;
using DemoAppliaction.Core.Application.Wrappers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

                switch (error)
                {
                    case NotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case DemoAppliaction.Core.Application.Exceptions.ApiException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case ValidationException e:
                        {
                            // custom application error
                            var validationResponse = new ValidationErrorResponse { Succeeded = false, Message = error.Message };
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            validationResponse.Data = e.Errors;
                            var validationResult = JsonSerializer.Serialize(validationResponse);
                            await response.WriteAsync(validationResult);
                            return;
                        }
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        //_logger.Error(error, $"Message : {0}{Environment.NewLine}Inner Message : {1}{Environment.NewLine}", 
                        //    error.Message, error.InnerException?.Message);
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
