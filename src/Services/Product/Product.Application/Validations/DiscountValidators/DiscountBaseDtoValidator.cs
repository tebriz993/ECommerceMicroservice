using FluentValidation;
using Product.Application.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Validations.DiscountValidators
{
    public class DiscountBaseDtoValidator : AbstractValidator<DiscountBaseDto>
    {
        public DiscountBaseDtoValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Discount name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(v => v.DiscountValue)
                .GreaterThan(0).WithMessage("Discount value must be greater than 0.");

            RuleFor(v => v.DiscountType)
                .IsInEnum().WithMessage("A valid discount type must be specified.");

            RuleFor(v => v.EndDate)
                .GreaterThan(v => v.StartDate)
                .WithMessage("End date must be after the start date.");
        }
    }
}
