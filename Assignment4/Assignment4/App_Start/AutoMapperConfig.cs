using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Assignment4
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
                cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithDetail>();
                cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineBase>();
                cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineWithDetail>();
            });
        }
    }
}
