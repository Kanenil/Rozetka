﻿using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Mapper;
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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService()
        {
            EFAppContext context = new EFAppContext();
            _orderRepository = new OrderRepository(context);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }
        public async Task CreateOrder(OrderEntityDTO entity)
        {
            var order = _mapper.Map<OrderEntityDTO, OrderEntity>(entity);
            order.OrderStatus = null;
            order.User = null;
            await _orderRepository.Create(order);
            entity.Id = order.Id;
        }

        public async Task EditOrder(OrderEntityDTO entity)
        {
            var order = _mapper.Map<OrderEntityDTO, OrderEntity>(entity);
            order.OrderStatus = null;
            order.User = null;
            await _orderRepository.Update(order.Id, order);
        }

        public async Task<ICollection<OrderEntityDTO>> GetOrdersBy(Func<OrderEntityDTO, bool> predicate)
        {
            return _mapper.Map<ICollection<OrderEntity>, ICollection<OrderEntityDTO>>(await _orderRepository.Orders.ToListAsync()).Where(predicate).ToList();
        }

        public async Task<ICollection<OrderStatusEntityDTO>> GetOrderStatuses()
        {
            return _mapper.Map<ICollection<OrderStatusEntity>, ICollection<OrderStatusEntityDTO>>(await _orderRepository.OrderStatuses.ToListAsync());
        }

    }
}
