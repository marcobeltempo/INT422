using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment3.Controllers
{
    public class EmployeeController : Controller
    {
        private Manager man = new Manager();
        // GET: Employee
        public ActionResult Index()
        {
            return View(man.EmployeeGetAll());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            // Attempt to fetch the matching object
            var o = man.EmployeeGetOne(id.GetValueOrDefault());

            if (o == null)
            {
                return HttpNotFound();
            }
            else
            {
                var editForm = Mapper.Map<EmployeeBase, EmployeeEditContactInfoForm>(o);
                return View(editForm);
            }
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, EmployeeEditContactInfo empInfo)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = empInfo.EmployeeId });

            }
            if (id.GetValueOrDefault() != empInfo.EmployeeId)
            {
                return RedirectToAction("Index");
            }

            var editedEmp = man.EmployeeEditContactInfo(empInfo);

            if (editedEmp == null)
            {
                return RedirectToAction("Edit", new { id = empInfo.EmployeeId });

            }
            else
            {
                return RedirectToAction("Index", new { id = empInfo.EmployeeId });
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
