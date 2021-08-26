using Model.Common;
using Repository.Common;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAndStudent.Common;

namespace Service
{
    public class StudentService: IStudentService
    {
        protected IStudentRepository StudentRepo { get; set; }

        public StudentService(IStudentRepository studentRepo) 
        {
            StudentRepo = studentRepo;
        }

        public async Task<List<IStudent>> GetAllStudentsAsync(Sort sorter)
        {
            return await StudentRepo.GetAllStudentsAsync(sorter);
        }
        public async Task<List<IStudent>> GetAllStudentsAsync(Sort sorter,Paging pager)
        {
           return await StudentRepo.GetAllStudentsAsync(sorter, pager);
        }
        public async Task<List<IStudent>> GetAllStudentsAsync(StudentFilter studentFilter)
        {
            return await StudentRepo.GetAllStudentsAsync(studentFilter);
        }
        public async Task<List<IStudent>> GetStudentByNameAsync(string studentName)
        {
            return await StudentRepo.GetStudentByNameAsync(studentName);
        }
        public async Task<IStudent> GetStudentByNameAsync(int studentOib)
        {
            return await StudentRepo.GetStudentByOibAsync(studentOib);
        }

        public async Task<int> PostStudentAsync(IStudent student)
        {
            return await StudentRepo.PostStudentAsync(student);
        }

        public async Task<int> PutStudentAsync(IStudent student)
        {
            return await StudentRepo.PutStudentAsync(student);
        }

        public async Task<bool> DeleteStudentByOibAsync(int studentOib)
        {
            return await StudentRepo.DeleteStudentByOibAsync(studentOib);
        }
    }
}
