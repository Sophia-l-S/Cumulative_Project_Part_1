using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogProject.Models;
using MySql.Data.MySqlClient;

namespace BlogProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private BlogDbContext Blog = new BlogDbContext();

        //This Controller Will access the authors table of our blog database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of Teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) like lower(@key) or lower (concat(teacherfname, ' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["TeacherId"];
                string TeacherFname = (string)ResultSet["TeacherFname"];
                string TeacherLname = (string)ResultSet["TeacherLname"];
                string employeeNumber = (string)ResultSet["employeeNumber"];
                DateTime HireDate = (DateTime)ResultSet["HireDate"];
                decimal Salary = (decimal)ResultSet["Salary"];

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.employeeNumber = employeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;


                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of Teacher names
            return Teachers;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where TeacherId = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();


            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["TeacherId"];
                string TeacherFname = (string)ResultSet["TeacherFname"];
                string TeacherLName = (string)ResultSet["TeacherLname"];

                string employeeNumber = (string)ResultSet["employeeNumber"];
                DateTime HireDate = (DateTime)ResultSet["HireDate"];
                decimal Salary = (decimal)ResultSet["Salary"];


               
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLName;
                NewTeacher.employeeNumber = employeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;
            }
            return NewTeacher;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <example> POST : /api/TeacherData/DeleteTeacher/3</example>
        /// 
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "delete form teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close(); 
        }


        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers(TeacherFname, TeacherLname, employeeNumber, HireDate, Salary) values (@TeacherFname, @TeacherLname, @employeeNumber, @HireDate, @Salary)";

            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@employeeNumber", NewTeacher.employeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();

        }

        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set TeacherFname=@TeacherFname, TeacherLname=@TeacherLname, employeeNumber=@employeeNumber, HireDate=@HireDate, Salary=@Salary where teacherid=@TeacherId";

            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@employeeNumber", TeacherInfo.employeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);


            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();

        }


    }
}
