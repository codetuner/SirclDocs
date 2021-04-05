using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Areas.MvcDashboardIdentity.Controllers
{
    [Area("MvcDashboardIdentity")]
    [Authorize(Roles = "Administrator,IdentityAdministrator")]
    public abstract class BaseController : Controller
    {
        protected IActionResult Back(bool allowCaching = true)
        {
            Response.Headers["X-Sircl-History"] = (allowCaching) ? "back" : "back-uncached";
            return this.StatusCode(204);
        }

        protected IActionResult Forward(string url)
        {
            Response.Headers["Location"] = url;
            return this.StatusCode(204);
        }

        protected IActionResult ForwardToAction(string action, string controller = null, object values = null)
        {
            return this.Forward(Url.Action(action, controller, values));
        }

        protected IActionResult DialogClose()
        {
            return this.StatusCode(204);
        }

        protected IActionResult DialogOk()
        {
            Response.Headers["X-Sircl-History"] = "refresh";
            return this.StatusCode(204);
        }
    }
}
