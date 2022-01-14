using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaTask
{
    public static class MeetingController
    {
        public static void Login()
        {
            bool exit = false;
            while (!exit)
            {
                string username = UI_Helper.AskForString("Please, enter your username ");
                string password = UI_Helper.AskForString("Please, enter your password ");

                var user = DB.Users.Where(x => x.Name == username).FirstOrDefault();

                if (user == null)
                {
                    Console.Clear();
                    Console.WriteLine("The username does not exist");
                    continue;
                }

                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    Console.Clear();
                    DB.CurrentUser = user;
                    exit = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect password.");
                    continue;
                }
            }
        }
        public static void Register()
        {
            bool exit = false;
            while (!exit)
            {
                string username = UI_Helper.AskForString("Please, enter your username ");
                string password = UI_Helper.AskForString("Please, enter your password : ");

                var user = DB.Users.Where(x => x.Name == username).FirstOrDefault();

                if (user != null)
                {
                    Console.Clear();
                    Console.WriteLine("The user already exists");
                    continue;
                }
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                var newUser = new User() { Name = username, Password = hashedPassword };

                DB.Users.Add(newUser);
                DB.SaveUsers();
                DB.CurrentUser = newUser;
                Console.Clear();
                exit = true;
            }
        }

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
