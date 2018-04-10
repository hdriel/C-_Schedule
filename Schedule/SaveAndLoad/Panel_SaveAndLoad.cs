using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule.SaveAndLoad
{
    class Panel_SaveAndLoad
    {
        public System.Windows.Forms.Panel panel { get; set; }
        public PanelSavements PS { get; set; }

        public Panel_SaveAndLoad() { }
        public Panel_SaveAndLoad(Form1 fm)
        {
            PS = new PanelSavements(fm);
            PS.panel.Location = new System.Drawing.Point();

            panel = new System.Windows.Forms.Panel();
            panel.BackColor = Color.Transparent;
            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;
            panel.RightToLeft = RightToLeft.Yes;

            panel.Controls.Add(PS.panel);
        }
    }
}
