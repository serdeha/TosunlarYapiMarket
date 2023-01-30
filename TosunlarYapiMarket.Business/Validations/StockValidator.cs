using FluentValidation;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Validations
{
    public class StockValidator:AbstractValidator<Stock>
    {
        public StockValidator()
        {
            RuleFor(x => x.StockName).NotEmpty().WithMessage("Lütfen ürün ismini seçiniz.");
            RuleFor(x => x.KDV).NotNull().WithMessage("Lütfen kdv tutarını seçiniz.");
            RuleFor(x => x.Price).NotNull().WithMessage("Lütfen ürün tutarını seçiniz.");
        }
    }
}
