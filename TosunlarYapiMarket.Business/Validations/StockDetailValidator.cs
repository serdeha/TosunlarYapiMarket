using FluentValidation;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Validations
{
    public class StockDetailValidator:AbstractValidator<StockDetail>
    {
        public StockDetailValidator()
        {
            RuleFor(x => x.StockDetailName).NotEmpty().WithMessage("Lütfen bir ürün detay ismi giriniz.");
        }
    }
}
