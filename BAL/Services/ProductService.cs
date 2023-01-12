using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        public ProductService()
        {
            EFAppContext context = new EFAppContext();
            _productRepository = new ProductRepository(context);
            _productImageRepository = new ProductImageRepository(context);
        }
        public async Task CreateProduct(ProductEntityDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryEntityDTO, CategoryEntity>();
                cfg.CreateMap<ProductImageEntityDTO, ProductImageEntity>();
                cfg.CreateMap<ProductEntityDTO, ProductEntity>()
                    .ForMember(dto => dto.Category, opt => opt.MapFrom(x => x.Category));
            });
            var mapper = new Mapper(config);

            var product = mapper.Map<ProductEntityDTO, ProductEntity>(entity);

            var images = product.Images;

            product.Images = null;
            product.Category = null;

            await _productRepository.Create(product);

            foreach (var image in images)
            {
                image.ProductId = product.Id;
                await _productImageRepository.Create(image);
            }

            entity.Id = product.Id;
        }
    }
}
