using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class University
    {
        public int Oib { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public University() { }
        public University(int oib, string name, string address)
        {
            Oib = oib;
            Name = name;
            Address = address;
        }
    }
}
