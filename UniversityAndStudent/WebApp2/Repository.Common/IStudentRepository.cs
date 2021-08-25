using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IStudentRepository
    {
        Task<List<IStudent>> GetAllStudentsAsync(string sort, string order);
        Task<List<IStudent>> GetAllStudentsAsync(int pageNum, int pageSize);
        //Task<List<IUniversity>> GetAllStudentsAsync(string attribute, string operators, string filter);
        Task<List<IStudent>> GetStudentByNameAsync(string studentName);
        Task<IStudent> GetStudentByOibAsync(int studentOib);
        Task<int> PostStudentAsync(IStudent student);
        Task<int> PutStudentAsync(IStudent student);
        Task<bool> DeleteStudentByOibAsync(int studentOib);
    }
}
