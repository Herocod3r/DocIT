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
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<GitConnectionConfig, GitConfigPayload>().ReverseMap();
                    cfg.CreateMap<GitConnectionConfig, GitConfigViewModel>().ReverseMap();
                    cfg.CreateMap<User, UpdateAccountPayload>().ReverseMap();
                    cfg.CreateMap<Project, ProjectViewModel>().ReverseMap();
                    cfg.CreateMap<Project, ProjectPayload>().ReverseMap();
                    cfg.CreateMap<ProjectListItem, ProjectViewModel>().ReverseMap();
                    cfg.CreateMap<Project, UpdateProjectPayload>().ReverseMap();
                    cfg.CreateMap<ProjectListItem, Project>().ReverseMap();

                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            
        }


        public static void Clear()
        {
            Mapper.Reset();
        }
    }
}
