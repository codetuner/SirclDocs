using Microsoft.AspNetCore.Mvc;
using SirclDocs.Website.Areas.MvcDashboardIdentity.Models.Home;
using SirclDocs.Website.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Areas.MvcDashboardIdentity.Controllers
{
    public class HomeController : BaseController
    {
        #region Construction

        private readonly ApplicationDbContext context;

        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TopMenu()
        {
            var model = new TopMenuModel();
            model.UserCount = context.Users.Count();
            model.RoleCount = context.Roles.Count();
            return View(model);
        }

        [HttpGet]
        public IActionResult GetStarted()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MvcDashboardsDropdown()
        {
            var model = new List<string>();
            foreach (var manifest in System.IO.Directory.EnumerateFiles(Path.Combine(AppContext.BaseDirectory, @"..\..\..\Areas")).Select(s => new FileInfo(s)))
            {
                if (manifest.Name.StartsWith("MvcDashboard.") && manifest.Name.EndsWith(".manifest"))
                {
                    model.Add(manifest.Name.Replace(".manifest", "").Replace(".", ""));
                }
            }

            return View(model);
        }
    }
}
