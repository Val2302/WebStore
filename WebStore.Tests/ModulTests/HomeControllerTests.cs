using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.Clients;
using Assert = Xunit.Assert;

namespace WebStore.Tests.ModulTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;

        [TestInitialize]
        public void SetupTest()
        {
            var mockValuesService = new Mock<IValuesService>();

            mockValuesService.Setup(c => c.GetAsync()).ReturnsAsync(new List<string> {"1", "2" });
            
            _controller = new HomeController(mockValuesService.Object);
        }

        [TestMethod]
        public async Task Index_Method_Returns_View_With_Values()
        {
            // Arrange and act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [TestMethod]
        public void Contact_Returns_View()
        {
            var viewResult = _controller.Contact();
            Assert.IsType<ViewResult>(viewResult);
        }

        [TestMethod]
        public void ErrorStatus_404_Redirects_to_NotFound()
        {
            var result = _controller.ErrorStatus("404");
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Page404", redirectToActionResult.ActionName);
        }

        [TestMethod]
        public void Checkout_Returns_View()
        {
            var result = _controller.Checkout();
            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            var result = _controller.BlogSingle();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            var result = _controller.Blog();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Error_Returns_View()
        {
            var result = _controller.Error();
            Assert.IsType<ViewResult>(result);
        }
        [TestMethod]
        public void Page404_Returns_View()
        {
            var result = _controller.Page404();
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatus_Another_Returns_Content_Result()
        {
            var result = _controller.ErrorStatus("500");
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Статуcный код ошибки: 500",
                contentResult.Content);
        }

    }
}
