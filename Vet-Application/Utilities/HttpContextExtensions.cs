using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Vet_Application.Utilities
{
    public static class HttpContextExtensions 
    {
        public async static Task AddPaginationHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            double count = await queryable.CountAsync();
            httpContext.Response.Headers.Append("x-total-count", count.ToString());
        }
    }
}
