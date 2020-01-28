using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebStore.Domain.Filters;
using WebStore.Domain.Models.Cart;
using WebStore.Domain.Models.Product;
using WebStore.Interfaces.Services;

namespace WebStore.Services
{
    public class CartService : ICartService
    {
        private readonly IProductData _productData;
        private readonly ICartStore _cartStore;
        private readonly IMapper _mapper;
        
        public CartService(IProductData productData, ICartStore cartStore, IMapper mapper)
        {
            _mapper = mapper;
            _productData = productData;
            _cartStore = cartStore;
        }

        public void DecrementFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                if (item.Quantity > 0)
                    --item.Quantity;

                if (item.Quantity == 0)
                    cart.Items.Remove(item);
            }

            _cartStore.Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
                cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        public void RemoveAll()
        {
            _cartStore.Cart.Items.Clear();
        }

        public void AddToCart(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
                ++item.Quantity;
            else
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1} );

            _cartStore.Cart = cart;
        }

        public CartViewModel TransformCart()
        {
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(
                _productData.GetProducts(new ProductFilter
                {
                    Ids = _cartStore.Cart.Items.Select(i => i.ProductId).ToList()
                }).Products);
            
            var cartViewModel = new CartViewModel
            {
                Items = _cartStore.Cart.Items.ToDictionary(
                    x => products.First(y => y.Id == x.ProductId),
                    x => x.Quantity)
            };

            return cartViewModel;
        }
    }
}
