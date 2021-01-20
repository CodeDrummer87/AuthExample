using AuthExample.Models;
using AuthExample.Modules.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace AuthExample.Modules.Implementation
{
    [Authorize]
    public class ContentDataTransfer : IContentDataTransfer
    {
        AuthExampleContext db;
        IHttpContextAccessor contextAccessor;

        public ContentDataTransfer(AuthExampleContext context, IHttpContextAccessor httpContext)
        {
            db = context;
            contextAccessor = httpContext;
        }

        public string SaveUserInDb(string firstname, string lastname, string middlename)
        {
            int userLoginId = Convert.ToInt32(contextAccessor.HttpContext.Request.Cookies["LoginId"]);
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

            contextAccessor.HttpContext.Response.Cookies.Delete("LoginId");
            contextAccessor.HttpContext.Response.Cookies.Append("SessionId", session.SessionId);

            return "/Content/NamedPage";
        }

        public User GetCurrentUser()
        {
            string sessionId = contextAccessor.HttpContext.Request.Cookies["SessionId"];

            if (sessionId != null)
            {
                int userId = db.Sessions.FirstOrDefault(s => s.SessionId == sessionId).UserId;
                User user = db.Users.FirstOrDefault(u => u.UserId == userId);

                if (user != null)
                {
                    return user;
                }
            }

            return null;
        }
    }
}
