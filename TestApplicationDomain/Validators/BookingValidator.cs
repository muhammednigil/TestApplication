using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TestApplicationDomain.DTO;

namespace TestApplicationDomain.Validators
{
    public class BookingValidator : AbstractValidator<BookingDto>
    {
        public BookingValidator()
        {
            RuleFor(x => x.Room).NotEmpty().NotNull().WithErrorCode("TA_ERR_1").WithMessage("There should be atleast one room selected");
            RuleFor(x => x.User).NotEmpty().NotNull().WithErrorCode("TA_ERR_2").WithMessage("There should be user attached with this request");
            RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThan(0).WithErrorCode("TA_ERR_3").WithMessage("The price should be gretaer than zero");
            RuleFor(x => x.DiscountedPrice).NotEmpty().NotNull().GreaterThanOrEqualTo(0).WithErrorCode("TA_ERR_4").WithMessage("The discounted price should be gretaer than or equal to zero");
            RuleFor(x => x.TotalPrice).NotEmpty().NotNull().GreaterThan(0).WithErrorCode("TA_ERR_5").WithMessage("The total price should be gretaer than zero");
            RuleFor(x => x.VATPrice).NotEmpty().NotNull().GreaterThan(0).WithErrorCode("TA_ERR_6").WithMessage("The VAT price should be gretaer than zero");
            RuleFor(x => x.Start).NotEmpty().NotNull().WithErrorCode("TA_ERR_7").WithMessage("The start date should not be empty");
            RuleFor(x => x.End).NotEmpty().NotNull().WithErrorCode("TA_ERR_8").WithMessage("The emd date should not be empty");
            RuleFor(x => x.End).GreaterThanOrEqualTo(x => x.Start).WithErrorCode("TA_ERR_9").WithMessage("The emd date should be greater than start date");
            RuleFor(x => x.AdultGuestCOunt).NotEmpty().NotNull().NotEqual(0).WithErrorCode("TA_ERR_10").WithMessage("There should atleast one adult to continue with the booking");
        }
    }
}
