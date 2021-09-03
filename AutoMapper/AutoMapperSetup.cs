using ApiUser.Dtos;
using ApiUser.Entities;
using AutoMapper;

namespace ApiUser.AutoMapper{

    public class AutoMapperSetup : Profile{

        public AutoMapperSetup(){

            CreateMap<User,UserReadDto>();
            CreateMap<UserCreateDto,User>();
            CreateMap<UserUpdateDto,User>();
            CreateMap<User,UserUpdateDto>();

        }

    }
}