using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Linq;
using System.Net;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {

        public static ProductViewModel ToProductViewModel(this Product productDB)
        {
            return new ProductViewModel
            {
                Id = productDB.Id,
                Name = productDB.Name,
                Cost = productDB.Cost,
                Description = productDB.Description,
                ImagePath = productDB.ImagePath,
            };
        }

        public static ICollection<ProductViewModel> ToProductsViewModels(this ICollection<Product> products)
        {
            ICollection<ProductViewModel> productsVM = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var productVM = product.ToProductViewModel();
                productsVM.Add(productVM);
            }
            return productsVM;
        }

        public static Product ToProductDb(this ProductViewModel productVM)
        {
            return new Product
            {
                Id = productVM.Id,
                Name = productVM.Name,
                Cost = productVM.Cost,
                Description = productVM.Description,
                ImagePath = productVM.ImagePath
            };
        }

        public static CartViewModel ToCartViewModel(this Cart cartDB)
        {
            var cartItemsVM = new List<CartItemViewModel>();

            foreach (var cartItem in cartDB.Items)
            {
                var cartItemVM = new CartItemViewModel
                {
                    Id = cartItem.Id,
                    Product = new ProductViewModel
                    {
                        Id = cartItem.Product.Id,
                        Name = cartItem.Product.Name,
                        Description = cartItem.Product.Description,
                        Cost = cartItem.Product.Cost,
                        ImagePath = cartItem.Product.ImagePath
                    },
                    Amount = cartItem.Amount
                };
                cartItemsVM.Add(cartItemVM);
            }


            var cartVM = new CartViewModel
            {
                Id = cartDB.Id,
                UserId = cartDB.UserId,
                Items = cartItemsVM
            };

            return cartVM;
        }



        public static OrderViewModel ToOrderViewModel(this Order orderDb)
        {
            return new OrderViewModel
            {
                Id = orderDb.Id,
                UserInfo = orderDb.UserInfo.ToUserDeliveryInfoViewModel(),
                Items = orderDb.Items.Select(x => x.ToCartItemViewModel()).ToList(),
                CreateDataTime = orderDb.CreateDataTime,
                Status = (OrderStatusViewModel)(int)orderDb.Status

            };
        }

        public static UserDeliveryInfoViewModel ToUserDeliveryInfoViewModel(this UserDeliveryInfo userDb)
        {
            return new UserDeliveryInfoViewModel
            {
                Id = userDb.Id,
                Email = userDb.Email,
                Name = userDb.Name,
                Surname = userDb.Surname,
                Patronymic = userDb.Patronymic,
                Phone = userDb.Phone,
                Address = userDb.Address,
            };
        }

        public static UserDeliveryInfo ToUserDeliveryInfo(this UserDeliveryInfoViewModel userVM)
        {
            return new UserDeliveryInfo
            {
                Id = userVM.Id,
                Email = userVM.Email,
                Name = userVM.Name,
                Surname = userVM.Surname,
                Patronymic = userVM.Patronymic,
                Phone = userVM.Phone,
                Address = userVM.Address,
            };
        }

        public static CartItemViewModel ToCartItemViewModel(this CartItem cartItemDb)
        {
            return new CartItemViewModel
            {
                Id = cartItemDb.Id,
                Amount = cartItemDb.Amount,
                Product = cartItemDb.Product.ToProductViewModel()
            };
        }
    }
}
