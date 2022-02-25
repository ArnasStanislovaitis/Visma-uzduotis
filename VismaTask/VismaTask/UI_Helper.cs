using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaTask
{
    public static class UI_Helper
    {
        public static int AskForSelection(string[] variants, string question)
        {
            Console.Clear();
            bool exit = false;
            int selection = 0;
            while (!exit)
            {
                Console.WriteLine(question);
                for (int i = 0; i < variants.Length; i++)
                {
                    Console.WriteLine("{0} - {1}", i, variants[i]);
                }

                if (!int.TryParse(Console.ReadLine(), out selection) || selection > variants.Length-1 || selection < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect input.");
                    continue;
                }
                exit = true;
            }
            return selection;
        }
       public static string AskForString(string question)
       {
            Console.Clear();
            string rez = "";
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine(question);
                var input = Console.ReadLine();
                if(input == null)
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect input.");
                    continue;
                }
                exit = true;
                rez = input;

            }

            return rez;
                
       }
        public static int AskForUserId(string question)
        {
            Console.Clear();
            var users = DB.Users.Select(x => x.Name).ToArray();                           
            var selection = AskForSelection(users,question);
            var selectedUserName =users[selection];
            var userId = DB.Users.Where(x => x.Name == selectedUserName).Select(x => x.Id).FirstOrDefault();
            return userId;
        }

        public static DateTime AskForDate(string question)
        {
            Console.Clear();
            DateTime rez = DateTime.Now;
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine(question);
                var input = Console.ReadLine();
                if (!DateTime.TryParse(input,out rez))
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect input");
                    continue;
                }
                exit = true;
                

            }

            return rez;

        }

        
            
	    

        
        

	















        
        /*
        public static void UniversalSelectPrompt(string[] variants, Action[] actions )
        {
            bool exit = false;
            int selection = 0;

            while (!exit)
            {   
                Console.WriteLine("Choices :");
                for (int i = 0; i <variants.Length; i++)
                {
                    Console.WriteLine("{0} - {1}",i,variants[i]);
                }

                if (!int.TryParse(Console.ReadLine(), out selection) || selection > variants.Length || selection < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Netinkamai ivestas pasirinkimas.");
                    continue;
                }

                exit = true;
                actions[selection]();
            }          
            
        }*/


    }
}
