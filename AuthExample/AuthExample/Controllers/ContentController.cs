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

        [HttpGet]
        public string SaveUserData(string firstname, string lastname, string middlename)
        {
            int userLoginId = Convert.ToInt32(HttpContext.Request.Cookies["LoginId"]);
            User user = new User
            {
                FirstName = firstname,
                LastName = lastname,
                MiddleName = middlename,
                LoginId = userLoginId
            };

            db.Users.Add(user);
            db.SaveChanges(); 

            int userId = db.Users.FirstOrDefault(u => u.LoginId == userLoginId).UserId;
            DateTime currentDateTime = DateTime.Now;

            SessionModel session = new SessionModel
            {
                SessionId = Guid.NewGuid().ToString(),
                UserId = userId,
                Created = currentDateTime,
                Expired = currentDateTime.AddMinutes(15)
            };

            db.Sessions.Add(session);
            db.SaveChanges();

            HttpContext.Response.Cookies.Delete("LoginId");
            HttpContext.Response.Cookies.Append("SessionId", session.SessionId);

            return "/Content/NamedPage";
        }

        public IActionResult NamedPage()
        {
            string sessionId = HttpContext.Request.Cookies["SessionId"];

            if (sessionId != null)
            {
                int userId = db.Sessions.FirstOrDefault(s => s.SessionId == sessionId).UserId;
                User user = db.Users.FirstOrDefault(u => u.UserId == userId);

                if (user != null)
                {
                    return View(user);
                }
                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}