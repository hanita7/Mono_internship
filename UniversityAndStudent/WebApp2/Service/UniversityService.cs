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
    public class UniversityService: IUniversityService
    {
        protected IUniversityRepository UniRepo { get; set; }
        public UniversityService(IUniversityRepository uniRepo) 
        {
            UniRepo = uniRepo;
        }

        public async Task<List<IUniversity>> GetAllUnisAsync(string sort, string order)
        {
            return await UniRepo.GetAllUnisAsync(sort, order);
        }
        public async Task<List<IUniversity>> GetAllUnisAsync(int pageNum, int pageSize)
        {
            return await UniRepo.GetAllUnisAsync(pageNum, pageSize);
        }
        public async Task<List<IUniversity>> GetAllUnisAsync(string attribute, string operators, string filter)
        {
            return await UniRepo.GetAllUnisAsync(attribute, operators, filter);
        }
        public async Task<List<IUniversity>> GetUniByNameAsync(string uniName)
        {
            return await UniRepo.GetUniByNameAsync(uniName);
        }
        public async Task<IUniversity> GetUniByOibAsync(int uniOib)
        {
            return await UniRepo.GetUniByOibAsync(uniOib);
        }

        public async Task<bool> PostUniversityAsync(IUniversity uni)
        {
            return await UniRepo.PostUniversityAsync(uni);
        }

        public async Task<bool> PutUniversityAsync(IUniversity uni)
        {
            return await UniRepo.PutUniversityAsync(uni);
        }

        public async Task<bool> DeleteUniByOibAsync(int uniOib)
        {
            
            return await UniRepo.DeleteUniByOibAsync(uniOib);
        }
        public async Task<bool> DeleteUniByNameAsync(string uniName)
        {
            
            return await UniRepo.DeleteUniByNameAsync(uniName);
        }
    }
}
