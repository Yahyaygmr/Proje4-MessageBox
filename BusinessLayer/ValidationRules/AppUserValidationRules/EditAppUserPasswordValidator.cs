using DTO.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.AppUserValidationRules
{
    public class EditAppUserPasswordValidator: AbstractValidator<EditAppUserPasswordDto>
    {
        public EditAppUserPasswordValidator()
        {
            RuleFor(x=>x.Email).NotEmpty();
            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x=>x.Surname).NotEmpty();
            RuleFor(x=>x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şİfre Alanı Boş Olamaz");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şİfre Alanı Boş Olamaz");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Şifreler Eşleşmiyor");
        }
    }
}
