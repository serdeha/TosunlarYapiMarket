namespace TosunlarYapiMarket.Core.Extensions
{
    public static class CalculateTotalPrice
    {
        public static decimal Calculate(decimal stockPrice, decimal kdv, decimal stockQuantity)
        {
            decimal totalStockPrice = stockPrice * stockQuantity;
            return totalStockPrice * (1 + kdv / 100);
        }
    }
}
