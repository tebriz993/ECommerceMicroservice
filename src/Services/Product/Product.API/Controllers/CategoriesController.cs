using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Controllers.Base;
using Product.Application.Dtos.Category;
using Product.Application.Features.Categories.Commands;
using Product.Application.Features.Categories.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        // --- QUERIES (Data Gətirmək) ---

        [HttpGet]
        [ProducesResponseType(typeof(Application.Wrappers.PagedResponse<IReadOnlyList<CategoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPage([FromQuery] GetCategoriesByPageQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryWithProductsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpGet("tree")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryTreeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTree()
        {
            return Ok(await Mediator.Send(new GetCategoryTreeQuery()));
        }

        [HttpGet("children")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetChildren([FromQuery] Guid? parentId)
        {
            return Ok(await Mediator.Send(new GetChildCategoriesQuery { ParentId = parentId }));
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryListItemDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] SearchCategoriesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        // GetAllCategoriesQuery-ni ləğv etmək olar, çünki GetByPage daha güclüdür.
        // Amma lazım olarsa, belə bir endpoint də saxlamaq olar:
        [HttpGet("all")]
        [ProducesResponseType(typeof(IReadOnlyList<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        // --- COMMANDS (Data Dəyişmək) ---

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto createDto)
        {
            var command = new CreateCategoryCommand { CreateCategoryDto = createDto };
            var newId = await Mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = newId }, new { id = newId });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto updateDto)
        {
            var command = new UpdateCategoryCommand { Id = id, UpdateCategoryDto = updateDto };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateCategoryStatusDto statusDto)
        {
            var command = new UpdateCategoryStatusCommand { Id = id, IsActive = statusDto.IsActive };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("display-order")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateDisplayOrder([FromBody] Dictionary<Guid, int> orders)
        {
            var command = new UpdateCategoryDisplayOrderCommand { CategoryOrders = orders };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });
            return NoContent();
        }
    }
}