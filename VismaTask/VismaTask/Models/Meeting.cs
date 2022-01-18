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
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResponsiblePersonId { get; set; }
        public string Description { get; set; }

        public Category Category { get; set; }
        public Type Type { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<User> People { get; set; } = new List<User>();

        public override string ToString()
        {
            var p0 = Name;
            var p1 = DB.Users.Where(x=> x.Id==ResponsiblePersonId).Select(x=>x.Name).FirstOrDefault();
            var p2 = Description;
            var p3 = Category;
            var p4 = Type;
            var p5 = StartDate.ToShortDateString();
            var p6 = EndDate.ToShortDateString();
            var p7 = People.Count();

            return String.Format("{0,-30}|{1,-20}|{2,-35}|{3,-10}|{4,-8}|{5,-10}|{6,-10}|{7,3}", 
                p0,p1,p2,p3,p4,p5,p6,p7);
        }

    }
}
