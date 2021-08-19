using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace WebApi.Controllers
{
    public class StudentController : ApiController
    {

        [HttpGet]
        [Route("api/Student/{studentName}")]
        public HttpResponseMessage GetStudentByName(string studentName)
        {
             
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, studentsString);
            return msg;
        }

        [HttpPost]
        [Route("api/Student/{studentOib}")]
        public HttpResponseMessage PostStudent(int studentOib, [FromBody] Student student)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "INSERT INTO student VALUES (" + studentOib.ToString() + ", @studentName, @studentUniOib);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@studentName", SqlDbType.VarChar, 50).Value = student.Name;
                command.Parameters.Add("@studentUniOib", SqlDbType.Int).Value = student.UniversityOib;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
            }

            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Created, "Student " + student.Name +" created.");
            return msg;
        }

        [HttpPut]
        [Route("api/Student/{studentOib}")]
        public HttpResponseMessage PutStudent(int studentOib, [FromBody] Student student)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                string queryString = "UPDATE student SET name = @studentName, universityOIB = @studentUniOib WHERE OIB = " 
                                        + studentOib.ToString() + ";";

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand();

                SqlCommand command = new SqlCommand(queryString);
                command.Parameters.Add("@studentName", SqlDbType.VarChar, 50).Value = student.Name;
                command.Parameters.Add("@studentUniOib", SqlDbType.Int).Value = student.UniversityOib;

                adapter.UpdateCommand = command; 
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "Student's data modified.");
            return msg;
        }

        [HttpDelete]
        [Route("api/Student/{studentOib}")]
        public HttpResponseMessage DeleteStudentByOib(int studentOib)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "DELETE FROM student WHERE OIB = " + studentOib.ToString() + ";";

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = queryString;
                adapter.DeleteCommand.ExecuteNonQuery();
            }
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "Student deleted.");
            return msg;
        }
    }
}