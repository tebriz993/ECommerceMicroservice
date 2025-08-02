using FluentValidation;
using Product.Application.Dtos.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Validations.DiscountValidators
{
    public class CreateDiscountDtoValidator : AbstractValidator<CreateDiscountDto>
    {
        public CreateDiscountDtoValidator()
        {
            // Apply the common rules by including the base validator
            RuleFor(x => x).SetValidator(new DiscountBaseDtoValidator());

            // You can add rules here that are specific ONLY to creating, if any.
        }
    }
}
