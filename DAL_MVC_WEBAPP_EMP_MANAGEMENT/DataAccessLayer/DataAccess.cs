using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class EmpOrm
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int DeptId { get; set; }
    }

    public class DataAccess
    {
        public string connStr;
        private SqlConnection conn;
        private SqlCommand cmd;

        public DataAccess(string connStr)
        {
            this.connStr = connStr;
            conn = new SqlConnection(connStr);
            cmd = conn.CreateCommand();
        }

        public IEnumerable<EmpOrm> GetAllEmployee()
        {
            List<EmpOrm> list = new List<EmpOrm>();
            try
            {
                cmd.CommandText = "SELECT * FROM Employees"; 
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new EmpOrm
                    {
                        EmpId = reader.GetInt32(0),
                        EmpName = reader.GetString(1),
                        DeptId = reader.GetInt32(2)
                    });
                }
                reader.Close();
            }
            catch (SqlException sqlEx) // Added specific error handling for SQL exceptions
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex) // Added general error handling
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally // Ensures connection is closed
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return list;
        }

        public EmpOrm GetEmployeeById(int id)
        {
            EmpOrm employee = null; 
            try
            {
                cmd.CommandText = $"SELECT * FROM Employees WHERE EmpId = {id}";
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    employee = new EmpOrm
                    {
                        EmpId = reader.GetInt32(0),
                        EmpName = reader.GetString(1),
                        DeptId = reader.GetInt32(2)
                    };
                }
                reader.Close();
            }
            catch (SqlException sqlEx) // Added specific error handling for SQL exceptions
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex) // Added general error handling
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally // Ensures connection is closed
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return employee;
        }

        public bool AddEmployee(EmpOrm emp)
        {
            try
            {
                cmd.CommandText = $"INSERT INTO Employees VALUES ({emp.EmpId}, '{emp.EmpName}', {emp.DeptId})"; 
                conn.Open();
                int rowsEffected = cmd.ExecuteNonQuery();
                return rowsEffected > 0;
            }
            catch (SqlException sqlEx) // Added specific error handling for SQL exceptions
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                return false;
            }
            catch (Exception ex) // Added general error handling
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally // Ensures connection is closed
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public bool ModifyEmployee(EmpOrm emp)
        {
            try
            {
                cmd.CommandText = $"UPDATE Employees SET EmpName = '{emp.EmpName}', DeptId = {emp.DeptId} WHERE EmpId = {emp.EmpId}"; 
                conn.Open();
                int rowsEffected = cmd.ExecuteNonQuery();
                return rowsEffected > 0;
            }
            catch (SqlException sqlEx) // Added specific error handling for SQL exceptions
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                return false;
            }
            catch (Exception ex) // Added general error handling
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally // Ensures connection is closed
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public bool RemoveEmployee(int id)
        {
            try
            {
                cmd.CommandText = $"DELETE FROM Employees WHERE EmpId = {id}"; 
                conn.Open();
                int rowsEffected = cmd.ExecuteNonQuery();
                return rowsEffected > 0;
            }
            catch (SqlException sqlEx) // Added specific error handling for SQL exceptions
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
                return false;
            }
            catch (Exception ex) // Added general error handling
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            finally // Ensures connection is closed
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        static void Main(string[] args)
        {
            
        }
    }
}
