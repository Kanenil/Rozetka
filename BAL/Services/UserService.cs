using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Utilities;
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
        private readonly ICategoryRepository _categoryRepository;
        public UserService()
        {
            EFAppContext context = new EFAppContext();
            _userRepository = new UserRepository(context);
            _categoryRepository = new CategoryRepository(context);
        }

        public async Task AddProductToBasket(BasketEntityDTO entityDTO)
        {
            var basket = MapBasket<BasketEntityDTO, BasketEntity>(entityDTO);

            await _userRepository.AddProductToBasket(basket);
        }

        public async Task<UserEntityDTO> Login(UserEntityDTO entity)
        {
            var user = MapUser<UserEntity, UserEntityDTO>(await _userRepository.FindByEmailOrPhone(entity.Email));

            if (user == null)
                throw new Exception("login error");
            else if(user.Password != PasswordHasher.Hash(entity.Password))
                throw new Exception("password error");

            var list = MapCategory<IEnumerable<CategoryEntity>, IEnumerable<CategoryEntityDTO>>(_categoryRepository.GetAll().Include(x => x.Products).ThenInclude(x => x.Images));
            foreach (var category in list)
                foreach (var product in category.Products)
                    product.Images = product.Images.OrderBy(x => x.Priority).ToList();


            foreach (var category in list)
            {
                foreach (var basket in user.Baskets)
                {
                    foreach (var product in category.Products)
                    {
                        if (product.Id == basket.ProductId)
                        {
                            basket.Product = product;
                            break;
                        }
                    }
                }
            }

            return user;
        }

        public async Task Registrate(UserEntityDTO entity)
        {
            var user = MapUser<UserEntityDTO, UserEntity>(entity);
            user.DateCreated = DateTime.Now;
            user.Password = PasswordHasher.Hash(entity.Password);

            if (await _userRepository.FindByEmailOrPhone(entity.Email) != null)
                throw new Exception("email error");
            else if (await _userRepository.FindByEmailOrPhone(entity.Phone) != null)
                throw new Exception("phone error");

            await _userRepository.Create(user);

            entity.Id = user.Id;
        }

        private TEntityTo MapUser<TEntityFrom, TEntityTo>(TEntityFrom entityDTOs)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserEntityDTO, UserEntity>()
                   .ForMember(dto => dto.Orders, opt => opt.MapFrom(x => x.Orders));
                cfg.CreateMap<OrderEntityDTO, OrderEntity>();
                cfg.CreateMap<BasketEntityDTO, BasketEntity>();

                cfg.CreateMap<UserEntity, UserEntityDTO>()
                   .ForMember(dto => dto.Orders, opt => opt.MapFrom(x => x.Orders));
                cfg.CreateMap<OrderEntity, OrderEntityDTO>();
                cfg.CreateMap<BasketEntity, BasketEntityDTO>();
            });
            var mapper = new Mapper(config);

            return mapper.Map<TEntityFrom, TEntityTo>(entityDTOs);
        }

        private TEntityTo MapBasket<TEntityFrom, TEntityTo>(TEntityFrom entityDTOs)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BasketEntityDTO, BasketEntity>();
                cfg.CreateMap<UserEntityDTO, BasketEntity>();
                cfg.CreateMap<CategoryEntityDTO, CategoryEntity>();
                cfg.CreateMap<ProductImageEntityDTO, ProductImageEntity>();
                cfg.CreateMap<ProductEntityDTO, ProductEntity>()
                    .ForMember(dto => dto.Category, opt => opt.MapFrom(x => x.Category));

                cfg.CreateMap<BasketEntity, BasketEntityDTO>();
                cfg.CreateMap<UserEntity, UserEntityDTO>();
                cfg.CreateMap<CategoryEntity, CategoryEntityDTO>();
                cfg.CreateMap<ProductImageEntity, ProductImageEntityDTO>();
                cfg.CreateMap<ProductEntity, ProductEntityDTO>()
                    .ForMember(dto => dto.Category, opt => opt.MapFrom(x => x.Category));

            });
            var mapper = new Mapper(config);

            return mapper.Map<TEntityFrom, TEntityTo>(entityDTOs);
        }

        private TEntityTo MapCategory<TEntityFrom, TEntityTo>(TEntityFrom entityDTOs)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductImageEntity, ProductImageEntityDTO>();
                cfg.CreateMap<CategoryEntity, CategoryEntityDTO>()
                   .ForMember(dto => dto.Products, opt => opt.MapFrom(x => x.Products));
                cfg.CreateMap<ProductEntity, ProductEntityDTO>()
                   .ForMember(dto => dto.Images, opt => opt.MapFrom(x => x.Images));

                cfg.CreateMap<ProductImageEntityDTO, ProductImageEntity>();
                cfg.CreateMap<CategoryEntityDTO, CategoryEntity>()
                   .ForMember(dto => dto.Products, opt => opt.MapFrom(x => x.Products));
                cfg.CreateMap<ProductEntityDTO, ProductEntity>()
                   .ForMember(dto => dto.Images, opt => opt.MapFrom(x => x.Images));
            });
            var mapper = new Mapper(config);

            return mapper.Map<TEntityFrom, TEntityTo>(entityDTOs);
        }
    }
}
