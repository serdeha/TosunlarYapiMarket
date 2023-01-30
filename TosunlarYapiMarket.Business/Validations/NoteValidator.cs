using FluentValidation;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.Business.Validations
{
    public class NoteValidator:AbstractValidator<Note>
    {
        public NoteValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithErrorCode("10").WithMessage("Lütfen bir müşteri seçiniz.");
            RuleFor(x => x.NoteTitle).NotEmpty().WithMessage("Lütfen bir not başlığı giriniz.");
            RuleFor(x => x.NoteDescription).NotEmpty().WithMessage("Lütfen bir not açıklaması giriniz.");
        }
    }
}
