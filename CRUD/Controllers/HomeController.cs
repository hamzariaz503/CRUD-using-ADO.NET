using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using CRUD.Models;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            try
            {
                EmployeeDbContext db = new EmployeeDbContext();
                List<Employee> obj = db.GetEmployees();
                return View(obj);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error occurred while retrieving employees: " + ex.Message);
                
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeDbContext context = new EmployeeDbContext();

                    bool check = context.AddEmployee(emp);
                    if (check)
                    {
                        TempData["InsertMessage"] = "Data inserted successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to insert data");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error occurred while adding employee: " + ex.Message);
               
                return View("Error");
            }
        }

        public ActionResult Edit(int id)
        {
            EmployeeDbContext context = new EmployeeDbContext();
            var row = context.GetEmployees().Find(model => model.id == id);
            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(int id,Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeDbContext context = new EmployeeDbContext();

                    bool check = context.UpdateEmployee(emp);
                    if (check)
                    {
                        TempData["UpdateMessage"] = "Data Upadte successfully";
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Failed to update data");
                    }
                }
             
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error occurred while updating employee: " + ex.Message);

                return View("Error");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            EmployeeDbContext context = new EmployeeDbContext();
            var row = context.GetEmployees().Find(model => model.id == id);
            return View(row);
        }
        public ActionResult Delete(int id)
        {
            EmployeeDbContext context = new EmployeeDbContext();
            var row = context.GetEmployees().Find(model => model.id == id);
            return View(row);
        }
        [HttpPost]
        public ActionResult Delete(Employee emp,int id)
        {
            EmployeeDbContext context = new EmployeeDbContext();

            bool check = context.DeleteEmployee(id);
            if (check)
            {
                TempData["DeleteMessage"] = "Data Delete successfully";
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to delete data");
            }

            return View();
        }
    }
}
