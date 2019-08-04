using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.WebApi
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserLoginDTO>();
            CreateMap<User, UserRegisterDTO>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserLoginDTO, User>();
        }
    }
}
