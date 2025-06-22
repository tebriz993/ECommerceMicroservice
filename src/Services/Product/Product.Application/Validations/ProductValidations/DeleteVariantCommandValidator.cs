using FluentValidation;
using Product.Application.Features.Products.Commands;
using System;

namespace Product.Application.Validations.ProductValidations
{
    public class DeleteVariantCommandValidator : AbstractValidator<DeleteVariantCommand>
    {
        public DeleteVariantCommandValidator()
        {
            // VariantId-nin Guid-in default dəyəri (000...-000) olmamasını yoxlayırıq.
            // .NotEmpty() metodu Guid üçün bu yoxlamanı avtomatik edir.
            RuleFor(v => v.VariantId)
                .NotEmpty().WithMessage("Variant ID is required to perform a delete operation.");
        }
    }
}