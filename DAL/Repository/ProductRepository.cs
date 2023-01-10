using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class ProductRepository : GenericRepository<ProductEntity>,
        IProductRepository
    {
        public ProductRepository(EFAppContext context) : base(context)
        {
        }
    }
}
