using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Model;

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
        public HttpResponseMessage GetUniByName([FromBody] string uniName)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "SELECT * FROM university WHERE name LIKE '%" + uniName + "%';";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<University> uniList = new List<University>();
                    while(reader.Read())
                    {
                        University uni = new University(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        uniList.Add(uni);
                    }
                    reader.Close();

                    string uniString = "";
                    foreach(University uni in uniList)
                    {
                        uniString = String.Format("Uni OIB: {0}, Uni Name: {1}, Uni Address: {2}\n",
                                                    uni.Oib, uni.Name, uni.Address);
                    }

                    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, uniString);
                    return msg;
                }
                else
                {
                    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
                    return msg;
                }
            }
        }
       
        [HttpGet]
        [Route("api/University/{uniOib}")]
        public HttpResponseMessage GetUniByOib(int uniOib)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "SELECT * FROM university WHERE oib = " + uniOib.ToString() + ";";
               
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<University> uniList = new List<University>();
                    while (reader.Read())
                    {
                        University uni = new University(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        uniList.Add(uni);
                    }
                    reader.Close();

                    string uniString = "";
                    foreach (University uni in uniList)
                    {
                        uniString = String.Format("Uni OIB: {0}, Uni Name: {1}, Uni Address: {2}\n",
                                                    uni.Oib, uni.Name, uni.Address);
                    }

                    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, uniString);
                    return msg;
                }
                else
                {
                    HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.NotFound, "No rows found.");
                    return msg;
                }
            }
        }
       
        [HttpPost]
        [Route("api/University/{uniOib}")]
        public HttpResponseMessage PostUniversity(int uniOib, [FromBody] University uni)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "INSERT INTO university VALUES (" + uniOib.ToString() + ", @uniName, @uniAddress);";

                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@uniName", SqlDbType.VarChar, 50).Value = uni.Name;
                command.Parameters.Add("@uniAddress", SqlDbType.VarChar, 50).Value = uni.Address;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = command;
                adapter.InsertCommand.ExecuteNonQuery();
            }
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.Created, "University " + uni.Name + " created.");
            return msg;
        }

        [HttpPut]
        [Route("api/University/{uniOib}")]
        public HttpResponseMessage PutUniversityByOib(int uniOib, [FromBody] University uni)
        {
            using(SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "UPDATE university SET name = " + uni.Name + ", address = "
                                     + uni.Address + " WHERE oib = " + uniOib.ToString() + ";";

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.UpdateCommand = new SqlCommand(queryString, connection);
                adapter.UpdateCommand.ExecuteNonQuery();
            }
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "University " + uni.Name + " modified.");
            return msg;
        }
        
        [HttpDelete]
        [Route("api/University/{uniOib}")]
        public HttpResponseMessage DeleteUniByOib(int uniOib)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "DELETE FROM university WHERE OIB = " + Convert.ToString(uniOib) + ";";

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = queryString;
                adapter.DeleteCommand.ExecuteNonQuery();
            }
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "University deleted.");
            return msg;
        }

        [HttpDelete]
        [Route("api/University")]
        public HttpResponseMessage DeleteUniByName([FromBody] string uniName)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "DELETE FROM university WHERE name LIKE '%" + uniName + "%';";

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = queryString;
                adapter.DeleteCommand.ExecuteNonQuery();
            }
            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK, "University deleted.");
            return msg;
        }
    }
}   
