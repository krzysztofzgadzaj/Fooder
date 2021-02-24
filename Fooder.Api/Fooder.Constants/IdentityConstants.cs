namespace Fooder.Constants
{
    public static class IdentityConstants
    {
        public const string AuthenticationRoute = "authentication";
        public const string AuthorizationRoute = "authorization?permissionCode={0}";
        public const string CreatePrincipalRoute = "principal";
        public const string GetUserRoutePattern = "principal/{0}";

        public const string AuthorizationHeaderKey = "Authorization";
        public const string ScopeName = "Fooder";
    }
}
