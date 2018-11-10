using AlphaCinemaWeb.Utilities.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AlphaCinemaWeb.Utilities.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseNotFoundExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<EntityNotFoundMiddleware>();
        }
    }
}
