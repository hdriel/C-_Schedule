using Newtonsoft.Json;
using Schedule.Lessons;
using Schedule.SaveAndLoad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.LinkLabel;

namespace Schedule.ReadWriteFiles
{
    class RW_Savements
    {
        //public static string PATH = @"c:\temp"; // The @ symboll it for refer to the text , with out double sign , like to do \ , and not \\
        public static string FILE_NAME = "Serialization_Savements"; // .xml
        public static string DEFAULT_PATH = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static bool WriteJSON(List<Savement> saves)
        {
            try
            {
                var path = "";
                path = DEFAULT_PATH + "\\" + FILE_NAME + ".txt";

                string json = JsonConvert.SerializeObject(saves);
                //write string to file
                System.IO.File.WriteAllText(path, json);

                /*
                //open file stream
                using (StreamWriter file = File.CreateText(@"D:\path.txt"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    serializer.Serialize(file, _data);
                }
                */

                System.Windows.Forms.MessageBox.Show("המידע שלך נשמר בהצלחה", "השמירה הצליחה");
                return true;
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("ישנה בעיה כלשהי\nפרטים נוספים:\n" + exp.Message, "השמירה נכשלה");
                return false;
            }
        }
        public static bool ReadJSON(ref List<Savement> saves, string path = "")
        {
            try
            {
                // Now we can read the serialized book ...  
                List<Savement> result = null;
                if (path.Equals("")) path = DEFAULT_PATH + "\\" + FILE_NAME + ".txt";

                //JObject o1 = JObject.Parse(File.ReadAllText(path));
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    result = JsonConvert.DeserializeObject<Savement[]>(json).ToList();
                }

                if (result != null)
                {
                    saves = result;
                    System.Windows.Forms.MessageBox.Show("המידע שלך נטען בהצלחה", "הטעינה הצליחה");
                    return true;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("לא נמצא מידע!", "הטעינה נכשלה");
                    return false;
                }
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("פעולת הטעינה נכשלה.\nפרטים נוספים:\n" + exp.Message, "הטעינה נכשלה");
                return false;
            }
        }


        // method to SAVE the user data of the program;
        public static bool WriteXML(List<Savement> saves)
        {
            try
            {
                var path = "";
                path = DEFAULT_PATH + "\\" + FILE_NAME;
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, saves);
                }
                return true;
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("התוכנה לא הצליחה לשמור את השמירות שלך.\nפרטים נוספים:\n" + exp.Message, "שגיאה");
                return false;
            }
        }
        // method to LOAD the user data of the program;
        public static bool ReadXML(ref List<Savement> saves)
        {
            try {
                var path = "";
                path = DEFAULT_PATH + "\\" + FILE_NAME;
                List<Savement> result;
                
                /*
                XmlSerializer ser = new XmlSerializer(typeof(List<Savement>));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    result = (List<Savement>)ser.Deserialize(fs);
                }
                */
                            
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    result = (List<Savement>)bin.Deserialize(stream);
                }
                saves = new List<Savement>();
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].panel != null)
                        saves.Add(result[i]);
                }

                if (result != null)
                {
                    //saves = result;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("התוכנה לא הצליחה לקרוא את השמירות שלך.\nפרטים נוספים:\n" + exp.Message, "שגיאה");
                return false;
            }
        }
    }
}
