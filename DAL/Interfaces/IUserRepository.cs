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
        IEnumerable<RoleEntity> GetAllRoles();
        Task AddUserRole(UserRoleEntity roleEntity);
        Task DeleteUserRole(UserRoleEntity roleEntity);
        Task AddProductToBasket(BasketEntity entity);
        Task EditProductBasket(BasketEntity entity);
        Task DeleteProductBasket(BasketEntity entity);
    }
}
