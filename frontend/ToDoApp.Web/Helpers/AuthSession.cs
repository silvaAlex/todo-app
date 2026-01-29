using System;
using System.Web;

namespace ToDoApp.Web.Helpers
{
    public static class AuthSession
    {
        private const string TOKEN_KEY = "JWT_TOKEN";
        private const string COOKIE_NAME = "AUTH_SESSION_ID";

        public static void SetToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentException("Token não pode ser nulo ou vazio");

            var sessionId = GetOrCreateSessionId();

            HttpContext.Current.Cache.Insert(
                GetCacheKey(sessionId),
                token,
                null,
                DateTime.Now.AddMinutes(30),
                System.Web.Caching.Cache.NoSlidingExpiration);
                
        }

        private static string GetCacheKey(string sessionId)
        {
            return $"{TOKEN_KEY}_{sessionId}";
        }

        private static string GetOrCreateSessionId()
        {
            var sessionId = GetSessionIdFromCookie();
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = Guid.NewGuid().ToString();
                CreateCookie(sessionId);
            }

            return sessionId;
        }

        private static void CreateCookie(string sessionId)
        {
            var cookie = new HttpCookie(COOKIE_NAME, sessionId)
            {
                HttpOnly = true,
                Secure = HttpContext.Current.Request.IsSecureConnection,
                Expires = DateTime.Now.AddMinutes(30)
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private static string GetSessionIdFromCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies[COOKIE_NAME];
            return cookie?.Value;
        }

        public static string GetToken()
        {
            var sessionId = GetSessionIdFromCookie();

            if (string.IsNullOrEmpty(sessionId))
                return null;

            var token = HttpContext.Current.Cache[GetCacheKey(sessionId)] as string;

            return token;
        }

        public static bool IsAuthenticated()
        {
            return !string.IsNullOrEmpty(GetToken());
        }

        public static void Logout()
        {
            var sessionId = GetSessionIdFromCookie();
            if (!string.IsNullOrEmpty(sessionId))
            {
                HttpContext.Current.Cache.Remove(GetCacheKey(sessionId));
                RemoveCookie();
            }
        }

        private static void RemoveCookie()
        {
            var cookie = new HttpCookie(COOKIE_NAME)
            {
                Expires = DateTime.Now.AddDays(-1)
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}