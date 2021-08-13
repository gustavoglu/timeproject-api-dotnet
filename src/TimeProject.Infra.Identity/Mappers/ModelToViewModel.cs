using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Infra.Identity.Models;
using TimeProject.Infra.Identity.ViewModels;

namespace TimeProject.Infra.Identity.Mappers
{
    public class ModelToViewModel : Profile
    {
        public ModelToViewModel()
        {
            CreateMap<User, UserSimpleViewModel>();
            CreateMap<User, UserSimpleViewModel>().ReverseMap();

            CreateMap<User, UserSelectViewModel>();
            CreateMap<User, UserSelectViewModel>().ReverseMap();

        }
    }
}
