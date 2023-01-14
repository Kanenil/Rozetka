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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService()
        {
            EFAppContext context = new EFAppContext();
            _categoryRepository = new CategoryRepository(context);
        }

        public async Task CreateCategory(CategoryEntityDTO entity)
        {
            var category = MapCategory<CategoryEntityDTO, CategoryEntity>(entity);

            await _categoryRepository.Create(category);

            entity.Id = category.Id;
        }

        public async Task DeleteCategory(CategoryEntityDTO entity)
        {
            await _categoryRepository.Delete(entity.Id);
        }

        public async Task EditCategory(CategoryEntityDTO entity)
        {
            var category = MapCategory<CategoryEntityDTO, CategoryEntity>(entity);

            await _categoryRepository.Update(category.Id, category);
        }

        public IEnumerable<CategoryEntityDTO> GetCategories()
        {
            var list = MapCategory<IEnumerable<CategoryEntity>, IEnumerable<CategoryEntityDTO>>(_categoryRepository.GetAll().Include(x => x.Products).ThenInclude(x => x.Images));
            foreach (var category in list)
                foreach (var product in category.Products)
                    product.Images = product.Images.OrderBy(x => x.Priority).ToList();
            return list;
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
