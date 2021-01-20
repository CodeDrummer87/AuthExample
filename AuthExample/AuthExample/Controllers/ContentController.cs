using AuthExample.Modules.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.Controllers
{
    [Authorize]
    public class ContentController : Controller
    {
        IContentDataTransfer transfer;

        public ContentController(IContentDataTransfer transfer)
        {
            this.transfer = transfer;
        }

        public IActionResult StartPage()
        {
            return View();
        }

        [HttpGet]
        public string SaveUserData(string firstname, string lastname, string middlename)
        {
            return transfer.SaveUserInDb(firstname, lastname, middlename);
        }

        public IActionResult NamedPage()
        {
            return transfer.GetCurrentUser() == null ?
                RedirectToAction("StartPage", "Content") : (IActionResult)View(transfer.GetCurrentUser());
        }
    }
}