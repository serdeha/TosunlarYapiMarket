using FluentValidation;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Validations
{
    public class DebtValidator:AbstractValidator<Debt>
    {
        public DebtValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithErrorCode("4").WithMessage("Lütfen bir müşteri seçiniz.");
            RuleFor(x=>x.PaymentAmount).NotEmpty().WithErrorCode("5").WithMessage("Lütfen bir ödeme tutarı giriniz.");
            RuleFor(x=>x.PaymentDate).NotEmpty().WithErrorCode("6").WithMessage("Lütfen bir ödeme tarihi seçiniz.");
        }
    }
}
