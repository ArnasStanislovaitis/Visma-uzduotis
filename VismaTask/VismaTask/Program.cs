using VismaTask;


bool exit = false;
int selection = 0;

DB.Load();
DB.LoadUsers();
DB.LoadIndex();

var variants = new string[]
{
    "Create a  new meeting",
    "Delete the meeting",
    "Add a new person to the meeting",
    "Remove a person from the meeting",
    "View all meetings",
    "Exit"
};
var loginVariants = new string[]
{
    "Login",
    "Sign up",   
    
};

/*
var actions = new Action[]
{
    () =>MeetingController.Create(),
    () => MeetingController.Delete(),
    () => MeetingController.AddPerson(),
    () => MeetingController.RemovePerson(),
    () => MeetingController.GetAll(),
    () => {Console.Clear(); exit = true; }  
};
while (!exit)
{
    UI_Helper.UniversalSelectPrompt(selections, actions);
}*/

selection = UI_Helper.AskForSelection(loginVariants,"Choices");

if (selection == 0)
{
   Console.Clear();
   MeetingController.Login();
}
if (selection == 1)
{
    Console.Clear();
    MeetingController.Register();
}





while (exit!=true)
{
    selection = UI_Helper.AskForSelection(variants,"Actions :");      

    if(selection == 0)
    {
        MeetingController.Create();
    }
    if (selection ==1)
    {
        MeetingController.Delete();
    }
    if (selection == 2)
    {
        MeetingController.AddPerson();
    }
    if (selection == 3)
    {
        MeetingController.RemovePerson();
    }
    if (selection == 4)
    {
        MeetingController.GetAll();
    }
    if (selection == 5)
    {
        Console.Clear();
        exit = true;    }    

}

