using AlphaCinemaServices.Exceptions;
using AlphaCinemaWeb.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Utilities.Middleware
{
    public class EntityNotFoundMiddleware
    {
        private readonly RequestDelegate next;

        public EntityNotFoundMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);

                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/404");
                }
            }
            catch (EntityDoesntExistException ex)
            {
                context.Response.Redirect("/404");
            }
            catch (InvalidClientInputException ex)
            {
                context.Response.Redirect("/404");
            }
        }
    }
}
