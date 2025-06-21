using MediatR;
using Product.Application.Dtos.Product;
using Product.Application.Wrappers;
using System;
using System.Collections.Generic;

namespace Product.Application.Features.Products.Queries
{
    /// <summary>
    /// Represents a query to get a paginated list of products with filtering and sorting.
    /// </summary>
    public class GetProductsByPageQuery : IRequest<PagedResponse<IReadOnlyList<ProductDto>>>
    {
        /// <summary>
        /// The page number to retrieve.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// The number of products per page.
        /// </summary>
        public int PageSize { get; set; } = 9; // Dizaynda 9 məhsul görünür

        /// <summary>
        /// Optional: Filter products by a specific category ID.
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// Optional: Filter products by a search term in their name or description.
        /// </summary>
        public string? SearchTerm { get; set; }

        /// <summary>
        /// Optional: Filter by minimum price.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Optional: Filter by maximum price.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Optional: Filter by a list of tag IDs (e.g., Organic, Fresh).
        /// </summary>
        public List<Guid>? TagIds { get; set; }

        /// <summary>
        /// Optional: The field to sort by (e.g., "Price", "Name", "Rating").
        /// </summary>
        public string? SortBy { get; set; } = "Default"; // "Default Sorting"

        /// <summary>
        /// Optional: The sorting direction.
        /// </summary>
        public bool IsAscending { get; set; } = true;
    }
}