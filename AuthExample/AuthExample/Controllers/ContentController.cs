using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.Controllers
{
    public class ContentController : Controller
    {
        public IActionResult StartPage()
        {
            return View();
        }
    }
}