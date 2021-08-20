using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UniversityService
    {
        public UniversityService() { }

        public async Task<List<University>> GetUniByNameAsync(string uniName)
        {
            UniversityRepository uniRepo = new UniversityRepository();
            return await uniRepo.GetUniByNameAsync(uniName);
        }
        public async Task<University> GetUniByOibAsync(int uniOib)
        {
            UniversityRepository uniRepo = new UniversityRepository();
            return await uniRepo.GetUniByOibAsync(uniOib);
        }

        public async Task<bool> PostUniversityAsync(University uni)
        {
            UniversityRepository uniRepo = new UniversityRepository();
            return await uniRepo.PostUniversityAsync(uni);
        }

        public async Task<bool> PutUniversityAsync(University uni)
        {
            UniversityRepository uniRepo = new UniversityRepository();
            return await uniRepo.PutUniversityAsync(uni);
        }

        public async Task<bool> DeleteUniByOibAsync(int uniOib)
        {
            UniversityRepository uniRepo = new UniversityRepository();
            return await uniRepo.DeleteUniByOibAsync(uniOib);
        }
        public async Task<bool> DeleteUniByNameAsync(string uniName)
        {
            UniversityRepository uniRepo = new UniversityRepository();
            return await uniRepo.DeleteUniByNameAsync(uniName);
        }
    }
}
