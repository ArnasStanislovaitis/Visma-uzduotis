using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaTask
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Password { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
