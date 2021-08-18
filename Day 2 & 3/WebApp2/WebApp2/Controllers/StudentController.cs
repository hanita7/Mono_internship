using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;

namespace WebApi.Controllers
{
    public class Student
    {
        //public static Dictionary<int, string> students = new Dictionary<int, string>();
        private string name;
        private int oib;

        public Student() { }
        public Student(int _oib, string _name)
        {
            oib = _oib;
            name = _name;
        }

        public int GetOib()
        {
            return oib;
        }
        public string GetName()
        {
            return name;
        }
        
    }
    public class StudentController : ApiController
    {
        public const string CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;"
                                        + "Integrated Security=True;Connect Timeout=30;Encrypt=False;"
                                        + "TrustServerCertificate=False;ApplicationIntent=ReadWrite;"
                                        + "MultiSubnetFailover=False";

        [HttpGet]
        [Route("api/Student/{studentName}")]
        // for given id returns student
        public HttpResponseMessage GetStudentOibByName(string studentName)
        {
             using (SqlConnection connection = new SqlConnection(CONNECTION))
             {
                connection.Open();
                 string queryString = "SELECT oib FROM student WHERE name LIKE '%" + studentName + "%';";
                 SqlCommand command = new SqlCommand(queryString, connection);

                 SqlDataReader reader = command.ExecuteReader();

                 if (reader.HasRows)
                 {
                     List<Student> studentsList = new List<Student>();
                     while (reader.Read())
                     {
                         Student s = new Student(reader.GetInt32(0), studentName);
                         studentsList.Add(s);
                     }
                     reader.Close();

                     string studentsString = "";
                     foreach(Student s in studentsList)
                     {
                         studentsString += String.Format("Student OIB: {0}, Student Name: {1}\n", s.GetOib(), s.GetName());
                     }

                     HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, studentsString);
                     return msg;
                 }
                 else
                 {
                     return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
                 }
             }
        }

        [HttpPost]
        [Route("api/Student/{studentOib}")]
        public HttpResponseMessage PostStudent(int studentOib, [FromBody] string studentName)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "INSERT INTO student VALUES (@studentOib, @studentName);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@studentOib", SqlDbType.Int).Value = studentOib;
                command.Parameters.Add("@studentName", SqlDbType.VarChar, 50).Value = studentName;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
            }

            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Created, "Student " + studentName +" created.");
            return msg;
        }

        [HttpPut]
        [Route("api/Student/{studentOib}")]
        // for given id modifies student
        public HttpResponseMessage PutStudentName(int studentOib, [FromBody] string studentName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    string queryString = "UPDATE students SET name = @studentName WHERE OIB = @studentOib;";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand();

                    SqlCommand command = new SqlCommand(queryString);
                    command.Parameters.Add("@studentOib", SqlDbType.Int).Value = studentOib;
                    command.Parameters.Add("@studentName", SqlDbType.VarChar, 50).Value = studentName;

                    adapter.UpdateCommand = command;
                }
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "Student's name modified.");
                return msg;
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Student doesn't exist under given id.");
            }
        }

        [HttpDelete]
        [Route("api/Student/{id}")]
        // for given id deletes student
        public HttpResponseMessage DeleteStudent(int id)
        {
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "Did nothing.");
            return msg;
        }
    }
}
