using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Schedule.Lessons;
using System.Text.RegularExpressions;

namespace Schedule.Excel
{
    public class ExcelOperation
    {
        static string connection = "";
        static string[] sheetNames;

        public static string getExcelFileFromUser()
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xls*", ValidateNames = true })
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
        
        public static DataSet ImportExcelXLS(string FileName, bool hasHeaders = false)
        {
            string HDR = hasHeaders ? "YES" : "NO";
            connection = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;data source='{0}';Extended Properties=\"Excel 12.0;HDR={1};IMEX=1\"", FileName, HDR);

            DataSet output = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();
                sheetNames = GetExcelSheetNames_byProcess(connection);

                DataTable schemaTable = conn.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow schemaRow in schemaTable.Rows)
                {
                    string sheet = schemaRow["TABLE_NAME"].ToString();

                    if (!sheet.EndsWith("_"))
                    {
                        try
                        {
                            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                            cmd.CommandType = CommandType.Text;

                            DataTable outputTable = new DataTable(sheet);
                            output.Tables.Add(outputTable);
                            new OleDbDataAdapter(cmd).Fill(outputTable);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, FileName), ex);
                        }
                    }
                }
            }
            return output;
        }

        public static string[] GetExcelSheetNames()
        {
            return sheetNames;
        }

        private static string[] GetExcelSheetNames_byProcess(string connectionString)
        {
            OleDbConnection con = null;
            DataTable dt = null;
            con = new OleDbConnection(connectionString);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
                return null;

            String[] excelSheetNames = new String[dt.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString();
                i++;
            }

            return excelSheetNames;
        }

        public static LessonList parserExcelFile(DataTable data)
        {
            LessonList result = new LessonList();
            string[] colNames = getIndexProperties(data);
            if (colNames == null) return null;

            string name = "";
            int year = 4;

            foreach (DataRow row in data.Rows)
            {
                string lecturer = "", type = "", day = "", place = "";
                int number = -1, hours = -1, start = -1, end = -1;

                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (colNames[i] != null && !row[colNames[i]].Equals(""))
                        {
                            string text = row[colNames[i]].ToString();
                            Regex regex = new Regex(@"(שנה (- )?(\w)('))");
                            Match match = regex.Match(text);
                            if (match.Success)
                                text = match.ToString();
                            switch (text)
                            {
                                case "1":
                                case "א":
                                case "א'":
                                case "שנה א":
                                case "שנה א'":
                                case "שנה - א":
                                case "שנה - א'":
                                    year = 1;
                                    break;
                                case "2":
                                case "ב":
                                case "ב'":
                                case "שנה ב":
                                case "שנה ב'":
                                case "שנה - ב":
                                case "שנה - ב'":
                                    year = 2;
                                    break;
                                case "3":
                                case "ג":
                                case "ג'":
                                case "שנה ג":
                                case "שנה ג'":
                                case "שנה - ג":
                                case "שנה - ג'":
                                    year = 3;
                                    break;
                                case "4":
                                case "ד":
                                case "ד'":
                                case "שנה ד":
                                case "שנה ד'":
                                case "שנה - ד":
                                case "שנה - ד'":
                                    year = 4;
                                    break;
                                default:
                                    break;
                            }

                            switch (i)
                            {
                                case 0: // year
                                    break;
                                case 1: // course name
                                    if(!text.Equals(""))
                                        name = text;
                                    break;
                                case 2: // lecturer
                                    lecturer = text;
                                    break;
                                case 3:// day  !work
                                    switch (text)
                                    {
                                        case "א":
                                        case "א'":
                                        case "ראשון":
                                        case "1":
                                            day = "א";
                                            break;
                                        case "ב":
                                        case "ב'":
                                        case "שני":
                                        case "2":
                                            day = "ב";
                                            break;
                                        case "ג":
                                        case "ג'":
                                        case "שלישי":
                                        case "3":
                                            day = "ג";
                                            break;
                                        case "ד":
                                        case "ד'":
                                        case "רביעי":
                                        case "4":
                                            day = "ד";
                                            break;
                                        case "ה":
                                        case "ה'":
                                        case "חמישי":
                                        case "5":
                                            day = "ה";
                                            break;
                                        case "ו":
                                        case "ו'":
                                        case "שישי":
                                        case "6":
                                            day = "ו";
                                            break;
                                    }
                                    break;
                                case 4:// start  !work
                                    start = returnTime(text, true);
                                    break;
                                case 5:// end  !work
                                    end = returnTime(text, false);
                                    break;
                                case 6: //week hours 
                                    try {
                                        hours = end - start;
                                        if(hours <= 0)
                                            hours = Int32.Parse(text);
                                    }
                                    catch {
                                        hours = 0;
                                    };
                                    break;
                                case 7: // class name
                                    place = text;
                                    break;
                                case 8: // number lesson 
                                    try {
                                        number = Int32.Parse(text);
                                    }
                                    catch {
                                        number = 1;
                                    };
                                    break;
                                case 9: // type
                                    switch (text)
                                    {
                                        case "הרצאה":
                                        case "ה":
                                            type = "ה";
                                            break;
                                        case "תרגול":
                                        case "ת":
                                            type = "ת";
                                            break;
                                        case "מעבדה":
                                        case "מ":
                                            type = "מ";
                                            break;
                                        default:
                                            type = "ת";
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                    Lesson l = new Lesson(name, type, number, lecturer, day, start, end, end - start, year, place);
                    result.add(l);
                }
                catch
                {

                }
            }

            return result;
        }

        private static int returnTime(string text, bool min = true)
        {
            Regex regex = new Regex(@"\d\d:\d\d");
            Match match = regex.Match(text);
            string hour1, hour2;
            int h1 = -1, h2 = -1;
            if (match.Success)
            {
                try
                {
                    hour1 = match.ToString();
                    hour1 = hour1.Substring(0, 2);
                    h1 = Int32.Parse(hour1);
                }
                catch
                {
                    h1 = 0;
                };
            }
            match = match.NextMatch();
            if (match.Success)
            {
                try
                {
                    hour2 = match.ToString();
                    hour2 = hour2.Substring(0, 2);
                    h2 = Int32.Parse(hour2);
                }
                catch
                {
                    h2 = 0;
                };
            }
            if (h2 > -1)
            {
                if (min)
                {
                    if (h1 > h2)
                        return h2;
                    return h1;
                }
                else
                {
                    if (h1 < h2)
                        return h2;
                    return h1;
                }
            }
            else
            {
                return h1;
            }
        }

        private static string[] getIndexProperties(DataTable dtData)
        {
            // 0        1       2       3       4       5       6       7       8       9
            // year     name    lec     day     start   end     hours   class   num     type
            string[] colNames = new string[10];
            
            foreach (DataRow row in dtData.Rows)
            {
                foreach (DataColumn col in dtData.Columns)
                {
                    Console.WriteLine(row[col.ColumnName].ToString());

                    switch (row[col].ToString())
                    {
                        case "שנה":
                            colNames[0] = col.ColumnName.ToString();
                            break;
                        case "שם קורס":
                        case "שם הקורס":
                        case "הקורס":
                        case "קורס":
                            colNames[1] = col.ColumnName.ToString();
                            break;
                        case "מרצה":
                        case "מתרגל":
                        case "מורה":
                            colNames[2] = col.ColumnName.ToString();
                            break;
                        case "יום":
                            colNames[3] = col.ColumnName.ToString();
                            break;
                        case "שעת התחלה":
                        case "התחלה":
                        case "מתחיל":
                        case "מתחיל ב":
                            colNames[4] = col.ColumnName.ToString();
                            break;

                        case "שעת סיום":
                        case "סיום":
                        case "מסתיים":
                        case "מסתים":
                        case "מסתיים ב":
                        case "מסתים ב":
                            colNames[5] = col.ColumnName.ToString();
                            break;

                        case "שעה":
                        case "שעות":
                            // "10:00-12:00" => start 10(10:00) | end 12(12:00)
                            colNames[4] = col.ColumnName.ToString();
                            colNames[5] = col.ColumnName.ToString();
                            break;

                        case "שעות שבועיות":
                        case "כמות שעות":
                        case "כמות שעות שבועיות":
                        case "שש":
                        case "ש\"ש":
                            colNames[6] = col.ColumnName.ToString();
                            break;
                        case "כיתה":
                        case "מקום":
                            colNames[7] = col.ColumnName.ToString();
                            break;
                        case "ק\"ה":
                        case "מספר":
                            colNames[8] = col.ColumnName.ToString();
                            break;

                        case "ה-ת-מ":
                        case "סוג":
                        case "הרצאה/תרגול/מעבדה":
                        case "הרצאה-תרגול-מעבדה":
                            colNames[9] = col.ColumnName.ToString();
                            break;

                        default:
                            break;
                    }
                    bool allFilled = true;
                    for (int i = 0; i < colNames.Length && allFilled; i++)
                    {
                        if(colNames[i] == null || colNames[i].Equals(""))
                        {
                            allFilled = false;
                        }
                    }

                    if (allFilled)
                    {
                        return colNames;
                    }
                }
            }
            return colNames;
           }
    }
}
