using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.MessageValidationRules
{
    public class SendMessageValidator : AbstractValidator<Message>
    {
        public SendMessageValidator()
        {
            RuleFor(x => x.Content).NotEmpty().WithMessage("Lütfen mesajınızı giriniz.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Lütfen konu giriniz.");
            RuleFor(x => x.RecieverMail).NotEmpty().WithMessage("Lütfen Alıcı Mail adresini giriniz.")
                .EmailAddress();
        }
    }
}
