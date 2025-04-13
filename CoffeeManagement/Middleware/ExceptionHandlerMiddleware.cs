using CoffeeManagement.Exceptions;
using CoffeeManagement.MetaData;
using System.ComponentModel.DataAnnotations;
using static CoffeeManagement.Exceptions.ApiException;
using ValidationException = CoffeeManagement.Exceptions.ApiException.ValidationException;


namespace CoffeeManagement.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _requestDelegate;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate requestDelegate, IHostEnvironment environment)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
            _environment = environment;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context); // call the next context if no error
            }
            catch (Exception exception)
            {
                var errorId = Guid.NewGuid().ToString();
                _logger.LogError(errorId, context, exception);
                // handleException
                await HandleExceptionAsync(context, errorId, exception);
            }
        }


        private void LogErrors(string errorId, HttpContext context, Exception exception)
        {
            var error = new
            {
                ErrorId = errorId,
                TimeSpan = DateTime.Now,
                RequestPanth = context.Request.Path,
                RequestMethod = context.Request.Method,
                ExceptionType = exception.GetType().Name,
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException?.Message,
                user = context.User?.Identity?.Name ?? "Anonymous",
                //AdditionaInfo
                AdditionalInfo = GetAdditionalInfo(exception)
            };
            var logLevel = exception switch
            {
                BussinessException => LogLevel.Warning,
                ValidationException => LogLevel.Warning,
                NotFoundException => LogLevel.Error,
                _ => LogLevel.Error,
            };
            _logger.Log(logLevel, exception,
                 "Error ID: {ErrorId} - Path: {Path} - Method: {Method} - {@error}",
                 errorId,
                 context.Request.Path,
                 context.Request.Method,
                 error
                );
        }

        private object GetAdditionalInfo(Exception exception)
        {
            return exception switch
            {
                ValidationException valEx => new
                {
                    ValidaitonDetail = valEx.Message
                },
                BussinessException busEx => new
                {
                    BusinessRule = busEx.Message
                },
                NotFoundException nofEx => new
                {
                    Entity = nofEx.Message
                },
                BadRequestException badRequestEx => new
                {
                    badRequest = badRequestEx.Message
                },
                _ => new { } // same default
            };
        }

        private async Task HandleExceptionAsync(HttpContext context, string errorId, Exception exception)
        {
            var (statusCode, message, reason) = exception switch
            {
                ApiException apiEx =>((int)apiEx.StatusCode, GetExceptionMessage(apiEx),apiEx.Message),
                InvalidOperationException =>(StatusCodes.Status400BadRequest,"Invalid Operation",exception.Message),
                _=>(StatusCodes.Status500InternalServerError ,"Internal Server Error",
                _environment.IsDevelopment()?exception.Message: "An unexpected error occurred")
            };
            var errorResponse = ApiResponseBuilder.BuildErrorsResponse(data: new
            {
                ErrorId = errorId,
                TimeSpan = DateTime.UtcNow,
                Details = GetAdditionalInfo(exception)
            },
            statusCode: statusCode,
            message: message,
            reason: reason
            );
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }

        private string GetExceptionMessage(ApiException exception) => exception switch
        {
            ValidationException => "Validation Error",
            NotFoundException => "Resource Not Found",
            BussinessException => "Business Rule Violation",
            BadRequestException => "Bad Request",
            UnauthorizedException => "Unauthorized Access",// no login or token are not access
            ForbiddenException => "Forbidden ",// have login but no permissions
            _ => "Api error"
        }; 
    }
}