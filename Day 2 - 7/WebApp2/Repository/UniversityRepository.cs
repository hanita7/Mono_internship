using Model.Common;
using Model;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UniversityRepository: IUniversityRepository
    {
        public UniversityRepository() { }

        public const string CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;"
                                        + "Integrated Security=True;Connect Timeout=30;Encrypt=False;"
                                        + "TrustServerCertificate=False;ApplicationIntent=ReadWrite;"
                                        + "MultiSubnetFailover=False";

        public async Task<List<IUniversity>> GetUniByNameAsync(string uniName)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "SELECT * FROM university WHERE name LIKE '%" + uniName + "%';";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    // there can be multiple university under similar name
                    // if uniName is "Odjel" there could be "Odjel za matematiku", "Odjel za fiziku", "Odjel za biologiju" ...
                    // all unis are saved in uniList
                    List<IUniversity> uniList = new List<IUniversity>();
                    while (reader.Read())
                    {
                        IUniversity uni = new University(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        uniList.Add(uni);
                    }
                    reader.Close();

                    return uniList;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<IUniversity> GetUniByOibAsync(int uniOib)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "SELECT * FROM university WHERE oib = " + uniOib.ToString() + ";";

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                reader.Read();
                if (reader.HasRows)
                {
                    IUniversity uni = new University(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    reader.Close();
                    return uni;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> PostUniversityAsync(IUniversity uni)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();
                    string queryString = "INSERT INTO university VALUES (@uniOib, @uniName, @uniAddress);";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@uniOib", SqlDbType.Int).Value = uni.Oib;
                    command.Parameters.Add("@uniName", SqlDbType.VarChar, 50).Value = uni.Name;
                    command.Parameters.Add("@uniAddress", SqlDbType.VarChar, 50).Value = uni.Address;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    await adapter.InsertCommand.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> PutUniversityAsync(IUniversity uni)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();
                    string queryString = "UPDATE university SET name = " + uni.Name + ", address = "
                                         + uni.Address + " WHERE oib = " + uni.Oib + ";";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand(queryString, connection);
                    await adapter.UpdateCommand.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUniByOibAsync(int uniOib)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();
                    string queryString = "DELETE FROM university WHERE OIB = " + Convert.ToString(uniOib) + ";";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.DeleteCommand = connection.CreateCommand();
                    adapter.DeleteCommand.CommandText = queryString;
                    await adapter.DeleteCommand.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUniByNameAsync(string uniName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();
                    string queryString = "DELETE FROM university WHERE name LIKE '%" + uniName + "%';";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.DeleteCommand = connection.CreateCommand();
                    adapter.DeleteCommand.CommandText = queryString;
                    await adapter.DeleteCommand.ExecuteNonQueryAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
