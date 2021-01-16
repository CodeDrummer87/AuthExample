using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AuthExample.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
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
        public async Task<string> RegisterUser([FromBody]RegisterModel model)
        {
            if (model.Password == model.ConfirmPassword && model.Email != String.Empty)
            {
                var salt = GetSalt();

                await Authenticate(model.Email);

                LoginModel loginModel = new LoginModel
                { 
                    Email = model.Email,
                    Password = GetHashImage(model.Password, salt),
                    Salt = salt     
                };

                db.AuthData.Add(loginModel);
                db.SaveChanges();

                int loginId = db.AuthData.FirstOrDefault(a => a.Email == model.Email).LoginId;
                HttpContext.Response.Cookies.Append("LoginId", loginId.ToString());

                return "/Content/StartPage";
            }

            return "/Home/Index";
        }

        private byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        private string GetHashImage(string pswrd, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pswrd,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}