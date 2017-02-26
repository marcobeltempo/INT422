using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment2.Controllers
{
    public class EmployeesController : Controller
    {
        private Manager man = new Manager();
        // GET: Employees
        public ActionResult Index()
        {
            return View(man.EmployeeGetAll());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {

            // Attempt to get the matching object
            var o = man.EmployeeGetOne(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);

            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create

        [HttpPost]

        public ActionResult Create(EmployeeAdd_vm newEmployee)
        {

            // Validate the input
            if (!ModelState.IsValid)

            {
               return View(newEmployee);
            }

            // Process the input
            var addedItem = man.EmployeeAdd(newEmployee);

            if (addedItem == null)
            {
                return View(newEmployee);
            }
            else
            {
                return RedirectToAction("details", new { id = addedItem.EmployeeId });
            }

        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}


/* 
 
        public ActionResult Details(int? id)
        {
            // Attempt to get the matching object
            var o = m.CustomerGetById(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                // Pass the object to the view
                return View(o);
            }
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            // At your option, create and send an object to the view
            return View();
        }

     
     
     */
