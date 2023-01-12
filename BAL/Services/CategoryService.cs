using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
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

        public IEnumerable<CategoryEntityDTO> GetCategories()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProductImageEntity, ProductImageEntityDTO>();
                cfg.CreateMap<CategoryEntity, CategoryEntityDTO>()
                   .ForMember(dto => dto.Products, opt => opt.MapFrom(x => x.Products));
                cfg.CreateMap<ProductEntity, ProductEntityDTO>()
                   .ForMember(dto=>dto.Images,opt=>opt.MapFrom(x => x.Images));

            });
            var mapper = new Mapper(config);

            var list = _categoryRepository.GetAll().Include(x=>x.Products).ThenInclude(x=>x.Images);

            return mapper.Map<IEnumerable<CategoryEntity>, IEnumerable<CategoryEntityDTO>>(list);
        }
    }
}
