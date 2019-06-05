using System;
using AutoMapper;
using DocIT.Core.Data.Models;
using DocIT.Core.Data.Payloads;
using DocIT.Core.Data.ViewModels;

namespace DocIT.Core
{
    public static class AutoMapperConfig
    {
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        public static void RegisterMappings()
        {
           
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<GitConnectionConfig, GitConfigPayload>().ReverseMap();
                cfg.CreateMap<GitConnectionConfig, GitConfigViewModel>().ReverseMap();


            });
        }


        public static void Clear()
        {
            Mapper.Reset();
        }
    }
}
