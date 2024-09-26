using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SirclDocs.Website.Areas.MvcDashboardContent.Models.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SirclDocs.Website.Areas.MvcDashboardContent.Controllers
{
    [Authorize(Roles = "Administrator,ContentAdministrator,ContentEditor,ContentAuthor")]
    public class MediaController : BaseController
    {
        #region Construction

        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;

        public MediaController(IConfiguration config, IWebHostEnvironment env)
        {
            this.config = config;
            this.env = env;
        }

        #endregion

        #region Index

        [HttpGet]
        public IActionResult Index(IndexModel model)
        {
            return IndexView(model);
        }

        private IActionResult IndexView(IndexModel model)
        {
            var path = Path.Combine(env.WebRootPath, config["Content:Media"]!, model.Path ?? ".");
            if (!path.StartsWith(Path.Combine(env.WebRootPath, config["Content:Media"]!), System.StringComparison.OrdinalIgnoreCase))
                return this.NotFound();
            var directory = new DirectoryInfo(path);
            model.CurrentPathName = directory.Name;
            model.ParentPath = model.Path != null && model.Path.Contains('\\')
                ? model.Path.Substring(0, model.Path.LastIndexOf('\\'))
                : null;
            model.ParentPathName = directory.Parent!.Name;
            model.DisplayPath = config["Content:Media"]! + ((model.Path != null) ? "\\" + model.Path : "");
            model.Directories = directory.GetDirectories();
            model.Files = directory.GetFiles();

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(IndexModel model, List<IFormFile>? files)
        {
            if (files == null)
            {
                this.SetToastrMessage("error", "Failed to upload files. Make sure total file size is not too large.");
                return NoContent();
            }
            else
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var filePath = Path.Combine(env.WebRootPath, config["Content:Media"]!, model.Path ?? ".", formFile.FileName);
                        if (!filePath.StartsWith(Path.Combine(env.WebRootPath, config["Content:Media"]!))) return NotFound();
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
                this.SetToastrMessage("success", "File(s) uploaded.");
            }

            return IndexView(model);
        }

        [HttpGet]
        public IActionResult RenameFile(IndexModel model, string filePath)
        { 
            return View(model);
        }

        [HttpPost]
        public IActionResult RenameFile(IndexModel model, string filePath, string newName)
        { 
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteFile(IndexModel model, string filePath)
        { 
            return View(model);
        }

        [HttpPost, ActionName("DeleteFile")]
        public IActionResult DeleteFilePost(IndexModel model, string filePath)
        { 
            return View(model);
        }

        #endregion
    }
}
