using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Models.Order;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Route("api/orders"),
     Produces("application/json"),
     ApiController]
    public class OrdersApiController : ControllerBase, IOrdersData
    {
        private readonly IOrdersData _ordersData;

        public OrdersApiController(IOrdersData ordersData)
        {
            _ordersData = ordersData;
        }

        //api/orders/user/userName GET
        [HttpGet("user/{userName}"), ActionName("Get")]
        public IEnumerable<OrderDto> GetUserOrders(string userName) => _ordersData.GetUserOrders(userName);

        //api/orders/id GET
        [HttpGet("{id}"), ActionName("Get")]
        public OrderDto GetOrderById(int id) => _ordersData.GetOrderById(id);

        //api/orders/userName? POST
        [HttpPost("{userName?}"), ActionName("Post")]
        public OrderDto CreateOrder([FromBody] CreateOrderModel orderModel, string userName)
            => _ordersData.CreateOrder(orderModel, userName);
    }
}