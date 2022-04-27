using Microsoft.AspNetCore.Mvc;

namespace ShopModule.Models
{
    public class ResponseMessage
    {
        public static JsonResult Success<T>(T element, int code)
        {
            var res = new JsonResult(element);
            res.StatusCode = code;
            return res;
        }

        public static JsonResult Error(string message, int code)
        {
            var res = new JsonResult(message);
            res.StatusCode = code;
            return res;
        }
    }
}
