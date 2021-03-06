﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Models.Cart;
using WebStore.Domain.Models.Order;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrdersData _ordersData;

        public CartController(ICartService cartService, IOrdersData ordersData)
        {
            _cartService = cartService;
            _ordersData = ordersData;
        }

        public IActionResult Details()
        {
            var model = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = new OrderViewModel()
            };

            return View(model);
        }

        public IActionResult DecrementFromCart(int id)
        {
            _cartService.DecrementFromCart(id);

            return Json(new {id, message = "The count of product decreased by 1" } );
        }

        public IActionResult RemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);

            return Json(new { id, message = "The count of product decreased by 1" } );
        }

        public IActionResult RemoveAll()
        {
            _cartService.RemoveAll();
            return RedirectToAction("Details");
        }

        public IActionResult AddToCart(int id)
        {
            _cartService.AddToCart(id);

            return Json(new {id, message = "Product added to backet"});
        }

        public IActionResult GetCartView()
        {
            return ViewComponent("Cart");
        }

        [HttpPost,
         ValidateAntiForgeryToken]
        public IActionResult Checkout(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createOrder = new CreateOrderModel
                {
                    OrderViewModel = model,
                    OrderItems = new List<OrderItemDto>()
                };

                foreach (var orderItem in _cartService.TransformCart().Items)
                {
                    createOrder.OrderItems.Add(new OrderItemDto()
                    {
                        Id = orderItem.Key.Id,
                        Price = orderItem.Key.Price,
                        Quantity = orderItem.Value
                    });
                }

                var orderResult = _ordersData.CreateOrder(createOrder,
                    User.Identity.Name);

                _cartService.RemoveAll();

                return RedirectToAction("OrderConfirmed", new { id = orderResult.Id });
            }
            var detailsModel = new DetailsViewModel()
            {
                CartViewModel = _cartService.TransformCart(),
                OrderViewModel = model
            };
            return View("Details", detailsModel);
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }
}