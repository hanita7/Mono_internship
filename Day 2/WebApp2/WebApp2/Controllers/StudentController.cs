using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class Student
    {
        public static Dictionary<int, string> students = new Dictionary<int, string>();
    }
    public class StudentController : ApiController
    {
        [HttpGet]
        [Route("api/Student/{id}")]
        // for given id returns student
        public HttpResponseMessage GetStudent(int id)
        {
            try
            {
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, Student.students[id]);
                return msg;
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Student doesn't exist under given id.");
            }
        }

        [HttpPost]
        [Route("api/Student/{id}")]
        // for given id creates student
        public HttpResponseMessage PostStudent(int id ,[FromBody]string name)
        {
            try
            {
                Student.students.Add(id, name);
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Created, "New student created.");
                return msg;
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Student already exists under given id.");
            }
        }

        [HttpPut]
        [Route("api/Student/{id}")]
        // for given id modifies student
        public HttpResponseMessage PutStudent(int id, [FromBody] string name)
        {
            try
            {
                if(Student.students[id] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Student doesn't exist under given id.");
                }
                Student.students[id] = name;
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "Student modified.");
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
            try
            {
                if (Student.students[id] == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Student doesn't exist under given id.");
                }
                Student.students.Remove(id);
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "Student deleted.");
                return msg;
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Student doesn't exist under given id.");
            }
        }
    }
}
