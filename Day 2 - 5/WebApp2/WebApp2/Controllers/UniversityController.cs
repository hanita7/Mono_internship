using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace WebApp2.Controllers
{
    public class UniversityController : ApiController
    {
        public const string CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;"
                                        + "Integrated Security=True;Connect Timeout=30;Encrypt=False;"
                                        + "TrustServerCertificate=False;ApplicationIntent=ReadWrite;"
                                        + "MultiSubnetFailover=False";
    
        [HttpGet]
        [Route("api/University")]
        public async Task<HttpResponseMessage> GetUniByNameAsync([FromBody] string uniName)
        {
            UniversityService uniSvc = new UniversityService();
            List<University> uniList = await uniSvc.GetUniByNameAsync(uniName);

            if(uniList == null)
            {
                HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
                return msg;
            }
            else
            {
                // there might be multiple unis
                string uniString = "";
                foreach (University uni in uniList)
                {
                    uniString += String.Format("University OIB: {0}, University Name: {1}, University Address: {2}\n",
                                                uni.Oib, uni.Name, uni.Address);
                }
                return Request.CreateResponse(HttpStatusCode.OK, uniString);
            }
        }
       
        [HttpGet]
        [Route("api/University/{uniOib}")]
        public async Task<HttpResponseMessage> GetUniByOibAsync(int uniOib)
        {
            UniversityService uniSvc = new UniversityService();
            University uni = await uniSvc.GetUniByOibAsync(uniOib);

            if (uni == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
            }
            else
            {
                string uniString = String.Format("University OIB: {0}, University Name: {1}, University Address: {2}",
                                                uni.Oib, uni.Name, uni.Address);
                return Request.CreateResponse(HttpStatusCode.OK, uniString);
            }
        }
       
        [HttpPost]
        [Route("api/University/")]
        public async Task<HttpResponseMessage> PostUniversityAsync([FromBody] University uni)
        {
            UniversityService uniSvc = new UniversityService();
            if (await uniSvc.PostUniversityAsync(uni))
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
            UniversityService uniSvc = new UniversityService();
            if ( await uniSvc.PutUniversityAsync(uni))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "University " + uni.Name + " modified.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error encountered.");
            }
        }
        
        [HttpDelete]
        [Route("api/University/{uniOib}")]
        public async Task<HttpResponseMessage> DeleteUniByOibAsync(int uniOib)
        {
            UniversityService uniSvc = new UniversityService();
            if(await uniSvc.DeleteUniByOibAsync(uniOib))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "University deleted.");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error encountered.");
            }
         }

        [HttpDelete]
        [Route("api/University")]
        public async Task<HttpResponseMessage> DeleteUniByNameAsync([FromBody] string uniName)
        {
            UniversityService uniSvc = new UniversityService();
            if (await uniSvc.DeleteUniByNameAsync(uniName))
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
