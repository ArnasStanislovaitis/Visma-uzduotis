using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaTask
{
    public static class DB
    {
        const string INDEX_FILE_NAME = "indeksai.json";
        const string Data_File_Name = "duomenys.json";
        const string User_File_Name = "users.json";

        public static Index Index = new Index();
        

        public static List<Meeting> Meetings = new List<Meeting>();
        public static List<User> Users = new List<User>();
        public static User CurrentUser = null;

        public static void SaveUsers()
        {
            var textData = JsonConvert.SerializeObject(Users);
            File.WriteAllText(User_File_Name, textData);
            SaveIndex();
        }
        public static void LoadUsers()
        {
            if (File.Exists(User_File_Name))
            {
                var textDataFromFile = File.ReadAllText(User_File_Name);
                var objectData = JsonConvert.DeserializeObject<List<User>>(textDataFromFile);
                Users = objectData;
            }
        }
        public static void SaveChanges()
        {
            var textData = JsonConvert.SerializeObject(Meetings);
            File.WriteAllText(Data_File_Name, textData);
            SaveIndex();
        }
        public static void Load()
        {
            if (File.Exists(Data_File_Name))
            {
                var textDataFromFile = File.ReadAllText(Data_File_Name);
                var objectData = JsonConvert.DeserializeObject<List<Meeting>>(textDataFromFile);
                Meetings = objectData;
            }
        }


        public static void SaveIndex()
        {
            var textData = JsonConvert.SerializeObject(Index);
            File.WriteAllText(INDEX_FILE_NAME, textData);
            
        }
        public static void LoadIndex()
        {
            if (File.Exists(INDEX_FILE_NAME))
            {
                var textDataFromFile = File.ReadAllText(INDEX_FILE_NAME);
                var objectData = JsonConvert.DeserializeObject <Index>(textDataFromFile);
                Index = objectData;
            }
        }
    }
}
