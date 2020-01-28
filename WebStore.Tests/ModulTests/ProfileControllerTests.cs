using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.Dto.Order;
using WebStore.Domain.Models.Order;
using WebStore.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.ModulTests
{
    [TestClass]
    public class ProfileControllerTests
    {
        private IMapper _mapper;

        private ProfileController _controller;

        Mock<IOrdersData> _mockOrdersService;

        [TestInitialize]
        public void SetupTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<OrderDto, UserOrderViewModel>()
                    .ForMember(nameof(UserOrderViewModel.TotalSum),
                        opt => opt.MapFrom(p => p.OrderItems.Sum(i => i.Price)))));

            _mockOrdersService = new Mock<IOrdersData>();

            _controller = new ProfileController(_mockOrdersService.Object, _mapper);
        }

        [TestMethod]
        public void Index_Returns_View()
        {
            _controller.ModelState.AddModelError("error", "InvalidModel");

            var result = _controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ProductList_Returns_View_With_Correct_Item()
        {
            // Arrange
            // create user
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "User1")
            }));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = user
                }
            };

            // create data
            _mockOrdersService.Setup(c => c.GetUserOrders(user.Identity.Name))
                .Returns( new List<OrderDto>
                {
                    new OrderDto()
                    {
                        Id = 1,
                        Name = "Виктор Коробейкин",
                        Phone = "79842137623",
                        Address = "г. Сызрань, ул. Ленина, 67",
                        Date = DateTime.Now,
                        OrderItems = new List<OrderItemDto>
                        {
                            new OrderItemDto()
                            {
                                Id = 3,
                                Quantity = 2,
                                Price = 1234
                            },
                            new OrderItemDto()
                            {
                                Id = 1,
                                Quantity = 1,
                                Price = 1234
                            }
                        }
                    }
                });
            
            // Act
            var correctResult = _controller.Orders();

            // Assert
            var correctViewResult = Assert.IsType<ViewResult>(correctResult);
            var ordersModel = Assert.IsAssignableFrom<List<UserOrderViewModel>>(
                correctViewResult.ViewData.Model);
            Assert.Equal(1, ordersModel[0].Id);
            Assert.Equal("79842137623", ordersModel[0].Phone);
            Assert.Equal("Виктор Коробейкин", ordersModel[0].Name);
        }
    }
}
