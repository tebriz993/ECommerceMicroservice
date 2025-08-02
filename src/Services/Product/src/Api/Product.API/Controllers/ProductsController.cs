using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Controllers.Base;
using Product.Application.Dtos.Product;
using Product.Application.Dtos.Review;
using Product.Application.Features.Products.Commands;
using Product.Application.Features.Products.Queries;
using Product.Application.Features.Reviews.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        #region Product Queries

        [HttpGet]
        [ProducesResponseType(typeof(Application.Wrappers.PagedResponse<IReadOnlyList<ProductDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsByPageQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductDetailsByIdQuery { Id = id }));
        }

        #endregion

        #region Product Commands

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createDto)
        {
            var command = new CreateProductCommand { CreateProductDto = createDto };
            var newId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = newId }, new { id = newId });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateDto)
        {
            var command = new UpdateProductCommand { Id = id, UpdateProductDto = updateDto };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await Mediator.Send(new DeleteProductCommand { Id = id });
            return NoContent();
        }

        #endregion

        #region Product Image Commands

        [HttpPost("{productId}/images")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddImagesToProduct(Guid productId, [FromBody] List<CreateProductImageDto> images)
        {
            var command = new AddImagesToProductCommand { ProductId = productId, Images = images };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductImage(Guid imageId, [FromBody] UpdateProductImageDto updateDto)
        {
            var command = new UpdateProductImageCommand { ImageId = imageId, UpdateDto = updateDto };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("images/{imageId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProductImage(Guid imageId)
        {
            await Mediator.Send(new DeleteProductImageCommand { ImageId = imageId });
            return NoContent();
        }

        #endregion

        #region Product Variant Commands

        [HttpPost("{productId}/variants")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddVariantsToProduct(Guid productId, [FromBody] List<CreateProductVariantDto> variants)
        {
            var command = new AddVariantsToProductCommand { ProductId = productId, Variants = variants };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("variants/{variantId}/stock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateVariantStock(Guid variantId, [FromBody] UpdateStockDto stockDto)
        {
            var command = new UpdateVariantStockCommand { VariantId = variantId, QuantityChange = stockDto.QuantityChange };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("variants/{variantId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVariant(Guid variantId)
        {
            await Mediator.Send(new DeleteVariantCommand { VariantId = variantId });
            return NoContent();
        }

        #endregion

        #region Product Review Commands

        [HttpPost("{productId}/reviews")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateReview(Guid productId, [FromBody] CreateReviewDto reviewDto)
        {
            // Təhlükəsizlik və məntiq üçün ProductId həm URL-dən, həm də body-dən gələ bilər.
            // Onların eyni olduğunu yoxlamaq olar. Biz sadəlik üçün DTO-ya əlavə edirik.
            reviewDto.ProductId = productId;
            var command = new CreateReviewCommand { ReviewDto = reviewDto };
            var newId = await Mediator.Send(command);
            return StatusCode(StatusCodes.Status201Created, new { id = newId });
        }

        #endregion
    }
}