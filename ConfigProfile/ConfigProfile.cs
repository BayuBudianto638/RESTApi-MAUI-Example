using AutoMapper;
using RESTWebApp.Application.Services.EmployeeService.Dto;
using RESTWebApp.Application.Services.UserService.Dto;
using RESTWebApp.Data.Models;
using RESTWebApp.Example.Models;

namespace RESTWebApp.Example.ConfigProfile
{
    public class ConfigProfile : Profile
    {
        public ConfigProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserDto>();

            CreateMap<CreateUserDto, UserModel>();
            CreateMap<UserModel, CreateUserDto>();

            CreateMap<UpdateUserDto, UserModel>();
            CreateMap<UserModel, UpdateUserDto>();

            CreateMap<UserDto, LoginModel>();
            CreateMap<LoginModel, UserDto>();
        }
    }
}
