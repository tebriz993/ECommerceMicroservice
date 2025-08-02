using FluentValidation;
using Product.Application.Dtos.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Validations.DiscountValidators
{
    public class UpdateDiscountDtoValidator : AbstractValidator<UpdateDiscountDto>
    {
        public UpdateDiscountDtoValidator()
        {
            RuleFor(x => x).SetValidator(new DiscountBaseDtoValidator());
        }
    }
}
