using EmpCrudAdoApp.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmpCrudAdoApp.Repository
{
    public class EmployeeRepo
    {

        string conStr = "server = DESKTOP-2MLVLBI\\SQLEXPRESS;database=Employee; integrated security=SSPI";
        //to add Employee record
        public void AddEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string insertQuery = $"INSERT INTO emp_table VALUES(" +
                    $"{emp.Id}, '{emp.Name}','{emp.Address}','{emp.PhoneNo}')";
                SqlCommand cmd = new SqlCommand(insertQuery, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //to get all data from table
        public IEnumerable<Employee> GetAllEmployee()
        {
            List<Employee> empList = new List<Employee>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();
                string query = "Select * From emp_table";
                SqlCommand cm = new SqlCommand(query, con);

                using (SqlDataReader rdr = cm.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Employee emp = new Employee();
                        emp.Id = Convert.ToInt32(rdr["Id"]);
                        emp.Name = rdr["Name"].ToString();
                        emp.Address = rdr["Address"].ToString();
                        emp.PhoneNo = Convert.ToInt64(rdr["Phone"]);
                        empList.Add(emp);
                    }
                }

                con.Close();
            }

            return empList;
        }
        //to get record of particular employee
        public Employee GetEmployeeData(int id)
        {
            Employee emp = new Employee();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sqlQuery = "SELECT * FROM emp_table WHERE Id='" + id + "'";
                SqlCommand cm = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cm.ExecuteReader();
                while (rdr.Read())
                {
                    emp.Id = Convert.ToInt32(rdr["Id"]);
                    emp.Name = rdr["Name"].ToString();
                    emp.Address = rdr["Address"].ToString();
                    emp.PhoneNo = Convert.ToInt64(rdr["Phone"]);
                }
                con.Close();
            }
            return emp;
        }

        //to update the record
        public void UpdateEmployee(Employee emp, int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string updateQuery = $"Update emp_table SET Name='{emp.Name}', Address='{emp.Address}', Phone='{emp.PhoneNo}' Where Id='{id}'";
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

        //to delete the record
        public void DeleteEmployee(Employee emp, int id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string updateQuery = $"DELETE FROM emp_table WHERE Id='{id}'";
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

    }

}