using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Assignment4.Models;

namespace Assignment4.Controllers
{
    public class Manager
    {
        private DataContext ds = new DataContext();

        public Manager()
        {

        }

        public IEnumerable<InvoiceBase> InvoiceGetAll()
        {
            return Mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBase>>(ds.Invoices.OrderByDescending(x => x.InvoiceId));
        }

        public InvoiceBase InvoiceGetOne(int id)
        {
            var o = ds.Invoices.Find(id);
            return (o == null) ? null : Mapper.Map<Invoice, InvoiceBase>(o);
        }

        public InvoiceWithDetail InvoiceGetByIdWithDetail(int id)
        {
            var o = ds.Invoices
                      .Include("Customer.Employee")
                      .Include("InvoiceLines.Track.Album.Artist")
                      .Include("InvoiceLines.Track.MediaType")
                      .SingleOrDefault(t => t.InvoiceId == id);

            return (o == null) ? null : Mapper.Map<Invoice, InvoiceWithDetail>(o);
        }
    }
}