using ShopModule.Converters;
using System.Text;

namespace ShopModule.Models
{
    public static class StaticData
    {
        // Module Url's
        public static string urlDeliveryModule = "http://host.docker.internal:44385/";
        public static string urlClientModule = "https://client.mini-delivery.com/";

        public static IVisitor defaultConverter => new MessageConverter();
        public static string regexCurrency = @"^[A-Z]{3}$";

        // TaxRate
        public static int minTaxRate = 0;
        public static int maxTaxRate = 10;

        public static string urlRequestPickup
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                return sb.Append(urlClientModule).Append("/requestPickup").ToString();
            }
        }

        public static string UrlCourierStatus(string courierId)
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append(urlClientModule).Append("/status/").Append(courierId).ToString();
        }
    }
}
