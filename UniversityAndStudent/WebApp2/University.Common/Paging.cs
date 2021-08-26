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
        public Paging() { }
        public Paging(string column, string order, int pageNum, int pageSize) 
        {
            PageNum = pageNum;
            PageSize = pageSize;
        }
        
        public string Pagination()
        {
            return " OFFSET " + ((PageNum - 1) * PageSize).ToString() + " ROWS FETCH NEXT "
                    + PageSize.ToString() + " ROWS ONLY";
        }
    }
}
