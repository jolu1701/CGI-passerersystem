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
                            tryer = reader.GetString(4); //Kollar om det finns något i databasen för fält 4, dvs Team. Om det inte finns något teamid för den anställde så sätts teamid i programmet till "0"
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
                        employees.Add(e); //Lägger till den hämtade anställde från databasen till listan i VS.
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
                    cmd.Parameters.AddWithValue("firstname", fn); // Det här är för att "@"-grejen ovan ska funka. Det inom hartassar är det som man ropar på med snabel-a, det andra är den variabel som det ska vara lika med.
                    cmd.Parameters.AddWithValue("surname", sn);
                    cmd.Parameters.AddWithValue("phonenumber", pn);
                    cmd.Parameters.AddWithValue("department", dep);
                    cmd.Parameters.AddWithValue("team", tm);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        

        public void AddGuest(string fn, string sn, string co)
        {
            Guest g = new Guest();
            g.firstName = fn;
            g.surName = sn;
            g.company = co;

            string stmt = "Insert into guest(firstname,surname,company) Values(@firstname,@surname,@company)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("firstname", fn);
                    cmd.Parameters.AddWithValue("surname", sn);
                    cmd.Parameters.AddWithValue("company", co);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AddMeeting(DateTime dt, int mh)
        {
            Meeting m = new Meeting();
            m.MeetingDT = dt;
            m.MeetingHolder = mh;

            string stmt = "Insert into meeting(datetime, fk_meetingholder) Values(@dt,@mh)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("dt", dt);
                    cmd.Parameters.AddWithValue("mh", mh);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AddDepartment(string name)
        {
            Department d = new Department();
            d.Name = name;

            string stmt = "Insert into department(departmentname) Values(@name)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveEmployee(Employee e)
        {
            int id = e.id;

            string stmt = "delete from employee where employeeid = @employeeid";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("employeeid", id); // Det här är för att "@"-grejen ovan ska funka. Det inom hartassar är det som man ropar på med snabel-a, det andra är den variabel som det ska vara lika med.
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
