using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Utilities;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService()
        {
            EFAppContext context = new EFAppContext();
            _userRepository = new UserRepository(context);
        }

        public async Task<UserEntityDTO> Login(UserEntityDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntity, UserEntityDTO>()
                   .ForMember(dto => dto.Orders, opt => opt.MapFrom(x => x.Orders));
                cfg.CreateMap<UserEntity, UserEntityDTO>();
            });
            var mapper = new Mapper(config);
            var user = mapper.Map<UserEntity, UserEntityDTO>(await _userRepository.FindByEmailOrPhone(entity.Email));

            if (user == null)
                throw new Exception("login error");
            else if(user.Password != PasswordHasher.Hash(entity.Password))
                throw new Exception("password error");

            return user;
        }

        public async Task Registrate(UserEntityDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntityDTO, UserEntity>()
                   .ForMember(dto => dto.Orders, opt => opt.MapFrom(x => x.Orders));
                cfg.CreateMap<UserEntityDTO, OrderEntity>();
            });
            var mapper = new Mapper(config);
            var user = mapper.Map<UserEntityDTO, UserEntity>(entity);
            user.DateCreated = DateTime.Now;
            user.Password = PasswordHasher.Hash(entity.Password);

            if (await _userRepository.FindByEmailOrPhone(entity.Email) != null)
                throw new Exception("email error");
            else if (await _userRepository.FindByEmailOrPhone(entity.Phone) != null)
                throw new Exception("phone error");

            await _userRepository.Create(user);

            entity.Id = user.Id;
        }
    }
}
