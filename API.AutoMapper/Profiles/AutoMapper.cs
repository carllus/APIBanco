using AutoMapper;
using API.AutoMapper.Dto;
using API.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.AutoMapper.Profiles
{
    public static class AutoMapperConfig
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteDto, Cliente>().ReverseMap();
                cfg.CreateMap<EmprestimoDto, Emprestimo>().ReverseMap();
                cfg.CreateMap<TipoEmprestimoDto, TipoEmprestimo>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
