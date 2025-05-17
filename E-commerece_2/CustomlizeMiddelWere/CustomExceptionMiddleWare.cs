using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shareds.ErrorMiddelWare;
using System.Text.Json;

namespace E_commerece_2.CustomlizeMiddelWere
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionMiddleWare> logger;

        public CustomExceptionMiddleWare(RequestDelegate Next,ILogger<CustomExceptionMiddleWare> logger)
        {
            next = Next;
            this.logger = logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
                if(httpContext.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    var Response = new ErrorToReturn()
                    {
                        StatusCode = httpContext.Response.StatusCode,
                        ErrorMessage = $"End Point {httpContext.Request.Path} is not Found" ,

                    };
                    //Return object as JSON
                    var ResponseToReturn = JsonSerializer.Serialize(Response);
                    await httpContext.Response.WriteAsync(ResponseToReturn);
                }
            }
            catch (Exception ex)
            {
                //logger
                logger.LogError(ex, "Something Wrong");
                // set Status Code for Response
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError,
                };
                //set content Type for Response
                httpContext.Response.ContentType = "application/json";
                // Response object
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage=ex.Message,
                };
                //Return object as JSON
                var ResponseToReturn = JsonSerializer.Serialize(Response);
                await httpContext.Response.WriteAsync(ResponseToReturn);
            }
           
        }
    }
}
