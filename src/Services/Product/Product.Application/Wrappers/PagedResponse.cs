using Product.Application.Wrappers.Base;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Product.Application.Wrappers
{
    /// <summary>
    /// Represents a standardized API response for paginated data.
    /// Inherits from the base Response class.
    /// </summary>
    /// <typeparam name="T">The type of the data list being returned.</typeparam>
    public class PagedResponse<T> : Response<T>
    {
        /// <summary>
        /// The current page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The total number of pages available.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// The total number of records matching the query.
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Creates a new PagedResponse instance.
        /// </summary>
        /// <param name="data">The list of items for the current page.</param>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <param name="totalRecords">The total number of records available.</param>
        public PagedResponse(T data, int pageNumber, int pageSize, int totalRecords)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = totalRecords;
            TotalPages = pageSize > 0 ? (int)Math.Ceiling(totalRecords / (double)pageSize) : 0;
            Data = data;
            Succeeded = true;
            Message = null;
        }
    }
}