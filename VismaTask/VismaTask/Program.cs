using VismaTask;


bool exit = false;
int selection = 0;

DB.Load();

while (exit!=true)
{
    Console.WriteLine("Commands:");
    Console.WriteLine("1 - Create a  new meeting");
    Console.WriteLine("2 - Delete the meeting");
    Console.WriteLine("3 - Add a new person to the meeting");
    Console.WriteLine("4 - Remove a person from the meeting");
    Console.WriteLine("5 - View all meetings");
    Console.WriteLine("6 - Exit");

    if(!int.TryParse(Console.ReadLine(), out selection) || selection > 6 || selection < 1)
    {
        Console.Clear();
        Console.WriteLine("Netinkamai ivestas pasirinkimas.");
        continue;
    }

    if(selection == 1)
    {
        MeetingController.Create();

    }
    if (selection ==2)
    {
        MeetingController.Delete();
    }
    if (selection == 3)
    {
        MeetingController.AddPerson();
    }
    if (selection == 4)
    {
        MeetingController.RemovePerson();
    }
    if (selection == 5)
    {
        MeetingController.GetAll();
    }
    if (selection == 6)
    {
        Console.Clear();
        exit = true;
    }
}