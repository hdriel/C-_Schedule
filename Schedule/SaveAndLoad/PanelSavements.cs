using Schedule.ReadWriteFiles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Schedule.SaveAndLoad
{
    //[XmlRoot("Configuration")]
    public class PanelSavements
    {
        public Data dataSavement { get; set; }
        public System.Windows.Forms.Panel panel { get; set; }

        //[XmlArray("SavementList"), XmlArrayItem(typeof(Savement), ElementName = "Savement")]
        public List<Savement> saves { get; set; }

        private Button btnFromFile { get; set; }
        private List<Bunifu.Framework.UI.BunifuSeparator> colsSeparetors { get; set; }
        private List<Bunifu.Framework.UI.BunifuSeparator> rowsSeparetors { get; set; }
        private List<Bunifu.Framework.UI.BunifuCustomLabel> titles { get; set; }
        
        private enum WIDTHS { SAVE = 90, LOAD = 90, RESET = 55, DATE = 123, TIME = 104, NUMBER = 50, NAME = 280 };
        private static string[] TITLES = { "איפוס", "שמירה", "טעינה", "שעה", "תאריך", "שם השמירה" , "מספר" };
        private static int[] WIDTHS_TITLES = { (int)WIDTHS.RESET , (int)WIDTHS.SAVE, (int)WIDTHS.LOAD, (int)WIDTHS.TIME, (int)WIDTHS.DATE, (int)WIDTHS.NAME, (int)WIDTHS.NUMBER };
        private static int COL_SPACE = 20;
        private static int ROW_SPACE = 15;
        public static string TEXT_FONT = "Guttman Hatzvi";
        public static int TEXT_SIZE = 11;
        public static int HEIGHT_ROW = 25;

        public static int PANEL_LOC_Y= 138;
        public static int PANEL_LOC_X = 44;
        // ----------------------------------------------------------------------------------------------------------------------

        public PanelSavements() { }
        public PanelSavements(Form1 mainForm)
        {
            Savement newSave = new Savement(mainForm, true);
            newSave.data = dataSavement;
            newSave.ListSavementsPanel = this;
            if (saves == null || saves.Count <= 0)
            {
                saves = new List<Savement>();
                List<Savement> s = new List<Savement>();
                //RW_Savements.ReadJSON(ref s); 
                saves =s;
                saves.Add(newSave);
            }
            else
            {
                saves.Add(newSave);
            }

            panel = new System.Windows.Forms.Panel();
            panel.AutoScroll = true;
            panel.AutoSize = false;
            panel.Height = 450;
            panel.Width = 1040;
            panel.Location = new Point(PANEL_LOC_X, PANEL_LOC_Y);
            initTable();


            btnFromFile = new Button();
            btnFromFile.AutoSize = false;
            btnFromFile.Width = 100;
            btnFromFile.BackColor = Color.Gold;
            btnFromFile.Height = 50;
            btnFromFile.Location = new Point(0, panel.Height - btnFromFile.Height);
            btnFromFile.Text = "בחר מקובץ";
            btnFromFile.Click += ((s,e) => {
                Data data = null;
                RW_Data.ReadJSON(ref data, RW_Data.getFileFromUser());
                if(data != null)
                {
                    mainForm.dataProgram = data;
                    mainForm.updateDateFromSavement();
                }
            });
            panel.Controls.Add(btnFromFile);
        }

        private int getWidthTable()
        {
            if (titles == null) return 0;
            int totalWidth = COL_SPACE;
            for (int i = 0; i < TITLES.Length; i++)
            {
                totalWidth += titles[i].Width + COL_SPACE;
            }
            return totalWidth;
        }
        private void initTable()
        {
            init_titles();
            init_separetors_cols();
            init_savements();
            init_separetors_rows();
        }
        private void init_titles()
        {
            titles = new List<Bunifu.Framework.UI.BunifuCustomLabel>();
            for (int i = 0; i < TITLES.Length; i++)
            {
                Bunifu.Framework.UI.BunifuCustomLabel l = new Bunifu.Framework.UI.BunifuCustomLabel();
                l.BackColor = Color.Transparent;
                l.AutoSize = false;
                l.Font = new Font(TEXT_FONT, TEXT_SIZE, FontStyle.Bold);
                l.ForeColor = Color.Snow;
                l.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                l.Text = TITLES[i];
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Width = WIDTHS_TITLES[i];
                l.Height = HEIGHT_ROW;
                titles.Add(l);
                if (i == 0)
                    l.Location = new Point(COL_SPACE, 0);
                else {
                    l.Location = new Point(titles.ElementAt(i-1).Location.X + titles.ElementAt(i - 1).Width + COL_SPACE, 0 );
                }
                panel.Controls.Add(l);
                l.SendToBack();
            }
        }
        private void init_separetors_rows()
        {
            int totalWidth = getWidthTable();
            int numbersSavements = 0;
            if (saves != null)
                numbersSavements = saves.Count;
            rowsSeparetors = new List<Bunifu.Framework.UI.BunifuSeparator>();
            for (int i = 0; i < numbersSavements + 1; i++)
            {
                Bunifu.Framework.UI.BunifuSeparator r = new Bunifu.Framework.UI.BunifuSeparator();
                r.BackColor = Color.Transparent;
                r.AutoSize = false;
                r.LineColor = Color.Snow;
                r.LineThickness = 2;
                r.Vertical = false;
                r.Width = totalWidth;
                r.Height = ROW_SPACE;
                if(i < numbersSavements) {
                    rowsSeparetors.Add(r);
                    r.Location = new Point(0, saves[i].panel.Location.Y + saves[i].panel.Height);
                }
                else
                {
                    rowsSeparetors.Insert(0, r);
                    r.Location = new Point(0, HEIGHT_ROW);
                }
                panel.Controls.Add(r);
                r.BringToFront();
            }
            updateList();
        }
        private void init_separetors_cols()
        {
            int numbers = 0;
            if (saves != null)
                numbers = saves.Count;
            colsSeparetors = new List<Bunifu.Framework.UI.BunifuSeparator>();
            for (int i = 0; i < TITLES.Length+1; i++)
            {
                Bunifu.Framework.UI.BunifuSeparator c = new Bunifu.Framework.UI.BunifuSeparator();
                c.BackColor = Color.Transparent;
                c.AutoSize = false;
                c.LineColor = Color.Snow;
                c.LineThickness = 1;
                c.Vertical = true;
                c.Width = COL_SPACE;
                c.Height = HEIGHT_ROW;
                colsSeparetors.Add(c);
                if (i == 0)
                    c.Location = new Point(0, 0);
                else
                {
                    c.Location = new Point(titles[i - 1].Location.X + titles[i - 1].Width , 0);
                }
                panel.Controls.Add(c);
                c.SendToBack();
            }
        }
        private void init_savements()
        {
            if(saves != null)
                for (int i = 0; i < saves.Count; i++)
                {
                    if(saves[i] != null)
                    {
                        saves[i].numberLbl.Text = (i + 1) + ".";
                        if (i == 0)
                        {
                            saves[i].panel.Location = new Point(0, HEIGHT_ROW + ROW_SPACE);
                        }
                        else
                        {
                            saves[i].panel.Location = new Point(0, saves[i - 1].panel.Location.Y + saves[i - 1].panel.Height + ROW_SPACE);
                        }
                        panel.Controls.Add(saves[i].panel);
                        saves[i].panel.SendToBack();
                    }
                }
            updateList();
        }
        public void updateList()
        {
            if(rowsSeparetors != null && colsSeparetors != null) { 
                for (int i = 0; i < colsSeparetors.Count; i++)
                {
                    colsSeparetors[i].Height = rowsSeparetors[rowsSeparetors.Count - 1].Location.Y + rowsSeparetors[rowsSeparetors.Count - 1].Height + 5;
                    colsSeparetors[i].BringToFront();
                }
                for (int i = 0; i < rowsSeparetors.Count; i++)
                {
                    rowsSeparetors[i].BringToFront();
                }
                for (int i = 0; i < saves.Count; i++)
                {
                    if (saves[i].deleted == true)
                    {
                        saves[i].panel.BringToFront();
                    }
                    else
                    {
                        saves[i].panel.SendToBack();
                    }
                }
                if(colsSeparetors.Count > 0)
                colsSeparetors[0].BringToFront();
                colsSeparetors[colsSeparetors.Count - 1].BringToFront();
            }
        }
    }
}
