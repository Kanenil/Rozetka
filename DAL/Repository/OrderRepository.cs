using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repository
{
    public class OrderRepository : GenericRepository<OrderEntity>,
        IOrderRepository
    {
        public OrderRepository(EFAppContext context) : base(context)
        {
        }

        public IQueryable<OrderEntity> Orders => GetAll()
                                                    .Include(x=>x.OrderStatus)
                                                    .Include(x=>x.User)
                                                    .Include(x=>x.OrderItems)
                                                        .ThenInclude(x=>x.Product)
                                                            .ThenInclude(x=>x.Category)
                                                    .Include(x => x.OrderItems)
                                                        .ThenInclude(x => x.Product)
                                                            .ThenInclude(x => x.Images);

        public IQueryable<OrderStatusEntity> OrderStatuses => _dbContext.OrderStatuses.AsNoTracking();
    }
}
