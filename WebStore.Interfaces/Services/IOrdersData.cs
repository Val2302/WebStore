using System.Collections.Generic;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Models.Order;

namespace WebStore.Interfaces.Services
{
    public interface IOrdersData
    {
        IEnumerable<OrderDto> GetUserOrders(string userName);
        OrderDto GetOrderById(int id);
        OrderDto CreateOrder(CreateOrderModel orderModel, string userName);
    }
}
