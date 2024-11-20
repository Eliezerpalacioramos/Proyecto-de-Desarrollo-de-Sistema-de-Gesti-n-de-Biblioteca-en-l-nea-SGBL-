using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SGBL.Data.Models;
using System.Threading.Tasks;

namespace SGBL.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(User user);
        Task<bool> IsEmailTaken(string email);
    }
}
