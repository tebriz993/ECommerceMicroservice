using AutoMapper;
using Identity.Application.Dtos.Auth;
using Identity.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            var command = _mapper.Map<RegisterUserCommand>(registerDto);
            var result = await _mediator.Send(command);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Registration successful. Please check your email to confirm your account." });
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var command = _mapper.Map<LoginUserCommand>(loginDto);
            var authResponse = await _mediator.Send(command);
            return Ok(authResponse);
        }

        [HttpGet("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] Guid userId, [FromQuery] string token)
        {
            if (userId == Guid.Empty || string.IsNullOrEmpty(token))
            {
                return BadRequest("Invalid confirmation request parameters.");
            }

            var command = new ConfirmEmailCommand { UserId = userId, Token = token };
            var result = await _mediator.Send(command);

            if (result)
            {
                // You can redirect to a "Success" page on your frontend here
                return Ok("Email confirmed successfully. You can now log in.");
            }

            return BadRequest("Email could not be confirmed. The link may have expired or is invalid.");
        }
    }
}