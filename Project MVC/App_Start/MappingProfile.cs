using AutoMapper;
using Project_MVC.Dtos;
using Project_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_MVC.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Trainer, TrainerDto>();
            Mapper.CreateMap<TrainerDto, Trainer>();
        }
    }
}