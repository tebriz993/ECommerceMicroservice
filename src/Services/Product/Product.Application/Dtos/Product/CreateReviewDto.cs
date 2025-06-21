using System;
using System.ComponentModel.DataAnnotations;

namespace Product.Application.Dtos.Review
{
    public class CreateReviewDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}