using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Sql
{
    public class SqlOrdersData : IOrdersData
    {
        private readonly IMapper _mapper;
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrdersData(WebStoreContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<OrderDto> GetUserOrders(string userName)
        {
            return _mapper.Map<IEnumerable<OrderDto>>(
                _context.Orders.Include("User").Include("OrderItems")
                    .Where(o => o.User.UserName.Equals(userName)));
        }

        public OrderDto GetOrderById(int id)
        {
            return _mapper.Map<OrderDto>(
                _context.Orders.Include("OrderItems")
                    .FirstOrDefault(o => o.Id.Equals(id)));
        }

        public OrderDto CreateOrder(CreateOrderModel orderModel, string userName)
        {
            User user = null;

            if (!string.IsNullOrEmpty(userName))
                user = _userManager.FindByNameAsync(userName).Result;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderModel.OrderViewModel.Address,
                    Name = orderModel.OrderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = orderModel.OrderViewModel.Phone,
                    User = user
                };

                _context.Orders.Add(order);

                foreach (var item in orderModel.OrderItems)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id.Equals(item.Id));

                    if (product == null)
                        throw new InvalidOperationException("Продукт не найден в базе");

                    var orderItem = new OrderItem()
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Product = product
                    };
                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                transaction.Commit();

                return GetOrderById(order.Id);
            }
        }
    }
}
