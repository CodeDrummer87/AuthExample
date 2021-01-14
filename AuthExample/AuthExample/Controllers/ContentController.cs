using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.Controllers
{
    public class ContentController : Controller
    {
        private AuthExampleContext db;

        public ContentController(AuthExampleContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult StartPage()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public string SaveUserData([FromBody] User user)
        {
            string email = HttpContext.Request.Cookies["email"];

            user.LoginId = db.AuthData.FirstOrDefault(login => login.Email == email).LoginId;
            db.Users.Add(user);
            db.SaveChanges();

            return "/Content/NamedPage";
        }

        [Authorize]
        public IActionResult NamedPage()
        {
            return View();
        }
    }
}