using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StudentRepository
    {
        public StudentRepository() { }

        public const string CONNECTION = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;"
                                        + "Integrated Security=True;Connect Timeout=30;Encrypt=False;"
                                        + "TrustServerCertificate=False;ApplicationIntent=ReadWrite;"
                                        + "MultiSubnetFailover=False";

        public async Task<List<Student>> GetStudentByNameAsync(string studentName)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "SELECT * FROM student WHERE name LIKE '%" + studentName + "%';";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    // there can be multiple students under similar name
                    List<Student> uniList = new List<Student>();
                    while (reader.Read())
                    {
                        Student s = new Student(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
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
        public async Task<Student> GetStudentByOibAsync(int studentOib)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION))
            {
                connection.Open();
                string queryString = "SELECT * FROM student WHERE oib = " + studentOib.ToString() + ";";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Student student = new Student(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
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
        // retur -2 if error occured
        public async Task<int> PostStudentAsync(Student student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();

                    StudentRepository studentRepo = new StudentRepository();
                    Student check = await studentRepo.GetStudentByOibAsync(student.Oib);
                    if(student.Oib == check.Oib)
                    {
                        return -1;
                    }

                    UniversityRepository uniRepo = new UniversityRepository();
                    University uni = await uniRepo.GetUniByOibAsync(student.UniversityOib);
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
                    adapter.InsertCommand.ExecuteNonQuery();
                }
                return 1;
            }
            catch
            {
                return -2;
            }
        }

        public async Task<int> PutStudentAsync(Student student)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(CONNECTION))
                {
                    connection.Open();

                    UniversityRepository uniRepo = new UniversityRepository();
                    University uni = await uniRepo.GetUniByOibAsync(student.UniversityOib);
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
                    adapter.UpdateCommand.ExecuteNonQuery();
                
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
                    adapter.DeleteCommand.ExecuteNonQuery();
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
