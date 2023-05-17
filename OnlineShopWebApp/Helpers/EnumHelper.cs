using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace OnlineShopWebApp.Helpers
{
    public class EnumHelper
    {
        public static string GetDisplayName(Enum value)
        {
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            return value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .GetName();
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
        }
    }
}
