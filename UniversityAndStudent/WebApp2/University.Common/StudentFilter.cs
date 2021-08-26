using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityAndStudent.Common
{
    public class StudentFilter
    {
        public string Column { get; set; }
        public string Operator { get; set; }
        public string Filter { get; set; }
        public StudentFilter() { }
        public StudentFilter(string column, string operators, string filter)
        {
            Column = column;
            Operator = operators;
            Filter = filter;
        }

        public string StudentFiltering()
        {
            if (Operator == "like")
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
