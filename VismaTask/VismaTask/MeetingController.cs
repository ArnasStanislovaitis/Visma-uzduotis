using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaTask
{
   
    public static class MeetingController
    {   
        public static string[] FILTER_VARIANTS =
        {
        "Filter by description",
        "Filter by responsible person",
        "Filter by category",
        "Filter by type",
        "Filter by date",
        "Filter by participants count"
        };

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

                var newUser = new User() {Id=DB.Index.NextUserId++ ,Name = username, Password = hashedPassword };

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
            var meeting = new Meeting()
            {
                Name = UI_Helper.AskForString("Enter the name of the meeting"),
                ResponsiblePersonId = DB.CurrentUser.Id,
                Description = UI_Helper.AskForString("Enter the description"),
                Category = (Category)UI_Helper.AskForSelection(Enum.GetNames(typeof(Category)),"Choose a meeting category"),
                Type = (Type)UI_Helper.AskForSelection(Enum.GetNames(typeof(Type)),"Choose a type of the meeting" +
                ""),
                StartDate = UI_Helper.AskForDate("Type in the start date of the meeting"),
                EndDate = UI_Helper.AskForDate("Type in end date of the meeting")
            };
            meeting.People.Add(DB.CurrentUser);
            DB.Meetings.Add(meeting);
            DB.SaveChanges();
            Console.Clear();
            Console.WriteLine("The meeting has been created !");

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

            var screen = DB.Meetings;
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Showing all the meetings");
                Console.WriteLine("Esc-escape");
                Console.WriteLine("f - filter");
                Console.WriteLine("Showing all meetings...");
                DB.Meetings.ForEach(x => Console.WriteLine(x));
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape) exit = true;
                if (key.Key == ConsoleKey.F)
                {
                    var selection = UI_Helper.AskForSelection(FILTER_VARIANTS, "Choose filter :");
                    var text = UI_Helper.AskForString("Enter text for filtering:");

                    if (selection == 0)
                    {
                        screen = screen.Where(x => x.Description.Contains(text)).ToList();
                    }
                    if (selection == 1)
                    {
                        screen = screen.Where(x => x.People.Select(x => x.Name).Contains(text)).ToList();
                    }
                    if (selection == 2)
                    {
                        screen = screen.Where(x => x.Category.ToString().Contains(text)).ToList();
                    }
                    if (selection == 3)
                    {
                        screen = screen.Where(x => x.Type.ToString().Contains(text)).ToList();
                    }
                    if (selection == 4)
                    {
                        screen = screen.Where(x => x.StartDate.ToShortDateString().Contains(text) || x.EndDate.ToShortDateString().Contains(text)).ToList();
                    }
                    if (selection == 5)
                    {
                        var count = 0;
                        if (int.TryParse(text, out count))
                        {
                            screen = screen.Where(x => x.People.Count > count).ToList();
                        }
                    }
                }
            }
        }

    }
}
