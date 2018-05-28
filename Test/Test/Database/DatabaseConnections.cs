using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Test.Model;
using Npgsql;

namespace Test.Database
{
    class DatabaseConnections
    {
        public List<Employee> GetAllEmployees()
        {
            Employee e;
            List<Employee> employees = new List<Employee>(); 

            string stmt = "select * from employee";
            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tryer;
                        try
                        {
                            tryer = reader.GetString(4);
                        }
                        catch (Exception)
                        {
                            tryer = "0";
                        }
                        
                        e = new Employee()
                        {
                            id = reader.GetInt32(0),
                            firstName = reader.GetString(1),
                            surName = reader.GetString(2),
                            department = int.Parse(reader.GetString(3)),                            
                            team = int.Parse(tryer),
                            phoneNumber = reader.GetString(5)
                        };
                        employees.Add(e);
                    }
                }
                return employees;
            }
        }

        public void AddEmployee(string fn, string sn, string pn, int dep, int tm)
        {
            Employee e = new Employee();
            e.firstName = fn;
            e.surName = sn;
            e.phoneNumber = pn;
            e.department = dep;
            e.team = tm;

            string stmt = "Insert into employee(firstname,surname,fk_departmentid,fk_teamid,phonenumber) Values(@firstname,@surname,@department,@team,@phonenumber)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("firstname", fn);
                    cmd.Parameters.AddWithValue("surname", sn);
                    cmd.Parameters.AddWithValue("phonenumber", pn);
                    cmd.Parameters.AddWithValue("department", dep);
                    cmd.Parameters.AddWithValue("team", tm);
                    cmd.ExecuteNonQuery();
                }

            }
        }

      
    }
}
