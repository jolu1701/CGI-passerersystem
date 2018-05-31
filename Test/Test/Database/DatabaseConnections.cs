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

        public void AddEmployee(string fn, string sn, string pn, string dep, int depid, string tm, int temid)
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
                    cmd.Parameters.AddWithValue("department", depid);
                    cmd.Parameters.AddWithValue("team", temid);
                    cmd.ExecuteNonQuery();
                }

            }
        }



        public int AddGuest(string fn, string sn, string co)
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

        public void AddMeetingGuest(int gid, int mid, string bg)
        {
            MeetingGuest mg = new MeetingGuest();
            mg.Guestid = gid;
            mg.Meetingid = mid;
            mg.Badge = bg;


            string stmt = "Insert into meeting_guest(fk_guestid, fk_meetingid, badge) Values(@guestid, @meetingid, @badge)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("guestid", gid);
                    cmd.Parameters.AddWithValue("meetingid", mid);
                    cmd.Parameters.AddWithValue("badge", bg);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AddMeeting(DateTime dt, DateTime tm, Employee mh, String nt)
        {
            Meeting m = new Meeting();
            m.Date = dt;
            m.Time = tm;
            m.MeetingHolder = mh.ToString();
            m.Note = nt;

            string stmt = "Insert into meeting(date, time, fk_meetingholder, note) Values(@dt,@tm,@mh,@nt)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("dt", dt);
                    cmd.Parameters.AddWithValue("tm", tm);
                    cmd.Parameters.AddWithValue("mh", mh.id);
                    cmd.Parameters.AddWithValue("nt", nt);
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

        public void CheckInGuest(GuestExtras g)
        {
            g.checkIn = DateTime.Now;

            string stmt = "update meeting_guest set checkin = @ci where fk_guestid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("ci", DateTime.Now);
                    cmd.Parameters.AddWithValue("id", g.id);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void RemoveGuestFromMeeting(GuestExtras g)
        {
            string stmt = "DELETE from meeting_guest where fk_guestid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("id", g.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Department> GetAllDepartments()
        {
            Department d;
            List<Department> departments = new List<Department>();

            string stmt = "select * from department";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        d = new Department()
                        {
                            DepartmentID = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };

                        departments.Add(d); //Lägger till den hämtade avdelningen från databasen till listan i VS.
                    }
                }
                return departments;
            }
        }

        public List<Team> GetAllTeams()
        {
            Team t;
            List<Team> teams = new List<Team>();

            string stmt = "select * from team";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        t = new Team()
                        {
                            id = reader.GetInt32(0),
                            teamName = reader.GetString(1)
                        };

                        teams.Add(t); //Lägger till den hämtade avdelningen från databasen till listan i VS.
                    }
                }
                return teams;
            }
        }

        public List<Employee> GetMeetingEmployees(int meetingID) //Skapar lista över anställda deltagar på ett specifikt möte som styrs av inparametern meetingID.
        {
            Employee e;
            List<Employee> meetingEmployees = new List<Employee>();

            string stmt = "select e.employeeid,e.firstname,e.surname,e.phonenumber, d.departmentname, t.teamname from employee e inner join meeting_attendees ma on ma.fk_employeeid = e.employeeid inner join meeting me on ma.fk_meetingid = me.meetingid inner join department d on e.fk_departmentid = d.departmentid inner join team t on e.fk_teamid = t.teamid where ma.fk_meetingid = @id";

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
                            e = new Employee();

                            e.id = reader.GetInt32(0);
                            e.firstName = reader.GetString(1);
                            e.surName = reader.GetString(2);
                            e.phoneNumber = reader.GetString(3);
                            e.department = reader.GetString(4);
                            e.team = reader.GetString(5);

                            meetingEmployees.Add(e);
                        }
                    }
                    return meetingEmployees;
                }
            }
        }

        public void AddEmployeeToMeeting(Employee e, Meeting m)
        {
            string stmt = "Insert into meeting_attendees(fk_employeeid, fk_meetingid) Values(@employeeid, @meetingid)";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("employeeid", e.id);
                    cmd.Parameters.AddWithValue("meetingid", m.MeetingID);
                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void RemoveEmployeeFromMeeting(Employee e)
        {
            string stmt = "DELETE from meeting_attendees where fk_employeeid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("id", e.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveTeam(Team t)
        {
            string stmt = "DELETE from team where teamid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("id", t.id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditTeam(Team t, string newname)
        {
            string stmt = "update team set teamname = @newname where teamid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("id", t.id);
                    cmd.Parameters.AddWithValue("newname", newname);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RemoveDepartment(Department d)
        {
            string stmt = "DELETE from department where departmentid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("id", d.DepartmentID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EditDepartment(Department d, string newname)
        {
            string stmt = "update department set departmentname = @newname where departmentid = @id";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                {
                    cmd.Parameters.AddWithValue("id", d.DepartmentID);
                    cmd.Parameters.AddWithValue("newname", newname);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<MeetingHistory> GetMeetingHistory()
        {
            MeetingHistory mh;
            List<MeetingHistory> MeetingHist = new List<MeetingHistory>();

            string stmt = "select m.meetingid,g.firstname,g.surname,d.departmentname,m.date,e.firstname,e.surname,mg.checkin,mg.checkout,mg.badge " +
                "from meeting_guest mg inner join guest g on g.guestid = mg.fk_guestid inner join meeting m on mg.fk_meetingid = m.meetingid " +
                "inner join employee e on m.fk_meetingholder = e.employeeid " +
                "inner join department d on d.departmentid=e.fk_departmentid";

            using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(stmt, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        mh = new MeetingHistory()
                        {
                            Meetingholder = reader.GetString(5) + "" + reader.GetString(6),
                            MeetingHolderID = reader.GetInt32(0),
                            MhDepartment = reader.GetString(3),
                            MhGuest = reader.GetString(1) + "" +reader.GetString(2),
                            //Checkin=
                            //Checkout
                        };

                        MeetingHist.Add(mh);
                    }
                }
                return MeetingHist;
            }

        }

        public List<MeetingHistory> MeetingHistFIlter(string depsearch, string mhname, string gname)
        {
            MeetingHistory mh;
            List<MeetingHistory> MeetingHist = new List<MeetingHistory>();
            
                string stmt = "select m.meetingid,g.firstname,g.surname,d.departmentname,m.date,e.firstname,e.surname,mg.checkin,mg.checkout,mg.badge " +
                    "from meeting_guest mg inner join guest g on g.guestid = mg.fk_guestid inner join meeting m on mg.fk_meetingid = m.meetingid " +
                    "inner join employee e on m.fk_meetingholder = e.employeeid " +
                    "inner join department d on d.departmentid=e.fk_departmentid " +
                    "where d.departmentname ilike '%"+depsearch+"%' AND (e.firstname ilike '%"+mhname+"%' OR e.surname ilike '%"+mhname+"%') AND (g.firstname ilike '%"+gname+"%' OR g.surname ilike '%"+gname+"%')";

                using (var conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["dbConn"].ConnectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(stmt, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            mh = new MeetingHistory()
                            {
                                Meetingholder = reader.GetString(5) + " " + reader.GetString(6),
                                MeetingHolderID = reader.GetInt32(0),
                                MhDepartment = reader.GetString(3),
                                MhGuest = reader.GetString(1) + " " + reader.GetString(2),
                                //Checkin=
                                //Checkout
                            };

                            MeetingHist.Add(mh);
                        }
                    }
                }
                return MeetingHist;
            }
        }
    }


    
