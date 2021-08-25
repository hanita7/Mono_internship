using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAndStudent.Common
{
    public class Sort
    {
        public Sort() { }

        public string SortBy(string sort, string order)
        {
            return "ORDER BY " + sort + " " + order.ToUpper();
        }
    }
}
