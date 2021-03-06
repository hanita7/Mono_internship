using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAndStudent.Common
{
    public class UniversityFilter
    {
        public string Column { get; set; }
        public string Operator { get; set; }
        public string Filter { get; set; }
        public UniversityFilter() { }
        public UniversityFilter(string column, string operators, string filter) 
        {
            Column = column;
            Operator = operators;
            Filter = filter;
        }

        public string UniFiltering()
        {
            if(Operator == "like")
            {
                Operator = " LIKE ";
                return " WHERE " + Column + Operator + "'%" + Filter + "%'";
            }
            else
            {
                Operator = " = ";
                return " WHERE " + Column + Operator + Filter;
            }
        }
    }
}
