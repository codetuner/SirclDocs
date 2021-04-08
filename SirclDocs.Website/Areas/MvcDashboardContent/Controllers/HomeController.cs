using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirclDocs.Website.Data;
using SirclDocs.Website.Data.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Areas.MvcDashboardContent.Controllers
{
    [Authorize(Roles = "Administrator,ContentAdministrator,ContentEditor,ContentAuthor")]
    public class HomeController : BaseController
    {
        #region Construction

        private readonly ContentDbContext context;

        public HomeController(ContentDbContext context)
        {
            this.context = context;
        }

        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetStarted()
        {
            return View();
        }
    }
}
