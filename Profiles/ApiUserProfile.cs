using ApiUser.Dtos;
using ApiUser.Models;
using AutoMapper;

namespace ApiUser.Profiles{

    public class ApiUserProfile : Profile{

        public ApiUserProfile(){

            CreateMap<User,UserReadDto>();
            CreateMap<UserCreateDto,User>();
            CreateMap<UserUpdateDto,User>();
            CreateMap<User,UserUpdateDto>();

        }

    }
}