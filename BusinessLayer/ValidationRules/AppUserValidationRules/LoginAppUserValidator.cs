using DTO.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class LoginAppUserValidator : AbstractValidator<LoginAppUserDto>
    {
        public LoginAppUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail Alanı zorunludur");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Alanı Zorunludur.");
        }
    }
}
