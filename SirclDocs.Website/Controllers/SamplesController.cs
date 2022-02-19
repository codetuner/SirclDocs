using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SirclDocs.Website.Data.Samples;
using SirclDocs.Website.Models.Samples;

namespace SirclDocs.Website.Controllers
{
    [EnableCors("SamplesPolicy")]
    public class SamplesController : Controller
    {
        #region Shared sample actions

        public IActionResult BrownFox(int opt = 0)
        {
            return View(opt);
        }

        public IActionResult LoremIpsum(int opt = 0)
        {
            return View(opt);
        }

        public IActionResult LizaDonnovan()
        {
            return View();
        }

        public IActionResult PeterJohnson()
        {
            return View();
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
        /// Change actions save change.
        /// </summary>
        public IActionResult SaveChange(string name = null, string value = null, bool? @checked = null)
        {
            return StatusCode(200);
        }

        #endregion

        #region TaskList sample

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

        #endregion

        #region DependentSelect sample

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

        #endregion

        #region AutoComplete sample

        /// <summary>
        /// Auto-Complete companies
        /// </summary>
        public IActionResult AcCompany([FromServices] SamplesDbContext context, string value = "", int take = 12)
        {
            var model = context.Customers.Where(c => c.CompanyName.Contains(value)).Take(take).Select(c => c.CompanyName).Distinct().OrderBy(s => s).ToList();
            if (model.Count == 1 && model[0] == value) model.Clear();
            return View("AcCompany", model);
        }

        #endregion

        #region CarLeaseCalculator sample

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

        #endregion

        #region ChangeActions sample

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

        #endregion

        #region DataTable samples

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

        public IActionResult DataTableWithSelection([FromServices] SamplesDbContext context, int page = 1, int pagesize = 3, string q = null)
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

        public IActionResult DataTableSelection([FromServices] SamplesDbContext context, int[] selection = null)
        {
            selection = selection ?? Array.Empty<int>();
            var model = new DataTable<Customer>();
            var query = context.Customers
                .Where(c => c.Id < 11)
                .Where(c => selection.Contains(c.Id));
            model.Items = query.OrderBy(c => c.Id).ToArray();

            return View("DataTableSelection", model);
        }

        #endregion

        #region InfiniteScroll sample

        public IActionResult InfiniteScroll([FromServices] SamplesDbContext context, int toskip = 0)
        {
            var model = new DataTable<Customer>();
            model.Page = toskip;
            model.PageSize = 4;
            var query = context.Customers
                .Where(c => c.Id <= 120)
                .OrderBy(c => c.Id)
                .Skip(toskip).Take(model.PageSize);
            model.Items = query.ToArray();

            return View(model);
        }

        #endregion

        #region TaskBoard Drag&Drop sample

        [HttpGet]
        public IActionResult TaskBoard()
        {
            return View(new TaskBoardModel());
        }

        [HttpPost]
        public IActionResult TaskBoard(TaskBoardModel model)
        {
            return View("TaskBoard", model);
        }

        [HttpPost]
        public IActionResult TaskBoardAdd(TaskBoardModel model)
        {
            if (String.IsNullOrWhiteSpace(model.NewTask.Label))
                ModelState.AddModelError("NewTask.Label", "Please enter a label for the task.");
            if (model.NewTask.Estimate < 0)
                ModelState.AddModelError("NewTask.Estimate", "Estimate cannot be negative.");

            if (ModelState.IsValid)
            {
                ModelState.Clear();
                model.NewTask.Id = model.NextId++;
                model.Todos.Add(model.NewTask);
                model.NewTask = new();
                Response.Headers["X-Sircl-Target"] = "#demo";
                return TaskBoard(model);
            }
            else
            {
                return View("TaskBoardAddModal", model);
            }
        }

        [HttpPost]
        public IActionResult TaskBoardRemove(TaskBoardModel model, int id)
        {
            if (model.Todos.Any(t => t.Id == id))
            {
                model.Todos.RemoveAll(t => t.Id == id);
            }
            else if (model.Doings.Any(t => t.Id == id))
            {
                model.Doings.RemoveAll(t => t.Id == id);
            }
            else if (model.Dones.Any(t => t.Id == id))
            {
                model.Dones.RemoveAll(t => t.Id == id);
            }

            ModelState.Clear();

            return TaskBoard(model);
        }

        [HttpPost]
        public IActionResult TaskBoardDrop(TaskBoardModel model, int id, int zone)
        {
            TaskBoardItem item = null;
            if (model.Todos.Any(t => t.Id == id))
            {
                item = model.Todos.Where(t => t.Id == id).SingleOrDefault();
                model.Todos.RemoveAll(t => t.Id == id);
            }
            else if (model.Doings.Any(t => t.Id == id))
            {
                item = model.Doings.Where(t => t.Id == id).SingleOrDefault();
                model.Doings.RemoveAll(t => t.Id == id);
            }
            else if (model.Dones.Any(t => t.Id == id))
            {
                item = model.Dones.Where(t => t.Id == id).SingleOrDefault();
                model.Dones.RemoveAll(t => t.Id == id);
            }

            if (item != null)
            {
                switch (zone)
                {
                    case 0:
                        model.Todos.Add(item);
                        break;
                    case 1:
                        model.Doings.Add(item);
                        break;
                    case 2:
                        model.Dones.Add(item);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("zone");
                }
            }

            ModelState.Clear();

            return TaskBoard(model);
        }

        #endregion

        #region RecipeBook sample

        private string[] ingredients = new string[] { "Water", "Milk", "Olive Oil", "Eggs", "Flour", "Baking Powder", "Butter", "Salt", "Sugar", "Yeast", "Cocoa Powder", "Raisins", "Gelatin", "Cream" };

        public IActionResult RecipeBook()
        {
            return View("RecipeBook");
        }

        public IActionResult RecipeBookAdd()
        {
            var model = new Recipe();
            return RecipeModalView(model);
        }

        [HttpPost]
        public IActionResult RecipeAddLine(Recipe model)
        {
            ModelState.Clear();

            if (String.IsNullOrWhiteSpace(model.NewIngredient.Dosage))
            {
                ModelState.AddModelError("NewIngredient.Dosage", "Dosage required.");
            }

            if (ModelState.IsValid)
            {
                model.Ingredients.Add(model.NewIngredient);
                model.NewIngredient = new IngredientItem();
                return RecipeModalView(model);
            }
            else
            {
                return RecipeModalView(model);
            }
        }

        [HttpPost]
        public IActionResult RecipeModalRemoveLine(Recipe model, int index)
        {
            ModelState.Clear();

            model.Ingredients.RemoveAt(index);

            return RecipeModalView(model);
        }

        private IActionResult RecipeModalView(Recipe model)
        {
            model.AvailableIngredients = ingredients.Except(model.Ingredients.Select(i => i.Name)).OrderBy(i => i).ToList();
            return View("RecipeModal", model);
        }

        [HttpPost]
        public IActionResult RecipeBookSave(Recipe model)
        {
            if (String.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Name required.");
            }

            if (model.Ingredients.Count == 0 && String.IsNullOrWhiteSpace(model.NewIngredient.Dosage))
            {
                ModelState.AddModelError("", "Recipe must have at least one ingredient.");
            }

            if (ModelState.IsValid)
            {
                if (!String.IsNullOrWhiteSpace(model.NewIngredient.Dosage))
                {
                    model.Ingredients.Add(model.NewIngredient);
                }

                model.Id = Environment.TickCount;

                Response.Headers.Add("X-Sircl-Target", "#recipe-list");
                Response.Headers.Add("X-Sircl-Target-Method", "append");
                return View("Recipe", model);
            }
            else
            {
                return RecipeModalView(model);
            }
        }

        #endregion

        #region Expenses sample

        [HttpGet, HttpPost]
        public IActionResult Expense(ExpenseModel model)
        {
            if (model.Item == null)
            {
                model.Item = new();
                //model.Item.Lines.Add(new Models.Samples.ExpenseLine() { Id = 1, Description = "First expense", Category = "Marketing", Date = DateTime.Now, Amount = 150.95m });
                //model.Item.Lines.Add(new Models.Samples.ExpenseLine() { Id = 2, Description = "Second expense", Category = "Finance", Date = DateTime.Now, Amount = 1123.45m });
            }

            ModelState.Clear();
            return ExpenseView(model);
        }

        [HttpPost]
        public IActionResult ExpenseLineNew(ExpenseModel model)
        {
            model.CurrentLine = new ExpenseLine();

            ModelState.Clear();
            return ExpenseLineView(model);
        }

        [HttpPost]
        public IActionResult ExpenseLineEdit(ExpenseModel model, int index)
        {
            model.CurrentLine = model.Item.Lines[index];

            ModelState.Clear();
            return ExpenseLineView(model);
        }

        [HttpPost]
        public IActionResult ExpenseLineRemove(ExpenseModel model, int index)
        {
            model.Item.Lines.RemoveAt(index);

            ModelState.Clear();
            return ExpenseView(model);
        }

        [HttpPost]
        public IActionResult ExpenseLine(ExpenseModel model)
        {

            ModelState.Clear();
            return ExpenseLineView(model);
        }

        [HttpPost]
        public IActionResult ExpenseLineSubmit(ExpenseModel model)
        {
            foreach (var key in ModelState.Keys.ToList())
            { 
                if (key !="CurrentLine" && !key.StartsWith("CurrentLine."))
                {
                    ModelState.Remove(key);
                }
            }

            if (ModelState.IsValid)
            {
                if (model.CurrentLine.Id == 0)
                {
                    model.CurrentLine.Id = Environment.TickCount;
                    model.Item.Lines.Add(model.CurrentLine);
                }
                else
                {
                    for (int i = 0; i < model.Item.Lines.Count; i++)
                    {
                        if (model.Item.Lines[i].Id == model.CurrentLine.Id)
                        {
                            model.Item.Lines[i] = model.CurrentLine;
                            break;
                        }
                    }
                }
                ModelState.Clear();
                return ExpenseView(model);
            }
            else
            {
                return ExpenseLineView(model);
            }
        }

        [HttpPost]
        public IActionResult ExpenseSubmit(ExpenseModel model, IFormCollection form)
        {
            if (model.Item.Lines.Count == 0)
            {
                ModelState.AddModelError("Item.Lines", "Expense note must contain at least one expense line.");
            }

            if (ModelState.IsValid)
            {
                return Saved(form, "Expense note submitted.", true);
            }
            else
            {
                return ExpenseView(model);
            }
        }

        private IActionResult ExpenseView(ExpenseModel model)
        {
            model.CurrentLine = null;

            model.Managers = new List<string> { "", "Alex Vandermeersch", "Jacky Smith", "Liza Montenegro", "Peter Connor" }
                .Select(i => new SelectListItem(i, i))
                .ToList();

            model.CostCenters = new List<string> { "", "Direction", "Finances", "Marketing", "Production" }
                .Select(i => new SelectListItem(i, i))
                .ToList();

            return View("Expense", model);
        }

        private IActionResult ExpenseLineView(ExpenseModel model)
        {
            Response.Headers["X-Sircl-Target"] = "#linedialog";

            model.Categories = new List<string> { "", "Hotel", "Transportation", "Material", "Meal", "Other" }
                .Select(i => new SelectListItem(i, i))
                .ToList();

            return View("ExpenseLine", model);
        }

        #endregion
    }
}
