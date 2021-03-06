using Model;
using Model.Common;
using Service;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UniversityAndStudent.Common;

namespace WebApi.Controllers
{
    public class StudentController : ApiController
    {
        IStudentService StudentService { get; set; }

        public StudentController(IStudentService studentService) 
        {
            StudentService = studentService;
        }

        [HttpGet]
        [Route("api/Student/all/sort/")]
        public async Task<HttpResponseMessage> GetAllStudentsAsync([FromUri]Sort sorter)
        {
            List<IStudent> studentList = await StudentService.GetAllStudentsAsync(sorter);
            if (studentList == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, studentList);
            }
        }
        [HttpGet]
        [Route("api/Student/all/paging")]
        public async Task<HttpResponseMessage> GetAllStudentsAsync([FromUri]Sort sorter,[FromUri]Paging pager)
        {
            List<IStudent> studentList = await StudentService.GetAllStudentsAsync(sorter, pager);
            if (studentList == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, studentList);
            }
        }
        [HttpGet]
        [Route("api/Student/all/filtering")]
        public async Task<HttpResponseMessage> GetAllStudentsAsync([FromUri] StudentFilter studentFilter)
        {
            List<IStudent> studentList = await StudentService.GetAllStudentsAsync(studentFilter);
            if (studentList == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, studentList);
            }
        }

        [HttpGet]
        [Route("api/Student/name/{studentName}")]
        public async Task<HttpResponseMessage> GetStudentByNameAsync(string studentName)
        {
            List<IStudent> studentList = await StudentService.GetStudentByNameAsync(studentName);
            if(studentList == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, studentList);
            }
        }

        [HttpGet]
        [Route("api/Student/oib/{studentOib}")]
        public async Task<HttpResponseMessage> GetStudentByOibAsync(int studentOib)
        {
            IStudent student = await StudentService.GetStudentByNameAsync(studentOib);
            if (student == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
        }

        [HttpPost]
        [Route("api/Student/")]
        public async Task<HttpResponseMessage> PostStudentAsync([FromBody] Student student)
        {
            int response = await StudentService.PostStudentAsync(student);
            if (response == 1)
            {
                return Request.CreateResponse(HttpStatusCode.Created, "Student " + student.Name +" created.");
            }
            else if (response == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Given University OIB not found in DataBase.");
            }
            else if (response == -1)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Student under given OIB already exists in DataBase.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured.");
            }
        }

        [HttpPut]
        [Route("api/Student/{studentOib}")]
        public async Task<HttpResponseMessage> PutStudentAsync(int studentOib, [FromBody] Student student)
        {
            student.Oib = studentOib;
            
            int response = await StudentService.PutStudentAsync(student);
            if (response == 1)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Student " + student.Name + " modified.");
            }
            else if (response == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Given University OIB not found in DataBase.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured.");
            }
        }

        [HttpDelete]
        [Route("api/Student/oib/{studentOib}")]
        public async Task<HttpResponseMessage> DeleteStudentByOibaSYNC(int studentOib)
        {
            if (await StudentService.DeleteStudentByOibAsync(studentOib))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Student deleted.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error occured.");
            }
        }
    }
}