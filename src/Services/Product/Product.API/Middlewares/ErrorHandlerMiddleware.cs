using Product.Application.Exceptions;
using Product.Application.Wrappers; // Bizim standart Response<T> üçün
using Product.Application.Wrappers.Base;
using System.Net;
using System.Text.Json;

namespace Product.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

                // Standart Response<string> formatında bir cavab hazırlayırıq.
                var responseModel = new Response<string>(error.Message);

                switch (error)
                {
                    case ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        // Xətaları xüsusi formatda doldururuq.
                        responseModel.Errors = e.Errors.Values.SelectMany(v => v).ToList();
                        break;

                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        // Gözlənilməyən digər xətalar
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        // Production mühitində error.Message yerinə ümumi bir mesaj göstərmək daha təhlükəsizdir.
                        // responseModel.Message = "An unexpected error occurred."; 
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel, new JsonSerializerOptions
                {
                    // JSON cavabını daha oxunaqlı etmək üçün
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await response.WriteAsync(result);
            }
        }
    }
}