using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAndStudent.Common
{
    public class Sort
    {
        public string Column { get; set; }
        public string Order { get; set; }
        public Sort() { }
        public Sort(string sort, string order) 
        {
            Column = sort;
            Order = order;
        }

        public string SortBy()
        {
            return "ORDER BY " + Column + " " + Order.ToUpper();
        }
    }
}
