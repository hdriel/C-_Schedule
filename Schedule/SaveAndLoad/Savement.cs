using Schedule.Lessons;
using Schedule.ReadWriteFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Schedule.SaveAndLoad
{
    [Serializable()]
    public class Savement //: Exception, ISerializable
    {
        public static string TEXT_FONT = "Guttman Hatzvi";
        public static int TEXT_SIZE = 11;
        public static int HEIGHT_ROW = 50;
        public static string MESSAGE = "לחץ כאן להוספת שמירה חדשה.";
        //public static readonly string[] TYPE_BUTTONS = { "שמירה", "טעינה", "איפוס" };
        private enum WIDTHS { SAVE = 90, LOAD = 90, RESET = 55, DATE = 123, TIME = 104, NUMBER = 50, NAME = 280 };
        private enum TYPES { SAVE, LOAD, RESET, DATE, TIME , NUMBER };
        public static int COL_SPACE = 20;
        private static int countSavements { get; set; }
        public PanelSavements ListSavementsPanel { get; set; }
        public bool deleted { get; set; }
        public Data data { get; set; }
        public DateTime datetime { get; set; }
        public int number { get; set; }
        public Bunifu.Framework.UI.BunifuMaterialTextbox nameSavement { get; set; }
        public Bunifu.Framework.UI.BunifuCustomLabel date { get; set; }
        public Bunifu.Framework.UI.BunifuCustomLabel time { get; set; }
        public Bunifu.Framework.UI.BunifuCustomLabel numberLbl { get; set; }
        public Bunifu.Framework.UI.BunifuCustomLabel ShowMessage { get; set; }
        public Bunifu.Framework.UI.BunifuThinButton2 save { get; set; }
        public Bunifu.Framework.UI.BunifuThinButton2 load { get; set; }
        public Bunifu.Framework.UI.BunifuThinButton2 reset { get; set; }
        public System.Windows.Forms.Panel panel { get; set; }

        public Form1 mainForm { get; set; }

        public Savement() { }
        //public Savement(string message) : base(message) { }
        //public Savement(string message, System.Exception inner) : base(message, inner) { }
        //protected Savement(SerializationInfo info, StreamingContext context) : base(info, context) {} // allways got this ctor from Serialization!!! problem!!

        public Savement(Form1 mf, bool showAddNewSavement = false)
        {
            mainForm = mf;
            datetime = DateTime.Now;
            save    = initButtons(save  , TYPES.SAVE);
            load    = initButtons(load  , TYPES.LOAD);
            reset   = initButtons(reset , TYPES.RESET);
            date    = initLables(date, TYPES.DATE);
            time    = initLables(time, TYPES.TIME);
            numberLbl = initLables(numberLbl, TYPES.NUMBER);
            panel   = initPanel(panel);
            nameSavement = initTextBoxes(nameSavement);
            countSavements++;
            number = countSavements;
            addControlsToPanel();

            ShowMessage = initMessage(ShowMessage, MESSAGE);
            if (showAddNewSavement) {
                deleted = true;
                removeControlsFromPanel();
                panel.Controls.Add(ShowMessage);
                ShowMessage.BringToFront();
                nameSavement.Text = "שמירה חדשה";
                if (ListSavementsPanel != null)
                    ListSavementsPanel.updateList();
            }
        }
        private Bunifu.Framework.UI.BunifuThinButton2 initButtons(Bunifu.Framework.UI.BunifuThinButton2 btn, TYPES type)
        {
            if (btn == null)
                btn = new Bunifu.Framework.UI.BunifuThinButton2();
            string text = "";
            int width = 0, height = 0, locY = 0, locX = 0;
            Color lineColor = Color.Transparent, textColor = Color.Transparent ;

            switch (type)
            {
                case TYPES.SAVE:
                    text = "שמור";
                    width = (int)WIDTHS.SAVE;
                    lineColor = Color.DeepSkyBlue;
                    textColor = Color.SkyBlue;
                    locX = 0; //
                    btn.Click += new EventHandler(saveData);
                    break;
                case TYPES.LOAD:
                    text = "טען";
                    width = (int)WIDTHS.LOAD;
                    lineColor = Color.SeaGreen;
                    textColor = Color.Aquamarine;
                    locX = 1; // 
                    btn.Click += new EventHandler(loadData);
                    break;
                case TYPES.RESET:
                    text = "אפס";
                    width = (int)WIDTHS.RESET;
                    lineColor = Color.Crimson;
                    textColor = Color.Pink;
                    locX = 2; // 
                    btn.Click += new EventHandler(resetData);
                    break;
                default:
                    break;
            }
            
            height = HEIGHT_ROW - 10;

            btn.BackColor = Color.Transparent;

            btn.IdleLineColor = lineColor;
            btn.ActiveLineColor = textColor;
            
            btn.IdleForecolor = textColor;
            btn.ActiveForecolor = Color.White;

            btn.IdleFillColor = Color.Transparent;
            btn.ActiveFillColor = lineColor;

            btn.AutoSize = false;
            btn.Width = width;
            btn.Height = height;
            btn.IdleBorderThickness = 2;
            btn.ActiveBorderThickness = 2;
            btn.IdleCornerRadius = 10;
            btn.ActiveCornerRadius = 10;
            btn.ButtonText = text.ToString();
            btn.TextAlign = ContentAlignment.MiddleCenter;
            btn.Location = new Point(locX, locY);
            btn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            return btn;
        }
        private Bunifu.Framework.UI.BunifuCustomLabel initLables(Bunifu.Framework.UI.BunifuCustomLabel lbl, TYPES type)
        {
            if (lbl == null)
                lbl = new Bunifu.Framework.UI.BunifuCustomLabel();
            lbl.BackColor = Color.Transparent;
            lbl.ForeColor = Color.Snow;
            lbl.Font = new Font(TEXT_FONT, TEXT_SIZE, FontStyle.Bold);
            lbl.AutoSize = false;
            lbl.Height = HEIGHT_ROW - 10;
            
            lbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            switch (type)
            {
                case TYPES.DATE:
                    lbl.Width = (int)WIDTHS.DATE; //
                    if (datetime == null)
                        lbl.Text = "dd/MM/yyyy";
                    else
                        lbl.Text = datetime.ToString("dd/MM/yyyy");
                    break;
                case TYPES.TIME:
                    lbl.Width = (int)WIDTHS.TIME; //
                    if (datetime == null)
                        lbl.Text = "HH:mm";
                    else
                        lbl.Text = datetime.ToString("HH:mm:ss");
                    break;
                case TYPES.NUMBER:
                    lbl.Width = (int)WIDTHS.NUMBER; //
                    lbl.Text = "0.";
                    break;
                default:
                    break;
            }
            return lbl;
        }
        private Bunifu.Framework.UI.BunifuMaterialTextbox initTextBoxes(Bunifu.Framework.UI.BunifuMaterialTextbox tb)
        {
            if (tb == null)
                tb = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            tb.Text = "שמירה בשם";
            tb.BackColor = Color.FromArgb(77, 102, 142);
            tb.ForeColor = Color.White;
            tb.LineIdleColor = Color.Transparent;
            tb.LineMouseHoverColor = Color.Orange;
            tb.LineFocusedColor = Color.Yellow;
            tb.LineThickness = 3;
            tb.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            tb.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            tb.AutoSize = false;
            tb.Height = HEIGHT_ROW - 10;
            tb.Width = (int)WIDTHS.NAME;
            return tb;
        }
        private System.Windows.Forms.Panel initPanel(System.Windows.Forms.Panel p)
        {
            if (p == null)
                p = new System.Windows.Forms.Panel();
            p.AutoSize = false;
            p.Height = HEIGHT_ROW;
            p.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            p.BackColor = Color.Transparent;
            int w = COL_SPACE;
            foreach (WIDTHS item in Enum.GetValues(typeof(WIDTHS)))
            {
                w += (int)item + COL_SPACE;
            }
            p.Width = w;
            return p;
        }
        private Bunifu.Framework.UI.BunifuCustomLabel initMessage(Bunifu.Framework.UI.BunifuCustomLabel lbl, string message)
        {
            if (lbl == null)
                lbl = new Bunifu.Framework.UI.BunifuCustomLabel();
            lbl.BackColor = Color.Transparent;
            lbl.ForeColor = Color.Snow;
            lbl.Font = new Font(TEXT_FONT, TEXT_SIZE + 10, FontStyle.Bold);
            lbl.AutoSize = false;
            lbl.Height = HEIGHT_ROW - 10;
            lbl.Text = message;
            lbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lbl.Width = panel.Width;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Location = new Point(0, 0);
            lbl.Click += ((s, e) => {
                deleted = false;
                data = mainForm.dataProgram;
                panel.Controls.Clear();
                addControlsToPanel();
            });
            return lbl;
        }

        protected void addControlsToPanel()
        {
            ReLocationAllControls();
            panel.Controls.Add(numberLbl);
            panel.Controls.Add(nameSavement);
            panel.Controls.Add(date);
            panel.Controls.Add(time);
            panel.Controls.Add(save);
            panel.Controls.Add(load);
            panel.Controls.Add(reset);
            panel.Invalidate();
            panel.SendToBack();
        }
        private void ReLocationAllControls()
        {
            reset.Location = new Point(COL_SPACE, 0);
            load.Location = new Point(reset.Location.X + reset.Width + COL_SPACE, 0);
            save.Location = new Point(load.Location.X + + load.Width + COL_SPACE, 0);
            time.Location = new Point(save.Location.X + +save.Width + COL_SPACE, 0);
            date.Location = new Point(time.Location.X + +time.Width + COL_SPACE, 0);
            nameSavement.Location = new Point(date.Location.X + date.Width + COL_SPACE, 0);
            numberLbl.Location = new Point(nameSavement.Location.X + nameSavement.Width + COL_SPACE, 0);
        }
        protected void removeControlsFromPanel()
        {
            panel.Controls.Clear();
        }
        public void saveData(object sender , EventArgs e)
        {
            
            if (data == null) {
                System.Windows.Forms.MessageBox.Show("לא הוגדר מידע לשמירה");
                return;
            }
            Bunifu.Framework.UI.BunifuThinButton2 btn = sender as Bunifu.Framework.UI.BunifuThinButton2;
            datetime = DateTime.Now;
            date.Text = datetime.ToString("d/M/yyyy");
            time.Text = datetime.ToString("HH:mm:ss");
            data.changeFileName(datetime);
            RW_Data.WriteJSON(data);
        }
        public void loadData(object sender, EventArgs e)
        {
            Bunifu.Framework.UI.BunifuThinButton2 btn = sender as Bunifu.Framework.UI.BunifuThinButton2;
            Data d = data;
            RW_Data.ReadJSON(ref d);
            data = d;
        }
        public void resetData(object sender, EventArgs e)
        {
            Bunifu.Framework.UI.BunifuThinButton2 btn = sender as Bunifu.Framework.UI.BunifuThinButton2;
            if(btn == reset) { 
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("האם אתה בטוח שאתה רוצה למחוק את השמירה הזאת?", "מחיקת שמירה", System.Windows.Forms.MessageBoxButtons.YesNo);
                if(result == System.Windows.Forms.DialogResult.Yes)
                {
                    deleted = true;
                    removeControlsFromPanel();
                    panel.Controls.Add(ShowMessage);
                    ShowMessage.BringToFront();
                    nameSavement.Text = "שמירה חדשה";
                    if (ListSavementsPanel != null)
                        ListSavementsPanel.updateList();
                }
            }
        }

        /*
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
        */
        
    }
}
