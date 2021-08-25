using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IUniversityService
    {
        Task<List<IUniversity>> GetAllUnisAsync(string sort, string order);
        Task<List<IUniversity>> GetAllUnisAsync(int pageNum, int pageSize);
        Task<List<IUniversity>> GetAllUnisAsync(string attribute, string operators, string filter);
        Task<List<IUniversity>> GetUniByNameAsync(string uniName);
        Task<IUniversity> GetUniByOibAsync(int uniOib);
        Task<bool> PostUniversityAsync(IUniversity uni);
        Task<bool> PutUniversityAsync(IUniversity uni);
        Task<bool> DeleteUniByOibAsync(int uniOib);
        Task<bool> DeleteUniByNameAsync(string uniName);
    }
}
