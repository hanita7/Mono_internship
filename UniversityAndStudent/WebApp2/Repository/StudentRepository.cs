using Model;
using Model.Common;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAndStudent.Common;

namespace Repository
{
    public class StudentRepository: IStudentRepository
    {
        public StudentRepository() { }

        public const string CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;"
                                        + "Integrated Security=True;Connect Timeout=30;Encrypt=False;"
                                        + "TrustServerCertificate=False;ApplicationIntent=ReadWrite;"
                                        + "MultiSubnetFailover=False";

        public async Task<List<IStudent>> GetAllStudentsAsync(string sort, string order)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();

                Sort add = new Sort();
                string queryString = "SELECT * FROM student " + add.SortBy(sort, order) + ";";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    List<IStudent> uniList = new List<IStudent>();
                    while (reader.Read())
                    {
                        IStudent s = new Student(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        uniList.Add(s);
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
        public async Task<List<IStudent>> GetAllStudentsAsync(int pageNum, int pageSize)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();

                Paging add = new Paging(pageNum, pageSize);
                string queryString = "SELECT * FROM student " + add.Pagination() + ";";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    List<IStudent> uniList = new List<IStudent>();
                    while (reader.Read())
                    {
                        IStudent s = new Student(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        uniList.Add(s);
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
        
        public async Task<List<IStudent>> GetStudentByNameAsync(string studentName)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "SELECT * FROM student WHERE name LIKE '%" + studentName + "%';";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    // there can be multiple students under similar name
                    List<IStudent> uniList = new List<IStudent>();
                    while (reader.Read())
                    {
                        IStudent s = new Student(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        uniList.Add(s);
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
        public async Task<IStudent> GetStudentByOibAsync(int studentOib)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "SELECT * FROM student WHERE oib = " + studentOib.ToString() + ";";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    reader.Read();
                    IStudent student = new Student(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    reader.Close();
                    return student;

                }
                else
                {
                    return null;
                }
            }
        }
        // return 1 if student posted
        // return 0 if given university OIB not in DB
        // return -1 if given student OIB already in DB
        // return -2 if error occured
        public async Task<int> PostStudentAsync(IStudent student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();

                    IStudentRepository studentRepo = new StudentRepository();
                    IStudent check = await studentRepo.GetStudentByOibAsync(student.Oib);
                    if(check != null)
                    {
                        return -1;
                    }

                    IUniversityRepository uniRepo = new UniversityRepository();
                    IUniversity uni = await uniRepo.GetUniByOibAsync(student.UniversityOib);
                    if (uni == null)
                    { 
                        return 0;
                    }

                    string queryString = "INSERT INTO student VALUES (@studentOib, @studentName, @studentUniOib);";

                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@studentOib", SqlDbType.Int).Value = student.Oib;
                    command.Parameters.Add("@studentName", SqlDbType.VarChar, 50).Value = student.Name;
                    command.Parameters.Add("@studentUniOib", SqlDbType.Int).Value = student.UniversityOib;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = command;
                    await adapter.InsertCommand.ExecuteNonQueryAsync();
                }
                return 1;
            }
            catch
            {
                return -2;
            }
        }

        public async Task<int> PutStudentAsync(IStudent student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();

                    IUniversityRepository uniRepo = new UniversityRepository();
                    IUniversity uni = await uniRepo.GetUniByOibAsync(student.UniversityOib);
                    if (uni == null)
                    {
                        return 0;
                    }

                    string queryString = "UPDATE student SET name = @studentName, universityOIB = @studentUniOib WHERE OIB = "
                                            + student.Oib + ";";

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = new SqlCommand();

                    SqlCommand command = new SqlCommand(queryString);
                    command.Parameters.Add("@studentName", SqlDbType.VarChar, 50).Value = student.Name;
                    command.Parameters.Add("@studentUniOib", SqlDbType.Int).Value = student.UniversityOib;

                    adapter.UpdateCommand = command;
                    await adapter.UpdateCommand.ExecuteNonQueryAsync();
                
                    return 1;
                }
            }
            catch
            {
                return -1;
            }
        }

        public async Task<bool> DeleteStudentByOibAsync(int studentOib)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();
                    string queryString = "DELETE FROM student WHERE OIB = " + studentOib.ToString() + ";";

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