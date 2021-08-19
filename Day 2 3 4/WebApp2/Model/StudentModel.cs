using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Student
    {
        public int Oib { get; set; }
        public string Name { get; set; }
        public int UniversityOib { get; set; }

        public Student() { }
        public Student(int oib, string name, int uniOib)
        {
            Oib = oib;
            Name = name;
            UniversityOib = uniOib;
        }
    }
}
