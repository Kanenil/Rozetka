﻿using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Mapper;
using BAL.Utilities;
using DAL.Constants;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService()
        {
            EFAppContext context = new EFAppContext();
            _userRepository = new UserRepository(context);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        public async Task AddProductToBasket(BasketEntityDTO entityDTO)
        {
            var basket = _mapper.Map<BasketEntityDTO, BasketEntity>(entityDTO);

            await _userRepository.AddProductToBasket(basket);
        }
        public async Task EditProductBasket(BasketEntityDTO entity)
        {
            var basket = _mapper.Map<BasketEntityDTO, BasketEntity>(entity);
            basket.Product = null;
            basket.User = null;
            await _userRepository.EditProductBasket(basket);
        }
        public async Task DeleteProductBasket(BasketEntityDTO entity)
        {
            var basket = _mapper.Map<BasketEntityDTO, BasketEntity>(entity);
            basket.Product = null;
            basket.User = null;
            await _userRepository.DeleteProductBasket(basket);
        }

        public async Task<UserEntityDTO> Login(UserEntityDTO entity)
        {
            var user = _mapper.Map<UserEntity, UserEntityDTO>(await _userRepository.FindByEmailOrPhone(entity.Email));

            if (user == null)
                throw new Exception("login error");
            else if(user.Password != PasswordHasher.Hash(entity.Password))
                throw new Exception("password error");

            foreach (var basket in user.Baskets)
            {
                basket.Product.Images = basket.Product.Images.OrderBy(x => x.Priority).ToList();
            }

            return user;
        }

        public async Task Registrate(UserEntityDTO entity)
        {
            var user = _mapper.Map<UserEntityDTO, UserEntity>(entity);
            user.DateCreated = DateTime.Now;
            user.Password = PasswordHasher.Hash(entity.Password);

            if (await _userRepository.FindByEmailOrPhone(entity.Email) != null)
                throw new Exception("email error");
            else if (await _userRepository.FindByEmailOrPhone(entity.Phone) != null)
                throw new Exception("phone error");

            await _userRepository.Create(user);

            var roles = _userRepository.GetAllRoles();

            var role = new UserRoleEntity()
            {
                UserId = user.Id,
                RoleId = roles.FirstOrDefault(x => x.Name == "User").Id
            };

            await _userRepository.AddUserRole(role);

            role.User = user;
            role.Role = roles.FirstOrDefault(x => x.Id == role.RoleId);

            entity.UserRoles = new List<UserRoleEntityDTO>()
            {
                _mapper.Map<UserRoleEntity,UserRoleEntityDTO>(role)
            };
            entity.Baskets = new List<BasketEntityDTO>();
            entity.Orders = new List<OrderEntityDTO>();


            entity.Id = user.Id;
        }

        public IEnumerable<UserEntityDTO> GetAllUsers()
        {
            var list = _mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserEntityDTO>>(_userRepository.GetAllUsers());
            if (list != null)
            {
                foreach (var user in list)
                    foreach (var basket in user.Baskets)
                        basket.Product.Images = basket.Product.Images.OrderBy(x => x.Priority).ToList();
            }
            return list;
        }

        public async Task EditUserRole(UserRoleEntityDTO old, string entityDTO)
        {
            var oldUserRole = _mapper.Map<UserRoleEntityDTO, UserRoleEntity>(old);
            oldUserRole.User = null;
            oldUserRole.Role = null;

            var roles = _userRepository.GetAllRoles();
            await _userRepository.EditUserRole(oldUserRole, new UserRoleEntity()
            {
                UserId = oldUserRole.UserId,
                RoleId = roles.FirstOrDefault(x=>x.Name == entityDTO).Id
            });

            old.RoleId = roles.FirstOrDefault(x => x.Name == entityDTO).Id;
            old.Role = _mapper.Map<RoleEntity, RoleEntityDTO>(roles.FirstOrDefault(x => x.Name == entityDTO));
        }
    }
}
