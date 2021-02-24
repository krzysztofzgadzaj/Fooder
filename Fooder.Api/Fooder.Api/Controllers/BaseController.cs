using Microsoft.AspNetCore.Mvc;

namespace Fooder.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected const string RoutePattern = "api/[controller]";
        protected const string ResourceIdentifierPattern = "{id:int}";
        protected const string CreateEntityPattern = "{0}/{1}";
    }
}
