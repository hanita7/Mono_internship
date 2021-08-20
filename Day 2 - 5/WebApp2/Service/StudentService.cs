using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class StudentService
    {
        public StudentService() { }

        public async Task<List<Student>> GetStudentByNameAsync(string studentName)
        {
            StudentRepository studentRepo = new StudentRepository();
            return await studentRepo.GetStudentByNameAsync(studentName);
        }
        public async Task<Student> GetStudentByNameAsync(int studentOib)
        {
            StudentRepository studentRepo = new StudentRepository();
            return await studentRepo.GetStudentByOibAsync(studentOib);
        }

        public async Task<int> PostStudentAsync(Student student)
        {
            StudentRepository studentRepo = new StudentRepository();
            return await studentRepo.PostStudentAsync(student);
        }

        public async Task<int> PutStudentAsync(Student student)
        {
            StudentRepository studentRepo = new StudentRepository();
            return await studentRepo.PutStudentAsync(student);
        }

        public async Task<bool> DeleteStudentByOibAsync(int studentOib)
        {
            StudentRepository studentRepo = new StudentRepository();
            return await studentRepo.DeleteStudentByOibAsync(studentOib);
        }
    }
}
