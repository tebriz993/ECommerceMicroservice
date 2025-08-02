namespace Product.Domain.Common
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
    }
}