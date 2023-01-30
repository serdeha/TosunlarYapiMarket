using FluentValidation;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Validations
{
    public class PayOffDebValidator:AbstractValidator<PayOffDebt>
    {
        public PayOffDebValidator()
        {
            RuleFor(x => x.AmountPaid).NotEmpty().WithMessage("Lütfen bir ödeme tutarı giriniz.");
            RuleFor(x => x.PaidDate).NotEmpty().WithMessage("Lütfen bir ödeme tarihi seçiniz.");
        }
    }
}
