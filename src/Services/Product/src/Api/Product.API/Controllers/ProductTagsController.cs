using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Controllers.Base;
using Product.Application.Dtos.ProductTags;
using Product.Application.Features.ProductTags.Commands;
using Product.Application.Features.ProductTags.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    // All endpoints will start with /api/v1/ProductTags
    public class ProductTagsController : BaseApiController
    {
        // --- QUERIES ---

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ProductTagWithProductCountDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTags()
        {
            // This query is designed for an admin list view, showing which tags are used most.
            return Ok(await Mediator.Send(new GetAllTagsWithProductCountQuery()));
        }

        [HttpGet("product/{productId}")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductTagDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTagsForProduct(Guid productId)
        {
            // This is useful for the client-side to display the tags on a product detail page.
            return Ok(await Mediator.Send(new GetTagsByProductIdQuery { ProductId = productId }));
        }

        // --- COMMANDS ---

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTag([FromBody] CreateProductTagDto createDto)
        {
            var command = new CreateProductTagCommand { CreateTagDto = createDto };
            var newId = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new { id = newId });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTag(Guid id, [FromBody] UpdateProductTagDto updateDto)
        {
            var command = new UpdateProductTagCommand { Id = id, UpdateTagDto = updateDto };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            await Mediator.Send(new DeleteProductTagCommand { Id = id });
            return NoContent();
        }
    }
}