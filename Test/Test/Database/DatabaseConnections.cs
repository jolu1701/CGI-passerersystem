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

            string stmt = "select m.employeeid, m.firstname, m.surname, t.teamname, d.departmentname, m.phonenumber from employee m " +
                "inner join team t on m.fk_teamid=t.teamid " +
                "inner join department d on m.fk_departmentid = d.departmentid order by m.employeeid asc";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                                              
                        e = new Employee()
                        {
                            id = reader.GetInt32(0),
                            firstName = reader.GetString(1),
                            surName = reader.GetString(2),
                            team = reader.GetString(3),
                            department = reader.GetString(4),                            
                            phoneNumber = reader.GetString(5)
                        };

                        if (e.team == "null")
                            e.team = "";

                        employees.Add(e); //Lägger till den hämtade anställde från databasen till listan i VS.
                    }
                }
                return employees;
            }
        }

        public void AddEmployee(string fn, string sn, string pn, string dep, string tm)
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

        

        public int AddGuest(string fn, string sn, string co )
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
            //////////////////////

            stmt = "Select * from guest where firstname = @firstname AND surname = @surname AND company = @company";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("firstname", fn);
                    cmd.Parameters.AddWithValue("surname", sn);
                    cmd.Parameters.AddWithValue("company", co);

                    object guestid = cmd.ExecuteScalar();
                    string gid = guestid.ToString();
                    g.id = int.Parse(gid);                    
                }
                
            }

            return g.id;
        }

        public void AddMeetingGuest( int gid, int mid, string bg, DateTime ci)
        {
            MeetingGuest mg = new MeetingGuest();
            mg.Guestid = gid;
            mg.Meetingid = mid;
            mg.Badge = bg;
            mg.Checkin = ci;
            

            string stmt = "Insert into meeting_guest(fk_guestid, fk_meetingid, badge, checkin) Values(@guestid, @meetingid, @badge, @checkin)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("guestid", gid);
                    cmd.Parameters.AddWithValue("meetingid", mid);
                    cmd.Parameters.AddWithValue("badge", bg);
                    cmd.Parameters.AddWithValue("checkin", ci);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AddMeeting(DateTime dt, string mh)
        {
            Meeting m = new Meeting();
            m.Date = dt;
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

        public void AddTeam(string teamname)
        {
            Team t = new Team();
            t.teamName = teamname;

            string stmt = "Insert into team(teamname) Values(@teamname)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("teamname", teamname);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public List<Meeting> GetAllMeetings()
        {
            Meeting m;
            List<Meeting> meetings = new List<Meeting>();

            string stmt = "select me.meetingid,me.date,em.firstname,em.surname from meeting me inner join employee em on me.fk_meetingholder = em.employeeid";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        m = new Meeting()
                        {
                            MeetingID = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            //Time = reader.GetDateTime(2),
                            MeetingHolder = reader.GetString(2) + " " + reader.GetString(3),                           
                            //Note = reader.GetString(4)
                        };                                              

                        meetings.Add(m); 
                    }
                }
                return meetings;
            }
        }

        public List<Guest> GetMeetingGuests(int meetingID) //Skapar gästlista för ett specifikt möte som styrs av inparametern meetingID.
        {
            Guest g;
            List<Guest> meetingGuests = new List<Guest>();

            string stmt = "select g.guestid,g.firstname,g.surname,g.company from guest g inner join meeting_guest mg on mg.fk_guestid = g.guestid inner join meeting me on mg.fk_meetingid = me.meetingid where meetingid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("id", meetingID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            g = new Guest()
                            {
                                id = reader.GetInt32(0),
                                firstName = reader.GetString(1),
                                surName = reader.GetString(2),
                                company = reader.GetString(3)
                            };

                            meetingGuests.Add(g);
                        }
                    }
                    return meetingGuests;
                }
            }
        }

        public List<GuestExtras> GetMeetingGuestsExtras(int meetingID) //Skapar gästlista för ett specifikt möte som styrs av inparametern meetingID samt skickar med in-,utcheckning och badge, sparas sen i en fusionmodell .
        {
            GuestExtras ge;
            List<GuestExtras> meetingGuests = new List<GuestExtras>();

            string stmt = "select g.guestid,g.firstname,g.surname,g.company,mg.checkin,mg.checkout,mg.badge from guest g inner join meeting_guest mg on mg.fk_guestid = g.guestid inner join meeting me on mg.fk_meetingid = me.meetingid where meetingid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("id", meetingID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            ge = new GuestExtras();


                            ge.id = reader.GetInt32(0);
                            ge.firstName = reader.GetString(1);
                            ge.surName = reader.GetString(2);
                            ge.company = reader.GetString(3);
                            try
                            {
                                ge.checkIn = reader.GetDateTime(4);
                            }
                            catch (Exception)
                            {
                                
                            }
                            try
                            {
                                ge.checkOut = reader.GetDateTime(5);
                            }
                            catch (Exception)
                            {
                                
                            }
                            ge.badge = reader.GetString(6);
                            

                            meetingGuests.Add(ge);
                        }
                    }
                    return meetingGuests;
                }
            }
        }
        //attans bananer

        public void CheckOutGuest(GuestExtras g)
        {
            g.checkOut = DateTime.Now;

            string stmt = "update meeting_guest set checkout = @co where fk_guestid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("co", DateTime.Now);
                    cmd.Parameters.AddWithValue("id", g.id);
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}
