using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SirclDocs.Website.Data.Samples;
using SirclDocs.Website.Models.Samples;

namespace SirclDocs.Website.Controllers
{
    [EnableCors("SamplesPolicy")]
    public class SamplesController : Controller
    {
        public IActionResult BrownFox()
        {
            return View();
        }

        public IActionResult LoremIpsum()
        {
            return View();
        }

        public IActionResult LizaDonnovan()
        {
            return View();
        }

        public IActionResult PeterJohnson()
        {
            return View();
        }

        public IActionResult TaskList()
        {
            return View("TaskList", new TaskList());
        }

        [HttpPost]
        public IActionResult AddToTaskList(TaskList model, string newTask)
        {
            ModelState.Clear();
            model.Tasks.Add(newTask);
            return View("TaskList", model);
        }

        [HttpPost]
        public IActionResult RemoveFromTaskList(TaskList model, int index)
        {
            ModelState.Clear();
            model.Tasks.RemoveAt(index);
            return View("TaskList", model);
        }

        [HttpGet, HttpPost]
        public IActionResult DependentSelect(string Continent = null)
        {
            var countries = new List<string>();
            switch (Continent)
            {
                case "Europe":
                    countries.Add("Austria");
                    countries.Add("Denmark");
                    countries.Add("Estonia");
                    countries.Add("Finland");
                    break;
                case "Americas":
                    countries.Add("Guatemala");
                    countries.Add("Haiti");
                    break;
                case "Asia":
                    countries.Add("Bangladesh");
                    countries.Add("China");
                    break;
            }

            ViewBag.Continent = Continent;
            return View("DependentSelect", countries);
        }

        [HttpGet, HttpPost]
        public IActionResult Saved(IFormCollection form, string msg = null, bool showdata = false)
        {
            ViewBag.Msg = msg;
            ViewBag.ShowData = showdata;
            ViewBag.Form = form;
            return View("Saved");
        }

        /// <summary>
        /// Auto-Complete companies
        /// </summary>
        public IActionResult AcCompany([FromServices] SamplesDbContext context, string value = "", int take = 12)
        {
            var model = context.Customers.Where(c => c.CompanyName.Contains(value)).Take(take).Select(c => c.CompanyName).Distinct().OrderBy(s => s).ToList();
            if (model.Count == 1 && model[0] == value) model.Clear();
            return View("AcCompany", model);
        }

        /// <summary>
        /// Change actions save change.
        /// </summary>
        public IActionResult SaveChange(string name = null, string value = null, bool? @checked = null)
        {
            return StatusCode(200);
        }

        public IActionResult CarLeaseCalculator([Bind(Prefix = "")] CarLeaseCalculatorModel form)
        {
            if (!String.IsNullOrEmpty(form.Model))
            {
                var price = 0m;
                switch (form.Model)
                {
                    case "A":
                        price = 36_000m;
                        break;
                    case "B":
                        price = 42_000m;
                        break;
                    case "G":
                        price = 48_000m;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unknown car model.");
                }
                if (form.Color == "B") price += 1_000m;
                if (form.Options.Contains("MES")) price += 2_000m;
                if (form.Options.Contains("GPS")) price += 2_000m;
                if (form.Options.Contains("FSD")) price += 8_000m;

                form.MonthlyPrice = (price * 1.25m) / form.Duration;
            }

            return View("CarLeaseCalculator", form);
        }

        public IActionResult ConnectionMode(string[] cm)
        {
            var result = new List<string>(cm);

            if (result.Contains("FM"))
            {
                result.Remove("WIFI");
                result.Remove("BT");
            }

            return Json(result.ToArray());
        }

        public IActionResult ConnectionSpeed(string modes, int value)
        {
            if (modes != null && modes.Contains("FM"))
            {
                return View("ConnectionSpeedError");
            }
            else
            {
                return View(value);
            }
        }

        public IActionResult DataTable([FromServices] SamplesDbContext context, int page = 1, int pagesize = 3, string q = null)
        {
            var model = new DataTable<Customer>();
            model.Page = page;
            model.PageSize = pagesize;
            model.Query = q;
            var query = context.Customers
                .Where(c => c.Id < 11)
                .Where(c => q == null || c.CompanyName.Contains(q));
            model.LastPage = (query.Count() + pagesize - 1) / pagesize;
            model.Items = query.OrderBy(c => c.Id).Skip((page - 1) * pagesize).Take(pagesize).ToArray();

            return View(model);
        }
    }
}
