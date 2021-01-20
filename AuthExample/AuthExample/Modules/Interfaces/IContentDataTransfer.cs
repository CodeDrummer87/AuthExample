using AuthExample.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthExample.Modules.Interfaces
{
    public interface IContentDataTransfer
    {
        string SaveUserInDb(string firstname, string lastname, string middlename);
        User GetCurrentUser();
    }
}
