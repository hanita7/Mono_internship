using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityAndStudent.Common;

namespace Repository.Common
{
    public interface IUniversityRepository
    {
        Task<List<IUniversity>> GetAllUnisAsync(Sort sorter);
        Task<List<IUniversity>> GetAllUnisAsync(Paging pager);
        Task<List<IUniversity>> GetAllUnisAsync(UniversityFilter uniFilter);
        Task<List<IUniversity>> GetUniByNameAsync(string uniName);
        Task<IUniversity> GetUniByOibAsync(int uniOib);
        Task<bool> PostUniversityAsync(IUniversity uni);
        Task<bool> PutUniversityAsync(IUniversity uni);
        Task<bool> DeleteUniByOibAsync(int uniOib);
        Task<bool> DeleteUniByNameAsync(string uniName);
    }
}