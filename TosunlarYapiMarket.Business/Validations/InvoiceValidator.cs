using FluentValidation;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Validations
{
    public class InvoiceValidator:AbstractValidator<Invoice>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithErrorCode("7").WithMessage("Lütfen bir müşteri seçiniz.");
            RuleFor(x => x.InvoiceCode).NotEmpty().WithErrorCode("9").WithMessage("Lütfen bir fatura ismi seçiniz.");
        }
    }
}
