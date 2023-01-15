using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DAL.Repository
{
    public class UserRepository : GenericRepository<UserEntity>,
        IUserRepository
    {
        public UserRepository(EFAppContext context) : base(context)
        {
        }

        public async Task AddProductToBasket(BasketEntity entity)
        {
            await _dbContext.Set<BasketEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserRole(UserRoleEntity roleEntity)
        {
            await _dbContext.Set<UserRoleEntity>().AddAsync(roleEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductBasket(BasketEntity entity)
        {
            _dbContext.Set<BasketEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserRole(UserRoleEntity roleEntity)
        {
            _dbContext.Set<UserRoleEntity>().Remove(roleEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditProductBasket(BasketEntity entity)
        {
            _dbContext.Set<BasketEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserEntity> FindByEmailOrPhone(string findBy)
        {
            return await _dbContext.Set<UserEntity>()
                .AsNoTracking()
                .Include(x => x.Baskets)
                .Include(x => x.UserRoles)
                .Where(e=>e.IsDelete == false)
                .FirstOrDefaultAsync(e => e.Email.ToLower() == findBy.ToLower() || e.Phone == findBy);
        }

        public IEnumerable<RoleEntity> GetAllRoles()
        {
            return _dbContext.Set<RoleEntity>()
                             .AsNoTracking();
        }
    }
}
