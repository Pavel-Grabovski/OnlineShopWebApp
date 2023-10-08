using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Admin.Models;
using OnlineShopWebApp.Models;
using System.Linq;
using System.Net;

namespace OnlineShopWebApp.Helpers
{
    public static class Mapping
    {
        public static EditProductViewModel ToEditProductViewModel(this Product product)
        {
            return new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagesPaths = product.Images.ToPaths().Where(str => Directory.Exists(str)).ToList(),
            };
        }
        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImagesPaths = product.Images.Where(img => Directory.Exists(img.Url)).Select(x => x.Url).ToArray(),
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

        public static Product ToProduct(this AddProductViewModel addProductViewModel, ICollection<string> imagesPaths)
        {
            return new Product
            {
                Name = addProductViewModel.Name,
                Cost = addProductViewModel.Cost,
                Description = addProductViewModel.Description,
                Images = ToImages(imagesPaths).ToList()
            };
        }
        public static Product ToProduct(this EditProductViewModel editProductViewModel)
        {
            return new Product
            {
                Id = editProductViewModel.Id,
                Name = editProductViewModel.Name,
                Cost = editProductViewModel.Cost,
                Description = editProductViewModel.Description,
                Images = editProductViewModel?.ImagesPaths?.ToImages()?.ToList(),
            };
        }

        public static ICollection<Image> ToImages(this ICollection<string> paths)
        {
            if(paths == null || paths.Count == 0)
            {
                return null;
            }
            return paths.Select(x => new Image { Url = x }).ToList();
        }

        public static ICollection<string> ToPaths(this ICollection<Image> images)
        {
            return images.Select(x => x.Url).ToList();
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
                        //ImagePath = cartItem.Product.ImagePath
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

        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                Phone = user.PhoneNumber,

                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic
            };
        }
    }
}
