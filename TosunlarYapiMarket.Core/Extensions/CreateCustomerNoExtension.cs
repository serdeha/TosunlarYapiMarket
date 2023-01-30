using TosunlarYapiMarket.Business.Abstract;

namespace TosunlarYapiMarket.Core.Extensions
{
    public static class CreateCustomerNoExtension
    {
        public static string GetCustomerNo()
        {
            string customerNo = Guid.NewGuid().ToString("N");
            var newCustomer = customerNo.Substring(0, 10);
            return $"TSN{newCustomer}".ToUpper();
        }
    }
}
