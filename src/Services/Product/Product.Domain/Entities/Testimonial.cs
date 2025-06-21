using Product.Domain.Common;

namespace Product.Domain.Entities
{
    /// <summary>
    /// Represents a customer testimonial about the website or service.
    /// </summary>
    public class Testimonial : EntityBase
    {
        /// <summary>
        /// The name of the client giving the testimonial.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// The client's profession or title (e.g., "Designer").
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        /// The URL of the client's profile picture.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// The actual testimonial text.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The rating given by the client (e.g., 1-5 stars).
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Controls whether the testimonial is active and displayed on the site.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}