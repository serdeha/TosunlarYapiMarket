using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TosunlarYapiMarket.Core.ComplexTypes;

namespace TosunlarYapiMarket.WebUI.Areas.Admin.Models
{
    public class StockAddViewModel
    {
        [Required(ErrorMessage = "Lütfen ürün ismini giriniz.")]
        [DisplayName("Ürün İsmi")]
        public string? StockName { get; set; }
        [DisplayName("Ürün Fotoğrafı")]
        public IFormFile? FormFile { get; set; }

        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Lütfen ürün fiyatını giriniz.")]
        [DisplayName("Ürün Fiyatı")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Lütfen kdv oranını giriniz.")]
        [DisplayName("KDV")]
        public decimal Kdv { get; set; }
        [Required(ErrorMessage = "Lütfen ürün miktarını giriniz.")]
        [DisplayName("Stok Detay (Metre/Metrekare/Adet/Ton)")]
        public double StockAnyDetail { get; set; }
        [Required(ErrorMessage = "Lütfen ürün tipini seçiniz.")]
        [DisplayName("Ürün Tipi")]
        public StockType StockType { get; set; }
        [Required(ErrorMessage = "Lütfen ürün detayını seçiniz.")]
        [DisplayName("Ürün Detayı")]
        public int StockDetailId { get; set; } = 0;
        

        //[DisplayName("Metre")]
        //public double Meter { get; set; } = 0;

        //[DisplayName("Metrekare")] 
        //public double SquareMeters { get; set; } = 0;
        //[DisplayName("Ton")]
        //public double Ton { get; set; } = 0;
        //[DisplayName("Adet")]
        //public int Quantity { get; set; } = 0;
    }
}
