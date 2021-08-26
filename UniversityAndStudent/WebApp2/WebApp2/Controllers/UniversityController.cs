using Model;
using Model.Common;
using Service;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using UniversityAndStudent.Common;

namespace WebApp2.Controllers
{
    public class UniversityController : ApiController
    {
        IUniversityService UniService { get; set; }
        public UniversityController(IUniversityService uniService)
        {
            UniService = uniService;
        }

        public const string CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;"
                                        + "Integrated Security=True;Connect Timeout=30;Encrypt=False;"
                                        + "TrustServerCertificate=False;ApplicationIntent=ReadWrite;"
                                        + "MultiSubnetFailover=False";

        [HttpGet]
        [Route("api/University/all/sort/")]
        public async Task<HttpResponseMessage> GetAllUnisAsync([FromUri]Sort sorter)
        {
            List<IUniversity> uniList = await UniService.GetAllUnisAsync(sorter);

            if (uniList == null)
            {
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
                return msg;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, uniList);
            }
        }
        [HttpGet]
        [Route("api/University/all/paging")]
        public async Task<HttpResponseMessage> GetAllUnisAsync([FromUri]Paging pager)
        {
            List<IUniversity> uniList = await UniService.GetAllUnisAsync(pager);

            if (uniList == null)
            {
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
                return msg;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, uniList);
            }
        }
        [HttpGet]
        [Route("api/University/all/")]
        public async Task<HttpResponseMessage> GetAllUnisAsync([FromUri]UniversityFilter uniFilter)
        {
            List<IUniversity> uniList = await UniService.GetAllUnisAsync(uniFilter);

            if (uniList == null)
            {
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
                return msg;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, uniList);
            }
        }

        [HttpGet]
        [Route("api/University/name/{uniName}")]
        public async Task<HttpResponseMessage> GetUniByNameAsync(string uniName)
        {
            List<IUniversity> uniList = await UniService.GetUniByNameAsync(uniName);

            if(uniList == null)
            {
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
                return msg;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, uniList);
            }
        }
       
        [HttpGet]
        [Route("api/University/oib/{uniOib}")]
        public async Task<HttpResponseMessage> GetUniByOibAsync(int uniOib)
        {
            IUniversity uni = await UniService.GetUniByOibAsync(uniOib);

            if (uni == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, uni);
            }
        }
       
        [HttpPost]
        [Route("api/University/")]
        public async Task<HttpResponseMessage> PostUniversityAsync([FromBody] University uni)
        {
            if (await UniService.PostUniversityAsync(uni))
            {
                return Request.CreateResponse(HttpStatusCode.Created, "University " + uni.Name + " created.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error encountered.");
             }
        }

        [HttpPut]
        [Route("api/University/{uniOib}")]
        public async Task<HttpResponseMessage> PutUniversityByOibAsync(int uniOib, [FromBody] University uni)
        {
            uni.Oib = uniOib;
            if ( await UniService.PutUniversityAsync(uni))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "University " + uni.Name + " modified.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error encountered.");
            }
        }
        
        [HttpDelete]
        [Route("api/University/oib/{uniOib}")]
        public async Task<HttpResponseMessage> DeleteUniByOibAsync(int uniOib)
        {
            if(await UniService.DeleteUniByOibAsync(uniOib))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "University deleted.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error encountered.");
            }
         }

        [HttpDelete]
        [Route("api/University/name/{uniName}")]
        public async Task<HttpResponseMessage> DeleteUniByNameAsync(string uniName)
        {
            if (await UniService.DeleteUniByNameAsync(uniName))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "University deleted.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error encountered.");
            }
        }
    }
}   
