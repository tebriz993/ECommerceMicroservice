using Product.Application.Interfaces.Base;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Interfaces
{
    public interface IReviewRepository:IRepositoryBase<Review>
    {
        Task<IReadOnlyList<Review>> GetReviewsByProductIdAsync(Guid productId, bool trackChanges = false);
    }
}

