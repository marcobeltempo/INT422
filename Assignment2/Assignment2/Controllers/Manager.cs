using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Assignment2.Models;

namespace Assignment2.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        public Manager()
        {

            // If necessary, add constructor code here
        }

        public IEnumerable<EmployeeBase_vm> EmployeeGetAll()
        {

            return Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBase_vm>>(ds.Employees);
        }


        public EmployeeBase_vm EmployeeGetOne(int id)
        {
            var o = ds.Employees.Find(id);

            return (o == null) ? null : Mapper.Map<Employee, EmployeeBase_vm>(o);
        }



        public EmployeeBase_vm EmployeeAdd(EmployeeAdd_vm newEmployee)
        {
            // Attempt to add the new item
            var addedItem = ds.Employees.Add(Mapper.Map<EmployeeAdd_vm, Employee>(newEmployee));
            ds.SaveChanges();

            // If successful, return the added item, mapped to a view model object
            return (addedItem == null) ? null : Mapper.Map<Employee, EmployeeBase_vm>(addedItem);
        }
    }
}