using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Model;

namespace Repository
{
    public class UniversityRepository
    {
        public const string CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;"
                                        + "Integrated Security=True;Connect Timeout=30;Encrypt=False;"
                                        + "TrustServerCertificate=False;ApplicationIntent=ReadWrite;"
                                        + "MultiSubnetFailover=False";

        public string GetUniByName(string uniName)
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

                    return uniString;
                }
                else
                {
                    return "No rows found.";
                }
            }
        }
        
    }
}
