using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAndStudent.Common
{
    public class Paging
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public Paging(int pageNum, int pageSize) 
        {
            PageNum = pageNum;
            PageSize = pageSize;
        }
        
        public string Pagination()
        {
            return "LIMIT " + PageNum.ToString() + " OFFSET " + PageSize.ToString();
        }
    }
}
