using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvcApplication.Controllers
{
    public class HomeController : Controller
    {

        public DataAccess dataAccess;

        public HomeController() {

            string connStr = ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString;
            dataAccess = new DataAccess(connStr);

        }
        public ActionResult Index()
        {
            try
            {
                var employees = dataAccess.GetAllEmployee();
                return View(employees); // This should now never be null
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework here)
                return View("Error", new HandleErrorInfo(ex, "Home", "Index"));
            }
        }

        // public ActionResult Details(int id)
        // {

        //     try
        //     {
        //         var employees = dataAccess.GetEmployeeById(id);
        //         if (employees == null)
        //         {
        //             return HttpNotFound();
        //         }
        //         return View(employees);
        //     }
        //     catch (Exception ex) {

        //         return null;
        //     }
        // }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmpOrm emp) {

            try
            {
                if (ModelState.IsValid)
                {

                    dataAccess.AddEmployee(emp);
                    return RedirectToAction("Index");
                }
                return View(emp);
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public ActionResult Details(int id)
        {
            try
            {
                var employee = dataAccess.GetEmployeeById(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        [HttpPost]
        public ActionResult Edit(EmpOrm emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dataAccess.ModifyEmployee(emp);
                    return RedirectToAction("Index");
                }
                return View(emp);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var employee = dataAccess.GetEmployeeById(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                dataAccess.RemoveEmployee(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return null;
            }
        }



    }
}
