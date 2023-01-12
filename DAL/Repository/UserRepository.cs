﻿using DAL.Data.Entities;
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

        public async Task<UserEntity> FindByEmailOrPhone(string findBy)
        {
            return await _dbContext.Set<UserEntity>()
                .AsNoTracking()
                .Where(e=>e.IsDelete == false)
                .FirstOrDefaultAsync(e => e.Email.ToLower() == findBy.ToLower() || e.Phone == findBy);
        }
    }
}
