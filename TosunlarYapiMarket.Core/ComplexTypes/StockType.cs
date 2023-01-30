using System.ComponentModel.DataAnnotations;

namespace TosunlarYapiMarket.Core.ComplexTypes
{
    public enum StockType
    {
        [Display(Name = "Metre")]
        Meter = 1,
        [Display(Name = "Metrekare")]
        SquareMeters = 2,
        [Display(Name = "Ton")]
        Ton = 3,
        [Display(Name = "Adet")]
        Quantity = 4
    }
}
