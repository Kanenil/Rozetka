using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repository
{
    public class SaleRepository : GenericRepository<CategoryEntity>
    {
        EFAppContext _saleContext;
        public SaleRepository(EFAppContext context) : base(context)
        {
            _saleContext = context;
        }

        public IEnumerable<SaleEntity> GetAllSales()
        {
            return _saleContext.Sales;
        }
    }
}
