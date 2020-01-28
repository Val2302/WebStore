using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Filters;
using WebStore.Domain.Models.Cart;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces.Services;
using WebStore.Services;
using Assert = Xunit.Assert;

namespace WebStore.Tests.ModulTests
{
    [TestClass]
    public class CartServiceTests
    {
        private IMapper _mapper;
        private Mock<IProductData> _productMock;
        private Mock<ICartStore> _cartStore;
        private ICartService _cartService;

        [TestInitialize]
        public void SetupTest()
        {
            _productMock = new Mock<IProductData>();
            _cartStore = new Mock<ICartStore>();

            _mapper = new Mapper(new MapperConfiguration(cfg =>
                cfg.CreateMap<ProductDto, ProductViewModel>()
                    .ForMember(nameof(ProductViewModel.Brand),
                        opt => opt.MapFrom(p => p.Brand != null
                            ? p.Brand.Name : String.Empty))));

            _cartService = new CartService(_productMock.Object, _cartStore.Object, _mapper);
        }

        [TestMethod]
        public void Cart_Class_ItemsCount_Returns_Correct_Quantity()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem()
                    {
                        ProductId = 1,
                        Quantity = 1
                    },
                    new CartItem
                    {
                        ProductId = 3,
                        Quantity = 3
                    }
                }
            };

            var result = cart.ItemsCount;

            Assert.Equal(4, result);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            var cartViewModel = new CartViewModel()
            {
                Items = new Dictionary<ProductViewModel, int>
                {
                    {
                        new ProductViewModel
                        {
                            Id = 1,
                            Name = "TestItem",
                            Price = 5.0m
                        },
                        1
                    },
                    {
                        new ProductViewModel
                        {
                            Id = 2,
                            Name = "TestItem2",
                            Price = 1.0m
                        },
                        2
                    }
                }
            };

            var result = cartViewModel.ItemsCount;

            Assert.Equal(3, result);
        }

        [TestMethod]
        public void CartService_AddToCart_WorksCorrect()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
            };

            _cartStore.Setup(c => c.Cart).Returns(cart);
            _cartService.AddToCart(5);

            Assert.Equal(1, cart.ItemsCount);
            Assert.Single( cart.Items);
            Assert.Equal(5, cart.Items[0].ProductId);
        }

        [TestMethod]
        public void CartService_AddToCart_Increment_Quantity()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 5, Quantity = 2}
                }
            };

            _cartStore.Setup(c => c.Cart).Returns(cart);
            _cartService.AddToCart(5);

            Assert.Single( cart.Items);
            Assert.Equal(3, cart.ItemsCount);
        }

        [TestMethod]
        public void CartService_RemoveFromCart_Removes_Correct_Item()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem(){ProductId = 1,Quantity = 3},
                    new CartItem(){ProductId = 2, Quantity = 1}
                }
            };

            _cartStore.Setup(c => c.Cart).Returns(cart);
            _cartService.RemoveFromCart(1);

            Assert.Single( cart.Items);
            Assert.Equal(2, cart.Items[0].ProductId);
        }

        [TestMethod]
        public void CartService_RemoveAll_Clear_Cart()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem{ProductId = 1,Quantity = 3},
                    new CartItem{ProductId = 2, Quantity = 1}
                }
            };

            _cartStore.Setup(c => c.Cart).Returns(cart);
            _cartService.RemoveAll();

            Assert.Empty(cart.Items);
        }
        [TestMethod]
        public void CartService_Decrement_Correct()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem{ProductId = 1,Quantity = 3},
                    new CartItem{ProductId = 2, Quantity = 1}
                }
            };

            _cartStore.Setup(c => c.Cart).Returns(cart);
            _cartService.DecrementFromCart(1);

            Assert.Equal(3, cart.ItemsCount);
            Assert.Equal(2, cart.Items.Count);
            Assert.Equal(1, cart.Items[0].ProductId);
        }

        [TestMethod]
        public void CartService_Remove_Item_When_Decrement()
        {
            var cart = new Cart()
            {
                Items = new List<CartItem>()
                {
                    new CartItem{ProductId = 1,Quantity = 3},
                    new CartItem{ProductId = 2, Quantity = 1}
                }
            };

            _cartStore.Setup(c => c.Cart).Returns(cart);
            _cartService.DecrementFromCart(2);

            Assert.Equal(3, cart.ItemsCount);
            Assert.Single( cart.Items);
        }

        [TestMethod]
        public void CartService_TransformCart_WorksCorrect()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem(){ProductId = 1, Quantity = 4}
                }
            };
            var model = new PagedProductDto
            {
                Products = new List<ProductDto>
                {
                    new ProductDto
                    {
                        Id = 1,
                        ImageUrl = "",
                        Name = "Test",
                        Order = 0,
                        Price = 1.11m,
                    }
                }, 
                TotalCount = 1
            };

            _productMock.Setup(c =>
                c.GetProducts(It.IsAny<ProductFilter>())).Returns(model);
            _cartStore.Setup(c => c.Cart).Returns(cart);

            var result = _cartService.TransformCart();

            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1.11m, result.Items.First().Key.Price);
        }
    }
}
