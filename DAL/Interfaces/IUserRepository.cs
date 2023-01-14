using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity, int>
    {
        Task<UserEntity> FindByEmailOrPhone(string findBy);
        Task AddProductToBasket(BasketEntity entity);
    }
}
