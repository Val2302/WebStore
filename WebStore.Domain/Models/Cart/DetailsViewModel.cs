using WebStore.Domain.Models.Order;

namespace WebStore.Domain.Models.Cart
{
    public class DetailsViewModel
    {
        public CartViewModel CartViewModel { get; set; }
        public OrderViewModel OrderViewModel { get; set; }

    }
}
