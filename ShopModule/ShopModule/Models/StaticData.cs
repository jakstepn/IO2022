using System.Text;

namespace ShopModule.Models
{
    public static class StaticData
    {
        public static string urlDeliveryModule = "https://delivery.mini-delivery.com/";
        public static string urlClientModule = "https://client.mini-delivery.com/";

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
