using AuthExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthExample.Modules.Interfaces
{
    public interface IAccountDataTransfer
    {
        Task<string> SaveUserInDb(RegisterModel model);
        Task<string> LoginUserInApp(ModelForLogin model);
    }
}
