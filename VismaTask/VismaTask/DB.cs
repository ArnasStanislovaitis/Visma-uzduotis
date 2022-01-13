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
        const string File_Name = "duomenys.json";
       public static List<Meeting> Meetings = new List<Meeting>();

        public static void SaveChanges()
        {
            var textData = JsonConvert.SerializeObject(Meetings);
            File.WriteAllText(File_Name, textData);
        }
        public static void Load()
        {
            if (File.Exists(File_Name))
            {
                var textDataFromFile = File.ReadAllText(File_Name);
                var objectData = JsonConvert.DeserializeObject<List<Meeting>>(textDataFromFile);
                Meetings = objectData;
            }            

        }


    }
}
