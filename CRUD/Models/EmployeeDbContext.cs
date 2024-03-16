using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CRUD.Models
{
    public class EmployeeDbContext
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;

        public List<Employee> GetEmployees()
        {
            List<Employee> EmployeesList = new List<Employee>();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spGetEmployees1", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Employee emp = new Employee();
                        emp.id = Convert.ToInt32(dr["id"]);
                        emp.name = dr["name"].ToString();
                        emp.gender = dr["gender"].ToString();
                        emp.age = Convert.ToInt32(dr["age"]);
                        emp.salary = Convert.ToInt32(dr["salary"]);
                        emp.city = dr["city"].ToString();
                        EmployeesList.Add(emp);
                    }
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error occurred while retrieving employees: " + ex.Message);
            }

            return EmployeesList;
        }

        public bool AddEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", emp.name);
                    cmd.Parameters.AddWithValue("@gender", emp.gender);
                    cmd.Parameters.AddWithValue("@age", emp.age);
                    cmd.Parameters.AddWithValue("@salary", emp.salary);
                    cmd.Parameters.AddWithValue("@city", emp.city);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error occurred while adding employee: " + ex.Message);
                throw new Exception("An error occurred while adding an employee. Please try again later.", ex);
            }
        }


        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", emp.id);
                    cmd.Parameters.AddWithValue("@name", emp.name);
                    cmd.Parameters.AddWithValue("@gender", emp.gender);
                    cmd.Parameters.AddWithValue("@age", emp.age);
                    cmd.Parameters.AddWithValue("@salary", emp.salary);
                    cmd.Parameters.AddWithValue("@city", emp.city);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error occurred while adding employee: " + ex.Message);
                throw new Exception("An error occurred while adding an employee. Please try again later.", ex);
            }
        }

        public bool DeleteEmployee(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return i > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while deleting employee: " + ex.Message);
                throw new Exception("An error occurred while deleting an employee. Please try again later.", ex);
            }
        }


    }
}
