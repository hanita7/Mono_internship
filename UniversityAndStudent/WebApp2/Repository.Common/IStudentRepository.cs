using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAndStudent.Common;

namespace Repository.Common
{
    public interface IStudentRepository
    {
        Task<List<IStudent>> GetAllStudentsAsync(Sort sorter);
        Task<List<IStudent>> GetAllStudentsAsync(Sort sorter,Paging pager);
        Task<List<IStudent>> GetAllStudentsAsync(StudentFilter studentFilter);
        Task<List<IStudent>> GetStudentByNameAsync(string studentName);
        Task<IStudent> GetStudentByOibAsync(int studentOib);
        Task<int> PostStudentAsync(IStudent student);
        Task<int> PutStudentAsync(IStudent student);
        Task<bool> DeleteStudentByOibAsync(int studentOib);
    }
}
