using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.Controllers
{
    public class AccountController : Controller
    {
        private AuthExampleContext db;

        public AccountController(AuthExampleContext context)
        {
            db = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public string RegisterUser([FromBody]RegisterModel model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                LoginModel loginModel = new LoginModel
                { 
                    Email = model.Email,
                    Password = model.Password,
                    //.:: temporary code
                    Salt = null     
                };

                db.AuthData.Add(loginModel);
                db.SaveChanges();

                return ".:: Регистрация успешно завершена";
            }
            else
            {
                return ".:: Ошибка регистрации";
            }
        }
    }
}