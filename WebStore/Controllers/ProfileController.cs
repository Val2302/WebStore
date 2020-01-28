using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Models.Order;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrdersData _ordersData;
        public ProfileController(IOrdersData ordersData, IMapper mapper)
        {

            _mapper = mapper;
            _ordersData = ordersData;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = _ordersData.GetUserOrders(User.Identity.Name);

            var orderModels = _mapper.Map<IEnumerable<UserOrderViewModel>>(orders);

            return View(orderModels);
        }
    }
}