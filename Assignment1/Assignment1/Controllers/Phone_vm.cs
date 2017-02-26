using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment1.Controllers
{
    public class PhoneBase
    {
        public PhoneBase()
        {
            DateReleased = DateTime.Now;
            PhoneName = "";
            Manufacturer = "";   
        }
        public int Id { get; set; }
        public string PhoneName { get; set; }
        public string Manufacturer { get; set; }
        //DateTime MUST be configured in the constructor
        public DateTime DateReleased { get; set; }
        public int MSRP { get; set; }
        public double ScreenSize { get; set; }
    }
}