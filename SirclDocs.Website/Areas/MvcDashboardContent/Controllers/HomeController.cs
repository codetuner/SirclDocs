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
