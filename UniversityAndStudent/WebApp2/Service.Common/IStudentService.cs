using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IStudentService
    {
        Task<List<IStudent>> GetAllStudentsAsync(string sort, string order);
        Task<List<IStudent>> GetAllStudentsAsync(int pageNum, int pageSize);
        //Task<List<IStudent>> GetAllStudentsAsync(string attribute, string operators, string filter);
        Task<List<IStudent>> GetStudentByNameAsync(string studentName);
        Task<IStudent> GetStudentByNameAsync(int studentOib);
        Task<int> PostStudentAsync(IStudent student);
        Task<int> PutStudentAsync(IStudent student);
        Task<bool> DeleteStudentByOibAsync(int studentOib);
    }
}
