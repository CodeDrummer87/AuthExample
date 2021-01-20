using AuthExample.Models;
using AuthExample.Modules.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthExample.Modules.Implementation
{
    public class AccountDataTransfer : IAccountDataTransfer
    {
        private readonly AuthExampleContext db;
        private readonly IHttpContextAccessor contextAccessor;
      
        public AccountDataTransfer(AuthExampleContext context, IHttpContextAccessor httpContext)
        {
            db = context;
            contextAccessor = httpContext;
        }

        public async Task<string> SaveUserInDb(RegisterModel model)
        {
            if (CheckLoginNotExist(model.Email))
            {
                if (model.Password == model.ConfirmPassword)
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
                    contextAccessor.HttpContext.Response.Cookies.Append("LoginId", loginId.ToString());

                    return "/Content/StartPage";
                }
                return "/Home/Index";
            }
            else return String.Empty;
        }
        public async Task<string> LoginUserInApp(ModelForLogin model)
        {
            LoginModel modelDb = db.AuthData.FirstOrDefault(a => a.Email == model.Email);

            if (modelDb != null && modelDb.Password == GetHashImage(model.Password, modelDb.Salt))
            {
                int userId = db.Users.FirstOrDefault(u => u.LoginId == modelDb.LoginId).UserId;
                await Authenticate(model.Email);

                RegisterSession(userId);

                return "/Content/NamedPage";
            }

            return null;
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

            await contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private void RegisterSession(int userId)
        {
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

            contextAccessor.HttpContext.Response.Cookies.Append("SessionId", session.SessionId);
        }

        private bool CheckLoginNotExist(string login)
        {
            return db.AuthData.FirstOrDefault(e => e.Email == login) == null ? true : false;
        }
    }
}
