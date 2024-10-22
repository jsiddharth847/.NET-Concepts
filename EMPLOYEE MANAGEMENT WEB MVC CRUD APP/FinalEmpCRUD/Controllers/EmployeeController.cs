using FinalCRUDEMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using System.Net;
using System.Net.Http;

namespace FinalEmpCRUD.Controllers
{
    public class EmployeeController : ApiController
    {

        DataAccess da = new DataAccess();

        // GET: api/Employee
        [HttpGet]
        public IEnumerable<Employee> GetAllEmployees()
        {


            var employee = da.GetAllEmployees();
            return employee;


        }
        // GET: api/Employee/id
        public IHttpActionResult GetEmp(int id)
        {
            try
            {
                var employee = da.GetEmployeesByID(id).First();

                if (employee == null)
                {

                    return Content(HttpStatusCode.NotFound, new { Message = "Employee not found", EmployeeId = id });



                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
                return BadRequest(ex.Message);
            }

        }


        // POST: api/Employee

        [HttpPost]
        public IHttpActionResult AddEmp(Employee emp)
        {
            try
            {
                if (emp == null)
                {

                    return BadRequest("Employee not found");

                }

                da.AddEmployee(emp);
                return Ok(emp);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Employee
        [HttpPut]
        public IHttpActionResult UpdateEmployee(Employee emp)
        {

            try
            {
                if (emp == null)
                {

                    return BadRequest("Employee not found");

                }

                var existingEmployee = da.GetAllEmployees().FirstOrDefault(e => e.id == emp.id);

                if (existingEmployee == null)
                {


                    return BadRequest("Employee not found");

                }
                da.UpdateEmployee(emp);
                return Ok(existingEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("something went wrong");
                return BadRequest(ex.Message);
            }
        }


        // DELETE: api/Employee/id

        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var employee = da.GetAllEmployees().FirstOrDefault(emp => emp.id == id);
            if (employee == null)
            {
                //return NotFound();
                return Content(HttpStatusCode.NotFound, new { Message = "Employee not found", EmployeeId = id });
            }
            da.DeleteEmployee(id);
            //return StatusCode(HttpStatusCode.NoContent);
            return Content(HttpStatusCode.OK, new { Message = "Employee deleted successfully", DeletedEmployeeId = id });
        }


    }
}