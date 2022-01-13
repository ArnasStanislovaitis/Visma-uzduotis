using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaTask
{
    public static class MeetingController
    {
       public static void Create()
        {
            Console.Clear();
            Console.WriteLine("Creating meeting...");
            var testas = new Meeting()
            {
                Name = "Testas",
                ResponsiblePerson = "TestPerson",
                Description = "TestDescription",
                Category = Category.Short,
                Type = Type.InPerson,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };
            DB.Meetings.Add(testas);
            DB.SaveChanges();

        }
        public static void Delete()
        {
            Console.Clear();
            Console.WriteLine( "Deleting meeting...");
        }
        public static void AddPerson()
        {
            Console.Clear();
            Console.WriteLine("Adding a new person to the meeting");
        }
        public static void RemovePerson()
        {
            Console.Clear();
            Console.WriteLine("Removing a person from the meeting");
        }
        public static void GetAll()
        {
            Console.Clear();
            Console.WriteLine("Showing all meetings...");
        }

    }
}
