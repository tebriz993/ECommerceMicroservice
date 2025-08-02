using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Controllers.Base;
using Product.Application.Dtos.Discount;
using Product.Application.Features.Discount.Commands;
using Product.Application.Features.Discount.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    // Bütün endpoint-lər /api/v1/Discounts ilə başlayacaq
    public class DiscountsController : BaseApiController
    {
        // --- QUERIES (Data Gətirmək) ---

        [HttpGet]
        [ProducesResponseType(typeof(Application.Wrappers.PagedResponse<IReadOnlyList<DiscountDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetDiscountsByPageQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("active")]
        [ProducesResponseType(typeof(IReadOnlyList<DiscountDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveDiscounts()
        {
            var result = await Mediator.Send(new GetActiveDiscountsQuery());
            return Ok(result);
        }

        // Qeyd: GetDiscountDetailsByIdQuery və onun handler-i hələ yazılmayıb.
        // Yazıldıqdan sonra bu endpoint aktivləşdirilə bilər.

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DiscountDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await Mediator.Send(new GetDiscountDetailsByIdQuery { Id = id });
            return Ok(result);
        }


        // --- COMMANDS (Data Yaratmaq və Dəyişmək) ---

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateDiscountDto createDto)
        {
            var command = new CreateDiscountCommand { DiscountDto = createDto };
            var newId = await Mediator.Send(command);
            // GetById endpoint-i hələ olmadığı üçün sadəcə ID-ni qaytarırıq.
            return StatusCode(StatusCodes.Status201Created, new { id = newId });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateDiscountDto updateDto)
        {
            var command = new UpdateDiscountCommand { Id = id, DiscountDto = updateDto };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchStatus(Guid id, [FromBody] UpdateDiscountStatusDto statusDto)
        {
            var command = new UpdateDiscountStatusCommand { Id = id, IsActive = statusDto.IsActive };
            await Mediator.Send(command);
            return NoContent();
        }
        // Qeyd: UpdateDiscountStatusDto-nu yaratmaq lazımdır.
        // public class UpdateDiscountStatusDto { public bool IsActive { get; set; } }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteDiscountCommand { Id = id });
            return NoContent();
        }
    }
}