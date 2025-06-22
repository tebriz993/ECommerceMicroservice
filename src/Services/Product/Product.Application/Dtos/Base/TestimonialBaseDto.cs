namespace Product.Application.Dtos.Base
{
    public abstract class TestimonialBaseDto
    {
        public string ClientName { get; set; }
        public string Profession { get; set; }
        public string ImageUrl { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public bool IsActive { get; set; }
    }
}