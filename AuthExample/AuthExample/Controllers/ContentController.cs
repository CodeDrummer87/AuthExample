using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        private AuthExampleContext db;

        public ContentController(AuthExampleContext context)
        {
            db = context;
        }

        public IActionResult StartPage()
        {
            return View();
        }

        [HttpPost]
        public string SaveUserData([FromBody] User user)
        {
            string email = HttpContext.Request.Cookies["email"];

            user.LoginId = db.AuthData.FirstOrDefault(login => login.Email == email).LoginId;
            db.Users.Add(user);
            db.SaveChanges();

            return "/Content/NamedPage";
        }

        public IActionResult NamedPage()
        { 
            return View();
        }
    }
}