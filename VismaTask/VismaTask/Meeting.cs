using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaTask
{
    public enum Category
        {
            CodeMonkey,
            Hub,
            Short,
            TeamBuilding
        }
    public enum Type
        {
            Live,
            InPerson
        }

    public class Meeting
    {
        public string Name { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
        public Type Type { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> People { get; set; } = new List<string>();   

    }
}
