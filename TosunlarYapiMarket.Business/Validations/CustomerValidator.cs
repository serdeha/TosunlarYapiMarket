using FluentValidation;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Validations
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithErrorCode("1").WithMessage("Müşteri adı boş bırakılamaz.");
            RuleFor(x => x.LastName).NotEmpty().WithErrorCode("2").WithMessage("Müşteri soyadı boş bırakılamaz.");
            RuleFor(x => x.CustomerNo).NotEmpty().WithErrorCode("3").WithMessage("Müşteri numarası boş bırakılamaz.");
        }
    }
}
