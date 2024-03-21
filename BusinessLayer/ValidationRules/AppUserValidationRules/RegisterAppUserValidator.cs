using DTO.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class RegisterAppUserValidator : AbstractValidator<RegisterAppUserDto>
    {
        public RegisterAppUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("İsim Alanı Zorunludur.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyisim Alanı Zorunludur");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail Alanı Zorunludur");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıc Adı Alanı Zorunludur");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Adı Alanı Zorunludur");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre Tekrar Adı Alanı Zorunludur");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Şifreler Eşleşmiyor");
        }
    }
}
