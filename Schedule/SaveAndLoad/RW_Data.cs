using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schedule.Lessons;
using Schedule.SaveAndLoad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule.ReadWriteFiles
{
    class RW_Data
    {
        //public static string PATH = @"c:\temp"; // The @ symboll it for refer to the text , with out double sign , like to do \ , and not \\
        //public string FILE_NAME = "SerializationObjects.xml";
        //public static string DEFAULT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static bool WriteJSON(Data data)
        {
            try
            {
                var path = "";
                path = data.Path + "\\" + data.FileName + ".txt";

                string json = JsonConvert.SerializeObject(data);
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
        public static bool ReadJSON(ref Data data, string path = "")
        {
            try
            {
                // Now we can read the serialized book ...  
                Data result = null;
                if (path.Equals("")) path = data.Path + "\\" + data.FileName + ".txt";

                //JObject o1 = JObject.Parse(File.ReadAllText(path));
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    result = JsonConvert.DeserializeObject<Data>(json);
                }

                if (result != null)
                {
                    data = result;
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
        public static bool WriteXML(Data data)
        {
            try
            {
                var path = "";
                path = data.Path + "\\" + data.FileName + ".xml";
                var writer = new System.Xml.Serialization.XmlSerializer(typeof(Data));
                var wfile = new System.IO.StreamWriter(path);
                writer.Serialize(wfile, data);
                wfile.Close();
                System.Windows.Forms.MessageBox.Show("המידע שלך נשמר בהצלחה", "השמירה הצליחה");
                return true;
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("ישנה בעיה כלשהי\nפרטים נוספים:\n" + exp.Message, "השמירה נכשלה");
                return false;
            }
        }
        // method to LOAD the user data of the program;
        public static bool ReadXML(ref Data data , string path = "")
        {
            try
            {
                // Now we can read the serialized book ...  
                System.Xml.Serialization.XmlSerializer reader =
                    new System.Xml.Serialization.XmlSerializer(typeof(Data));

                if(path.Equals(""))
                    path = data.Path + "\\" + data.FileName + ".xml";

                System.IO.StreamReader file = new System.IO.StreamReader(path);
                Data result = (Data)reader.Deserialize(file);
                file.Close();
                if (result != null)
                {
                    data = result;
                    System.Windows.Forms.MessageBox.Show("המידע שלך נטען בהצלחה", "הטעינה הצליחה");
                    return true;
                }
                else {
                    System.Windows.Forms.MessageBox.Show("לא נמצא מידע!", "הטעינה נכשלה");
                    return false;
                }
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("פעולת הטעינה נכשלה.\nפרטים נוספים:\n" + exp, "הטעינה נכשלה");
                return false;
            }
        }
        // method to LOAD the user data of the program;
        public static bool ReadXMLFromFile(ref Data data)
        {
            try
            {
                Data result = null;
                string path = getFileFromUser();
                ReadXML(ref result, path);
                if (result != null)
                {
                    data = result;
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



        public static string getFileFromUser()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return ofd.FileName;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
