using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Models.Cart;
using WebStore.Domain.Models.Order;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;


namespace WebStore.Tests.ModulTests
{
    [TestClass]
    public class CartControllerTests
    {
        private CartController _controller;

        Mock<ICartService> _mockCartService;
        Mock<IOrdersData> _mockOrdersService;

        [TestInitialize]
        public void SetupTest()
        {
            _mockCartService = new Mock<ICartService>();
            _mockOrdersService = new Mock<IOrdersData>();

            _controller = new CartController(_mockCartService.Object,
                _mockOrdersService.Object);
        }

        [TestMethod]
        public void Checkout_ModelState_Invalid_Returns_ViewModel()
        {
            _controller.ModelState.AddModelError("error", "InvalidModel");

            var result = _controller.Checkout(new OrderViewModel()
            {
                Name = "test"
            });

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<DetailsViewModel>(viewResult.ViewData.Model);
            Assert.Equal("test", model.OrderViewModel.Name);
        }

        [TestMethod]
        public void Checkout_Calls_Service_And_Return_Redirect()
        {
            #region Arrange
            // create user
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));

            // setup cartService
            _mockCartService.Setup(c => c.TransformCart()).Returns(new
                CartViewModel()
            {
                Items = new Dictionary<ProductViewModel, int>()
                    {
                        { new ProductViewModel(), 1 }
                    }
            });

            // setup ordersService
            _mockOrdersService.Setup(c =>
                    c.CreateOrder(It.IsAny<CreateOrderModel>(), It.IsAny<string>()))
                .Returns(new OrderDto() { Id = 1 });

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };
            #endregion

            // Act
            var result = _controller.Checkout(new OrderViewModel()
            {
                Name = "test",
                Address = "",
                Phone = ""
            });

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectResult.ControllerName);
            Assert.Equal("OrderConfirmed", redirectResult.ActionName);
            Assert.Equal(1, redirectResult.RouteValues["id"]);
        }
    }
}
