using MediatR;
using System;

namespace Product.Application.Features.Categories.Commands;

// Bu Command heç bir nəticə qaytarmadığı üçün IRequest istifadə edir.
public class UpdateCategoryStatusCommand : IRequest
{
    // Hansı kateqoriyanın dəyişdiriləcəyini bildirir.
    public Guid Id { get; set; }

    // Yeni statusu bildirir (true = aktiv, false = passiv).
    public bool IsActive { get; set; }
}