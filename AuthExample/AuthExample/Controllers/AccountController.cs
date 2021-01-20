using AuthExample.Models;
using AuthExample.Modules.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountDataTransfer transfer;

        public AccountController(IAccountDataTransfer transfer)
        {
            this.transfer = transfer;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> RegisterUser([FromBody]RegisterModel model)
        {
            return await transfer.SaveUserInDb(model);
        }

        [HttpPost]
        public async Task<string> LoginUser([FromBody] ModelForLogin model)
        {
            return await transfer.LoginUserInApp(model);
        }
    }
}