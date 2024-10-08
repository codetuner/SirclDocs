﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SirclDocs.Website.Data;
using SirclDocs.Website.Data.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SirclDocs.Website.Models.Sitemap;

namespace SirclDocs.Website.Controllers
{
    public class SitemapController : Controller
    {
        #region Construction

        private readonly ContentDbContext context;

        public SitemapController(ContentDbContext context)
        {
            this.context = context;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            var model = new IndexModel("https://www.getsircl.com");

            var pages = context.ContentPublishedDocuments
                .AsNoTracking()
                .Include(d => d.Document)
                .Where(d => d.Path != null && d.DocumentTypeName.EndsWith("Page"))
                .OrderBy(d => d.Path).ThenBy(d => d.Id);

            var securedPaths = await context.ContentSecuredPaths.ToListAsync();

            foreach (var page in pages)
            {
                if (securedPaths.Any(sp => page.Path.StartsWith(sp.Path))) continue;

                model.Urls.Add(new SitemapUrl(page.Path, page.Document.LastPublishedOnUtc));
            }

            return View(model);
        }
    }
}
