using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Controllers.Base;
using Product.Application.Dtos.Testimonial;
using Product.Application.Features.Testimonial.Commands;
using Product.Application.Features.Testimonial.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    public class TestimonialsController : BaseApiController
    {
        // --- QUERIES ---

        [HttpGet("featured")]
        [ProducesResponseType(typeof(IReadOnlyList<TestimonialDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeatured([FromQuery] int count = 4)
        {
            // This is for the public-facing website to display a few testimonials.
            return Ok(await Mediator.Send(new GetFeaturedTestimonialsQuery { Count = count }));
        }

        [HttpGet]
        [ProducesResponseType(typeof(Application.Wrappers.PagedResponse<IReadOnlyList<TestimonialDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaginated([FromQuery] GetTestimonialsByPageQuery query)
        {
            // This is for the admin panel to manage all testimonials.
            return Ok(await Mediator.Send(query));
        }

        // --- COMMANDS ---

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTestimonial([FromBody] CreateTestimonialDto createDto)
        {
            var command = new CreateTestimonialCommand { CreateDto = createDto };
            var newId = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new { id = newId });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTestimonial(Guid id, [FromBody] UpdateTestimonialDto updateDto)
        {
            var command = new UpdateTestimonialCommand { Id = id, UpdateDto = updateDto };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTestimonial(Guid id)
        {
            await Mediator.Send(new DeleteTestimonialCommand { Id = id });
            return NoContent();
        }
    }
}