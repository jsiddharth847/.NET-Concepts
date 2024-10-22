using FinalCRUDEMP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FinalCRUDEMP
{
    public class DataAccess
    {


        public List<Employee> GetAllEmployees()
        {
            try
            {
                using (MyDBEntities mydb = new MyDBEntities())
                {
                    return mydb.Employees.ToList();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong");
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public List<Employee> GetEmployeesByID(int id)
        {

            try
            {

                using (MyDBEntities mydb = new MyDBEntities())
                {

                    return mydb.Employees.Where(record => record.id == id).ToList();

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Something went wrong");
                return null;
            }
        }



        public void AddEmployee(Employee emp)
        {

            try
            {

                using (MyDBEntities mydb = new MyDBEntities())
                {


                    if (emp != null)
                    {
                        Console.WriteLine("Employee is empty");
                    }

                    mydb.Employees.Add(emp);
                    mydb.SaveChanges();

                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message, "Something went wrong"); }


        }


        public void UpdateEmployee(Employee emp)
        {
            try
            {
                using (MyDBEntities mydb = new MyDBEntities())
                {


                    var empSelected = (from Employee in mydb.Employees
                                       where Employee.id == emp.id
                                       select Employee).First();

                    empSelected.name = emp.name;
                    empSelected.age = emp.age;

                    mydb.SaveChanges();



                }
            }
            catch (Exception ex) { Console.WriteLine("Something went wrong"); }


        }

        public void DeleteEmployee(int id)
        {

            try
            {
                using (MyDBEntities mydb = new MyDBEntities())
                {


                    var employee = (from emp in mydb.Employees where emp.id == id select emp).First();

                    mydb.Employees.Remove(employee);
                    mydb.SaveChanges();
                }
            }
            catch (Exception ex) { Console.WriteLine("Something went wrong"); }
        }
        static void Main(string[] args)
        {
            // SOAP API
            // REST 
        }
    }
}
