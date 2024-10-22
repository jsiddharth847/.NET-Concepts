using FinalCRUDEMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FinalEmpCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly string apiBaseUrl = "https://localhost:44381/api/Employee";


        //public async Task<List<Employee>> GetEmployeesAsync() {
        //    try
        //    {
        //        List<Employee> employees = new List<Employee>();


        //        using (var httpClient = new HttpClient()) {



        //            httpClient.BaseAddress = new Uri(apiBaseUrl);

        //            HttpResponseMessage response = await httpClient.GetAsync("Employee");

        //            if (response.IsSuccessStatusCode)
        //            {

        //                //Deserializing the response content to List<Employee>
        //                employees = await response.Content.ReadAsAsync<List<Employee>>();
        //            }

        //            else {

        //                ModelState.AddModelError(string.Empty, "Error fetching Employee data");
        //            }
        //            }
        //        return employees;




        //        }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return new List<Employee>(); // Return an empty list on error
        //    }
        //}

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(apiBaseUrl);
                    HttpResponseMessage response = await httpClient.GetAsync("Employee");

                    if (response.IsSuccessStatusCode)
                    {
                        // Deserializing the response content to List<Employee>
                        return await response.Content.ReadAsAsync<List<Employee>>();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error fetching Employee data");
                        return new List<Employee>(); // Return an empty list instead of null
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Employee>(); // Return an empty list on error
            }
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page ";
            List<Employee> employees = await GetEmployeesAsync();
            return View(employees);
        }

    }
}
