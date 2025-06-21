using MediatR;
using Product.Application.Dtos.Category;
using System;
using System.Collections.Generic;

namespace Product.Application.Features.Categories.Queries;

// Cavab olaraq bir siyahı (IReadOnlyList<CategoryDto>) gözləyirik.
public class GetChildCategoriesQuery : IRequest<IReadOnlyList<CategoryDto>>
{
    // Hansı kateqoriyanın alt kateqoriyalarını istədiyimizi bildirir.
    // Guid? (nullable) olması o deməkdir ki, əgər bu parametr göndərilməzsə (null olarsa),
    // biz ana (root) kateqoriyaları gətirə bilərik.
    public Guid? ParentId { get; set; }

    // Datanı dəyişmək niyyətimiz olmadığı üçün default olaraq `false` təyin edirik.
    public bool TrackChanges { get; set; } = false;
}