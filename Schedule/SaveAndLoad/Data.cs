using Schedule.Lessons;
using Schedule.ReadWriteFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.SaveAndLoad
{
    [Serializable()]
    public class Data
    {
        public static string[] SAVE_NAMES = { "DATA0", "DATA1", "DATA2", "DATA3", "DATA4", "DATA5" , "DATA6", "DATA7", "DATA8" , "DATA9", "DATA10"};
        public List<Lesson> importCourses { get; set; }   
        public List<Lesson> checkedCourses { get; set; }  
        public List<Lesson> checkedLessonsFromCourses { get; set; }

        public DateTime datetime { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string userName { get; set; }
        public int numberSave { get; set; }

        public Data() {
            this.userName = "Unkown";
            datetime = DateTime.Now;
            numberSave = 0;
            this.FileName = "Serialization_" + SAVE_NAMES[numberSave];
            this.Path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            importCourses = new List<Lesson>();
            checkedCourses = new List<Lesson>();
            checkedLessonsFromCourses = new List<Lesson>();
        }
        public Data(string userName, DateTime datetime, string Path = "", int numberSave = 0)
        {
            this.userName = userName;
            this.numberSave = numberSave;
            changeFileName(datetime);
            if (Path.Equals(""))
            {
                this.Path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
            else
            {
                this.Path = Path;
            }
            if (!(numberSave >= 0 && numberSave < SAVE_NAMES.Length))
                numberSave = 0;
            this.FileName = "Serialization_" + SAVE_NAMES[numberSave];
            importCourses = new List<Lesson>();
            checkedCourses = new List<Lesson>();
            checkedLessonsFromCourses = new List<Lesson>();
        }

        public void changeFileName(DateTime dt)
        {
            datetime = dt;
        }
        public string getDate()
        {
            return datetime.ToString("dd.MM.yyyy");
        }
        public string getTime()
        {
            return datetime.ToString("HH.mm");
        }

        public void SaveData()
        {
            RW_Data.WriteJSON(this);
        }

        public Data LoadData(int i = 0)
        {
            Data d = new Data();
            d.numberSave = i;
            this.FileName = "Serialization_" + SAVE_NAMES[numberSave];
            RW_Data.ReadJSON(ref d);
            return d;
        }
    }
}
