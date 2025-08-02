using AutoMapper;
using Identity.Application.Dtos.Auth;
using Identity.Application.Features.Auth.Commands;
using Identity.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Identity.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequestDto, RegisterUserCommand>();
            CreateMap<LoginRequestDto, LoginUserCommand>();
            CreateMap<RegisterUserCommand, AppUser>();
        }
    }
}