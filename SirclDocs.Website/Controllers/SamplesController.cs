using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
    }
}
