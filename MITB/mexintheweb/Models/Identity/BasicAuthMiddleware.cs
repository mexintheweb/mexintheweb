using mexintheweb.Services;
using System.Net.Http.Headers;
using System.Text;

namespace mexintheweb.Models.Identity
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private ILoginService LoginService { get; }

        public BasicAuthMiddleware(RequestDelegate next, ILoginService loginService)
        {
            _next = next;
            LoginService = loginService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                var token = await LoginService.GetWebtokenByLogin(new LoginModel { Username = username, Password = password });

                // Authorization Header mit Bearer füllen
                context.Request.Headers["Authorization"] = $"Bearer {token}";
            }
            catch
            {
                // nichts tun!
            }

            await _next(context);
        }
    }
}
