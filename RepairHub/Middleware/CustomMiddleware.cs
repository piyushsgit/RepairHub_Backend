using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Model.UsersModels;
using RepairHub.Middleware;
using Services.User;
 

namespace RepairHub.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
       // private readonly IUserService userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="memoryCache"></param>
        public CustomMiddleware(RequestDelegate next,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _next = next;
            _hostingEnvironment = hostingEnvironment;
           // userService = _userService;
        }

        /// <summary>
        /// Invoke on every request response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="settingService"></param>
        public async Task InvokeAsync(HttpContext context, IUserService userService)
        {
            try
            {
                // Delete files from folder for logs of request and response older than 7 days. 
                // Check JWT Token validity expiry
                var authorizationHeaderValues = context.Request.Headers.TryGetValue(HeaderNames.Authorization, out var values)
     ? values.FirstOrDefault()
     : null;

                string jwtToken = authorizationHeaderValues != null
                    ? authorizationHeaderValues.Replace(JwtBearerDefaults.AuthenticationScheme, "")
                    : null;

                if (!string.IsNullOrEmpty(jwtToken))
                {
                    TokenModel tokenModel = userService.GetUserTokenData(jwtToken.Trim());

                    if (tokenModel != null)
                    {
                        if (tokenModel.ValidTo < DateTime.UtcNow.AddMinutes(1))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return;
                        }
                    }
                }


                await _next(context);
            }
            catch (Exception ex)
            {
                // Add error logs in folder
                throw ex;
            }
         }
       
    }

}

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCulture(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomMiddleware>();
    }
}