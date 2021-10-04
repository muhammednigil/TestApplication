using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TestApplicationDomain.DTO;

namespace TestApplicationDomain.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().WithErrorCode("TA_ERR_11").WithMessage("The email should not be empty");
            RuleFor(x => x.Email).Must((request, dateTime, x) => isEmailValid(request.Email)).WithErrorCode("TA_ERR_12").WithMessage("The email should be valid");
        }

        private bool isEmailValid(string email)
        {
            return !string.IsNullOrWhiteSpace(email) || BaseValidation.IsValidEmail(email);
        }
    }
}
