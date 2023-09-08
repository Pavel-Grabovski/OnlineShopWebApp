using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Linq;

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

        //public static CartViewModel ToCartViewModel(this Cart cartDB)
        //{
        //    var cartItemsVM = new List<CartItemViewModel>();

        //    foreach (var cartItem in cartDB.CartItems)
        //    {
        //        var cartItemVM = new CartItemViewModel
        //        {
        //            Id = cartItem.Id,
        //            Product = new ProductViewModel
        //            {
        //                Id = cartItem.Product.Id,
        //                Name = cartItem.Product.Name,
        //                Description = cartItem.Product.Description,
        //                Cost = cartItem.Product.Cost,
        //                ImagePath = cartItem.Product.ImagePath
        //            },
        //            Amount = cartItem.Amount
        //        };
        //        cartItemsVM.Add(cartItemVM);
        //    }


        //    var cartVM = new CartViewModel
        //    {
        //        Id = cartDB.Id,
        //        UserId = cartDB.UserId,
        //        Items = cartItemsVM
        //    };

        //    return cartVM;
        //}



        //public static OrderViewModel ToOrderViewModel(this Order orderDb)
        //{
        //    return new OrderViewModel
        //    {
        //        Id = orderDb.Id,
        //        CreateDateTime = orderDb.CreateDateTime,
        //        UserDeliveryInfo = orderDb.UserDeliveryInfo.ToUserDeliveryInfoViewModel(),
        //        Items = orderDb.Items.Select(x => x.ToCartItemViewModel()).ToList(),
        //        Status = (OrderStatusViewModel)(int)orderDb.Status
        //    };
        //}

        //public static UserDeliveryInfoViewModel ToUserDeliveryInfoViewModel(this UserDeliveryInfo userDb)
        //{
        //    return new UserDeliveryInfoViewModel
        //    {
        //        Address = userDb.Address,
        //        City = userDb.City,
        //        Comments = userDb.Comments,
        //        FirstName = userDb.FirstName,
        //        LastName = userDb.LastName,
        //        Phone = userDb.Phone,
        //        ZipCode = userDb.ZipCode
        //    };
        //}

        //public static UserDeliveryInfo ToUserDeliveryInfo(this UserDeliveryInfoViewModel user)
        //{
        //    return new UserDeliveryInfo
        //    {
        //        Address = user.Address,
        //        City = user.City,
        //        Comments = user.Comments,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Phone = user.Phone,
        //        ZipCode = user.ZipCode
        //    };
        //}

        //public static CartItemViewModel ToCartItemViewModel(this CartItem cartItemDb)
        //{
        //    return new CartItemViewModel
        //    {
        //        Id = cartItemDb.Id,
        //        Amount = cartItemDb.Amount,
        //        Product = cartItemDb.Product.ToProductViewModel()
        //    };
        //}
    }
}
