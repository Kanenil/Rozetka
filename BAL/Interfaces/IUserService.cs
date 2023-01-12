using BAL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IUserService
    {
        Task Registrate(UserEntityDTO entity);
        Task<UserEntityDTO> Login(UserEntityDTO entity);
    }
}
