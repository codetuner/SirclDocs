﻿using Microsoft.AspNetCore.Mvc;
using SirclDocs.Website.Areas.MvcDashboardIdentity.Models.Home;
using SirclDocs.Website.Data;
using System.Linq;

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
            var model = new TopMenuModel
            {
                UserCount = context.Users.Count(),
                RoleCount = context.Roles.Count()
            };
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult GetStarted()
        {
            return View();
        }
    }
}
