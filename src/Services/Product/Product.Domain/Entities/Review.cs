using Product.Domain.Common;

namespace Product.Domain.Entities
{

    public class Review : EntityBase
    {
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
        public Guid UserId { get; set; } // Reference to Identity service
        public string UserName { get; set; }
        public int Rating { get; set; } // 1-5
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}