using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment2
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {

            // AutoMapper create map statements - using AutoMapper static API
            // Mapper.Initialize(cfg => cfg.CreateMap< FROM , TO >());
            // Add map creation statements here
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Controllers.EmployeeAdd_vm, Models.Employee>();
                cfg.CreateMap<Models.Employee, Controllers.EmployeeBase_vm>();
            });

        }
    }
}