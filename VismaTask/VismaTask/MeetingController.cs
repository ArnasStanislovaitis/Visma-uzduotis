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
                Id = DB.Index.NextMeetingId++,
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
            var variant = DB.Meetings.Select(x => x.Name).ToArray();
            var selection = UI_Helper.AskForSelection(variant,"Choose a meeting you want to delete :");
            var meeting = DB.Meetings[selection];
            if (meeting.ResponsiblePersonId == DB.CurrentUser.Id)
            {
                DB.Meetings.Remove(meeting);
                DB.SaveChanges();
                Console.Clear();
                Console.WriteLine("Meeting {0} deleted",meeting.Name);
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Meeting {0} cannot be deleted. You don't have access",meeting.Name);
                Console.ReadKey();

            }

        }
        public static void AddPerson()
        {
            Console.Clear();
            var meetingVariants = DB.Meetings.Select(x => x.Name).ToArray();
            var meetingSelection = UI_Helper.AskForSelection(meetingVariants, "Choose a meeting:");
            var meeting= DB.Meetings[meetingSelection];
            
            var userVariants = DB.Users.Select(x => x.Name).ToArray();
            var userSelection = UI_Helper.AskForSelection(userVariants, "Choose a person to add to the meeting:");
            var user = DB.Users[userSelection];

            var intersects = DB.Meetings.Where(x => x.People.Contains(user) && meeting.Between(x.StartDate,x.EndDate)).ToList();

            if (!meeting.People.Contains(user))
            {   Console.Clear();
                var key = ConsoleKey.Y;

                if (intersects.Count > 0)
                {                    
                    foreach(var item in intersects)
                    {
                        Console.WriteLine("This meeting collides with the meeting {0}",item.Name);
                    }
                    Console.WriteLine("Do you want to continue? Y/N");
                    key = Console.ReadKey().Key;                   
                }
                if(key == ConsoleKey.Y)
                {
                    meeting.People.Add(user);
                    Console.Clear();
                    Console.WriteLine("Add {0} to the meeting {1} at {2}",user.Name,meeting.Name,meeting.StartDate);
                    Console.ReadKey();
                }


            }
            else
            {
                Console.Clear();
                Console.WriteLine("Person {0} is already in the meeting {1}",user.Name,meeting.Name);
                Console.ReadKey();
            }
            

        }
        public static void RemovePerson()
        {
            var meetingVariants = DB.Meetings.Select(x => x.Name).ToArray();
            var meetingSelection = UI_Helper.AskForSelection(meetingVariants, "Choose a meeting:");
            var meeting = DB.Meetings[meetingSelection];

            var userVariants = DB.Users.Select(x => x.Name).ToArray();
            var userSelection = UI_Helper.AskForSelection(userVariants, "Choose a person to add to the meeting:");
            var user = meeting.People[userSelection];

            if(meeting.ResponsiblePersonId != user.Id)
            {
                meeting.People.Remove(user);
                DB.SaveChanges();
                Console.WriteLine("User {0} has been removed from the meeting {1}",user.Name,meeting.Name);
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Cannot remove the meeting creator");
                Console.ReadKey();
            }

        }
        public static void GetAll()
        {

            var screen = DB.Meetings;
            bool exit = false;
            while (!exit)
            {   
                Console.Clear();
                Console.WriteLine("Showing all the meetings");
                Console.WriteLine("X-escape");
                Console.WriteLine("f - filter");
                Console.WriteLine("Showing all meetings...");
                screen.ForEach(x => Console.WriteLine(x));
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.X) exit = true;
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
        
        public static bool Between(this Meeting current, DateTime start, DateTime end)
        {
            bool startCheck = start <= current.StartDate && current.StartDate <= end;
            bool endCheck = start <= current.EndDate && current.EndDate <= end;
            return startCheck || endCheck;
        }

    }
}
