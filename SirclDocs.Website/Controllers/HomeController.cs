using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SirclDocs.Website.Data.Content;
using SirclDocs.Website.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SirclDocs.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Migrate([FromServices] ContentDbContext contentContext, CancellationToken ct)
        { 
            foreach(var doc in contentContext.ContentDocuments.Where(d => d.LastPublishedOnUtc != null))
            {
                await doc.PublishAsync(contentContext, null, false, ct);
            }

            await contentContext.SaveChangesAsync(ct);

            return RedirectToAction("Index");
        }
    }
}
