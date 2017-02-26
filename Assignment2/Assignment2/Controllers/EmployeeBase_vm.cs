using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment2.Controllers
{
    public class EmployeeBase_vm : EmployeeAdd_vm
    {
        public EmployeeBase_vm()
        {

        }

        [Key]
        public int EmployeeId { get; set; }
    }
}