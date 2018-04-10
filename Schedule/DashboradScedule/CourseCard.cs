using Schedule.Lessons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    class CourseCard
    {
        static int WIDTH = 180,
                   HIEGHT = 41;


        static int PROP_CARD_BORDER_RADIUS = 0,
                    PROP_CARD_SIZE_CLOSE_WIDTH = WIDTH,
                    PROP_CARD_SIZE_CLOSE_HIEGHT = HIEGHT,
                    PROP_TITLE_HIEGHT = 28, 
                    PROP_PERSENR_HR_SEPERATION = 80;
                   

        static string FONT_TEXT = "";
        static int FONT_SIZE_TITLE = 11,
                   FONT_SIZE_SUBTITLE = 9;

        static int amount = 0;

        Lesson[] lessonSelected;
        Label LINE_TITLE = new Label();
        Label[] LINE_SUBTITLES = new Label[2];
        Bunifu.Framework.UI.BunifuCards card;

        Label title;
        LessonList lessons;
        RadioButtonLsn[][] radioButtons;
        Panel[] groups;
        Bunifu.Framework.UI.BunifuiOSSwitch toggle;
        Point locationCard;
        int id, max_height = HIEGHT;
        bool openCard = false;
        int[] lastChecked;

        static string[] TITLES = { "הרצאה", "מעבדה", "תרגול"};

        // color: 1. card (top of the card) | Oncolor (toggle) | Onoffcolor (toggle), 2. backColor (card), 3. textColor (toggle)
        const int colorSize = 11;
        static Color[,] colors = new Color[colorSize, 3]
        { {Color.Tomato         , (Color)System.Drawing.Color.FromArgb(255,224,192)     , (Color)System.Drawing.Color.FromArgb(255,128,0)  },
          {Color.SeaGreen       , (Color)System.Drawing.Color.FromArgb(192,255,192)     , Color.Lime  },
          {Color.DarkCyan       , (Color)System.Drawing.Color.FromArgb(192,255,255)     , Color.Cyan  },
          {Color.DodgerBlue     , (Color)System.Drawing.Color.FromArgb(124, 172, 249)   , Color.LightSkyBlue  },
          {Color.DarkRed        , (Color)System.Drawing.Color.FromArgb(255, 192, 192)   , Color.Red  },
          {Color.Goldenrod      , (Color)System.Drawing.Color.FromArgb(255, 255, 192)   , Color.Yellow  },
          {Color.DarkSlateBlue  , (Color)System.Drawing.Color.FromArgb(192, 192, 255)   , Color.Plum  },
          {Color.DeepPink       , (Color)System.Drawing.Color.FromArgb(255, 192, 255)   , Color.DeepPink  },
          {Color.DeepPink       , (Color)System.Drawing.Color.FromArgb(255, 192, 255)   , Color.DeepPink  },
          {Color.Black          , Color.White                                           , Color.Snow  },
          {Color.Maroon         , Color.Crimson   , Color.Red  }
        };

        PanelSchedule panelSchedule;

        public CourseCard(PanelSchedule panelSchedule, LessonList course, int x, int y)
        {
            if(course == null)
            {
                return;
            }
            lessons = course;
            lessons.sortBy("type", true);
            locationCard = new Point(x, y);
            id = amount;
            lessonSelected = new Lesson[3];

            int len = course[0].courseName.Length > 20 ? 20 : course[0].courseName.Length;
            this.panelSchedule = panelSchedule;

            init_card(amount);
            init_title(course[0].courseName.Substring(0,len));
            init_toggle();
            init_lessons();

            amount++;
        }
        public void changeLocation(int x, int y)
        {
            card.Location = new Point(x, y);
        }
        public void changeLocation(Point p)
        {
            card.Location = p;
        }
        public Point getLocation()
        {
            return card.Location;
        }
        public Bunifu.Framework.UI.BunifuCards getCard()
        {
            return card;
        }
        public string getCourseName()
        {
            if(lessons != null)
                return lessons[0].courseName;
            return null;
        }
        public Lesson[] getSelectedLessons()
        {
            return lessonSelected;
        }
        public int[] LessonsTypeLenght()
        {
            int[] lens = new int[3];
            for (int i = 0; i < radioButtons.Length; i++)
            {
                lens[i] = radioButtons[i].Length;
            }
            return lens;
        }

        public void clickOnRadioButtonLesson(Lesson lsn)
        {
            int indexType = 0;
            switch (lsn.type)
            {
                case "ה":
                case "הרצאה":
                    indexType = 0;
                    break;
                case "מ":
                case "מעבדה":
                    indexType = 1;
                    break;
                case "ת":
                case "תרגול":
                    indexType = 2;
                    break;
                default:
                    indexType = -1;
                    break;
            }
            for (int i = 0; i < radioButtons[indexType].Length; i++)
            {
                if (radioButtons[indexType][i].lsn.sameValue(lsn))
                {
                    radioButtons[indexType][i].PerformClick();
                }
            }
        }

        private bool clickRadioButtonFirst(RadioButtonLsn[][] rb, int type)
        {
            if (rb[type] != null && rb[type].Length > 0 && rb[type][0] != null)
            {
                rb[type][0].PerformClick();
                return true;
            }
            return false;
        }
        public bool clickOnRadioButtonLesson_FirstLecture()
        {
            return clickRadioButtonFirst(radioButtons, 0);
        }
        public bool clickOnRadioButtonLesson_FirstLabrary()
        {
            return clickRadioButtonFirst(radioButtons, 1);
        }
        public bool clickOnRadioButtonLesson_Firstpractice()
        {
            return clickRadioButtonFirst(radioButtons, 2);
        }

        private bool clickRadioButtonNext(RadioButtonLsn[][] rb, int type)
        {
            if (rb[type] != null && rb[type].Length > 0)
            {
                for (int i = 0; i < rb[type].Length; i++)
                {
                    if (rb[type][i] != null && rb[type][i].Checked)
                    {
                        if (i + 1 < rb[type].Length && rb[type][i + 1] != null)
                        {
                            rb[type][i + 1].PerformClick();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if(rb[type] == null)
                    {
                        return false;
                    }
                }
            }
            return false;
        }
        public bool clickOnRadioButtonLesson_NextLecture()
        {
            return clickRadioButtonNext(radioButtons, 0);
        }
        public bool clickOnRadioButtonLesson_NextLabrary()
        {
            return clickRadioButtonNext(radioButtons, 1);
        }
        public bool clickOnRadioButtonLesson_NextPractice()
        {
            return clickRadioButtonNext(radioButtons, 2);
        }


        private void init_card(int id)
        {
            card = new Bunifu.Framework.UI.BunifuCards();
            card.AutoSize = false;
            card.BorderRadius = PROP_CARD_BORDER_RADIUS;
            card.Font = new Font(FONT_TEXT, FONT_SIZE_SUBTITLE);
            card.RightToLeft = RightToLeft.Yes;
            card.color = colors[id % colors.GetLength(0), 0];
            card.BackColor = colors[id % colors.GetLength(0), 1];
            card.Width = WIDTH;
            card.Height = HIEGHT;
            card.Location = locationCard;
            
            card.Click += ((sender, e) => {
                openCard = !openCard;
                if (openCard)
                {
                    card.Height = max_height;
                    card.Invalidate();
                }
                else
                {
                    card.Height = HIEGHT;
                    card.Invalidate();
                }
                panelSchedule.reorderLocationCards_callback();
            });
            
        }
        private void init_title(string text)
        {
            title = new Label();
            title.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            title.AutoSize = false;
            title.Font = new Font(FONT_TEXT, FONT_SIZE_TITLE, FontStyle.Bold | FontStyle.Underline);
            title.BackColor = Color.Transparent;
            title.ForeColor = Color.Black;
            title.Width = WIDTH - 13;
            title.Height = PROP_TITLE_HIEGHT;
            title.Location = new Point(5, 8);
            title.RightToLeft = RightToLeft.Yes;
            title.TextAlign = ContentAlignment.MiddleLeft;
            title.Text = text;
            title.Click += ((sender, e) => {
                openCard = !openCard;
                if (openCard)
                {
                    card.Height = max_height;
                    card.Invalidate();
                }
                else
                {
                    card.Height = HIEGHT;
                    card.Invalidate();
                }
                panelSchedule.reorderLocationCards_callback();
                
            });

            card.Controls.Add(title);

            LINE_TITLE.Width = WIDTH;
            LINE_TITLE.Height = 2;
            LINE_TITLE.Text = "";
            LINE_TITLE.BackColor = colors[id % colors.GetLength(0),0];
            LINE_TITLE.Location = new Point(0, HIEGHT - LINE_TITLE.Height);
            card.Controls.Add(LINE_TITLE);
        }
        private void init_toggle()
        {
            toggle = new Bunifu.Framework.UI.BunifuiOSSwitch();
            toggle.OffColor = Color.Gray;//colors[id % colors.GetLength(0), 0];
            toggle.OnColor = colors[id % colors.GetLength(0), 0];
            toggle.AutoSize = false;
            toggle.RightToLeft = RightToLeft.Yes;
            int w = 40, h = 24;
            toggle.MinimumSize = new Size(w, h);
            toggle.MaximumSize = new Size(w, h);
            toggle.Size = new Size(w, h);
            toggle.Location = new Point(2, (HIEGHT / 2) - (toggle.Height / 2) + 2);

            toggle.Click += ((sender,e)=> {
                Bunifu.Framework.UI.BunifuiOSSwitch tgl = sender as Bunifu.Framework.UI.BunifuiOSSwitch;
                if (tgl.Value)
                {
                    for (int i = 0; radioButtons != null && i < radioButtons.Length; i++)
                    {
                        for (int j = 0; radioButtons[i] != null && j < radioButtons[i].Length; j++)
                        {
                            if (radioButtons[i] != null && radioButtons[i][j] != null) { 
                                radioButtons[i][j].Enabled = true;
                            }
                        }
                        if(radioButtons[i] != null && radioButtons[i][lastChecked[i]] != null) { 
                            radioButtons[i][lastChecked[i]].PerformClick();
                        }
                    }
                }
                else
                {
                    for (int i = 0; radioButtons != null && i < radioButtons.Length; i++)
                    {
                        for (int j = 0; radioButtons[i] != null && j < radioButtons[i].Length; j++)
                        {
                            if(radioButtons[i] != null && radioButtons[i][j] != null) { 
                                radioButtons[i][j].Enabled = false;
                                panelSchedule.setSelectedLessons_callback(radioButtons[i][j].lsn, true);
                            }
                        }
                    }
                }
                panelSchedule.enableCards_callback(lessons[0], tgl.Value);
            });
            card.Controls.Add(toggle);
            toggle.BringToFront();
        }
        private void init_lessons()
        {
            // get amount of lesson in each category
            int lengthTypes = TITLES.Length;
            int[] sizeLessonType = getAmountOfTypes(ref lengthTypes);
            lengthTypes = 3;

            // init 3 groupBoxes for: 
            groups = new Panel[lengthTypes];
            for (int i = 0; i < groups.Length; i++)
            {
                groups[i]           = new Panel();
                groups[i].AutoSize  = false;
                groups[i].Width     = WIDTH - 10;
                groups[i].Height    = 25;
                groups[i].Anchor    = AnchorStyles.Top | AnchorStyles.Right;
                groups[i].BackColor = Color.Transparent;
                groups[i].ForeColor = Color.Black;
                groups[i].Font      = new Font(FONT_TEXT, FONT_SIZE_SUBTITLE);
                groups[i].RightToLeft = RightToLeft.Yes;

                Label l     = new Label();
                l.Width     = groups[i].Width;
                l.Height    = groups[i].Height;
                l.Font      = new Font(FONT_TEXT, FONT_SIZE_SUBTITLE , FontStyle.Underline);
                l.Text = TITLES[i];
                l.Location = new Point(0, 2);
                groups[i].Controls.Add(l);
            }

            // init radio buttons size: lec, lab, prac
            radioButtons = new RadioButtonLsn[lengthTypes][];
            lastChecked = new int[lengthTypes];
            for (int i = 0; i < sizeLessonType.Length; i++)
            {
                if (sizeLessonType[i] > 0)
                { 
                    radioButtons[i] = new RadioButtonLsn[sizeLessonType[i]];
                }
                else
                {
                    radioButtons[i] = new RadioButtonLsn[1];
                }
            }
            
            
            // init the properties and data
            LINE_SUBTITLES = new Label[TITLES.Length - 1];
            for (int i = 0; i < lengthTypes; i++)
            {
                for (int k = 0, m = 0 ; k < lessons.amount(); k++)
                {
                    if (lessons[k].type.Equals(TITLES[i]) || lessons[k].type.Equals(TITLES[i].Substring(0,1)))
                    {
                        int s = m;
                        radioButtons[i][s] = new RadioButtonLsn();
                        radioButtons[i][s].lsn = lessons[k];
                        radioButtons[i][s].RightToLeft = RightToLeft.Yes;
                        radioButtons[i][s].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        radioButtons[i][s].AutoSize = false;
                        radioButtons[i][s].Width = groups[i].Width - 10;
                        radioButtons[i][s].Height = 20;
                        radioButtons[i][s].Tag = k;
                        radioButtons[i][s].number = s;
                        radioButtons[i][s].Text = (s + 1) + ".יום " + lessons[k].getShortDay() + " " + lessons[k].getStartHour() + "-" + lessons[k].getEndHour();

                        if (s == 0)
                        {
                            radioButtons[i][s].Location = new Point(5, groups[i].Height);
                            radioButtons[i][s].PerformClick();
                        }
                        else
                        {
                            radioButtons[i][s].Location = new Point(5, radioButtons[i][s - 1].Location.Y + radioButtons[i][s - 1].Height);
                        }

                        radioButtons[i][s].Click += ((sender, e) =>
                        {
                            RadioButtonLsn rb = sender as RadioButtonLsn;
                            Lesson l = lessons[(int)(rb.Tag)];
                            int y = 0;
                            switch (l.type)
                            {
                                case "ה":
                                case "הרצאה":
                                    y = 0;
                                    break;
                                case "מ":
                                case "מעבדה":
                                    y = 1;
                                    break;
                                case "ת":
                                case "תרגול":
                                    y = 2;
                                    break;
                                default:
                                    break;
                            }
                            lessonSelected[y] = lessons[(int)(rb.Tag)];
                            panelSchedule.setSelectedLessons_callback(lessonSelected[y]);
                            lastChecked[y] = rb.number;
                        });

                        groups[i].Controls.Add(radioButtons[i][s]);
                        groups[i].Height = radioButtons[i][s].Location.Y + radioButtons[i][s].Height + 10;
                        m++;
                    }
                }

                if (i == 0)
                {
                    groups[i].Location = new Point(5, LINE_TITLE.Location.Y + LINE_TITLE.Height + 10);
                }
                else
                {
                    groups[i].Location = new Point(5, LINE_SUBTITLES[i - 1].Location.Y + LINE_SUBTITLES[i - 1].Height + 2);
                }
                card.Controls.Add(groups[i]);


                // add seperation line to the next type lesson
                int j = i;
                if (j < LINE_SUBTITLES.Length)
                {
                    LINE_SUBTITLES[j] = new Label();
                    if (PROP_PERSENR_HR_SEPERATION > 100 || PROP_PERSENR_HR_SEPERATION == 0) PROP_PERSENR_HR_SEPERATION = 80;
                    LINE_SUBTITLES[j].Width = (int)((float)(PROP_PERSENR_HR_SEPERATION * 1.0 / 100) * WIDTH);
                    LINE_SUBTITLES[j].Height = 1;
                    LINE_SUBTITLES[j].Text = "";
                    LINE_SUBTITLES[j].BackColor = colors[id % colors.GetLength(0), 0];

                    int locX = ((WIDTH / 2) - (LINE_SUBTITLES[j].Width / 2));
                    int locY = groups[i].Location.Y + groups[i].Height + 5;
                    LINE_SUBTITLES[j].Location = new Point(locX, locY);
                    card.Controls.Add(LINE_SUBTITLES[j]);
                }
            }
            max_height = groups[groups.Length - 1].Location.Y + groups[groups.Length - 1].Height + 15;
        }

        
        private int[] getAmountOfTypes(ref int amountTitles)
        {
            int[] arr = new int[3] { 0, 0, 0 };
            amountTitles = 3;

            for (int i = 0; i < lessons.amount(); i++)
            {
                switch (lessons[i].type)
                {
                    case "ה":
                    case "הרצאה":
                        arr[0]++;
                        break;

                    case "מ":
                    case "מעבדה":
                        arr[1]++;
                        break;

                    case "ת":
                    case "תרגול":
                        arr[2]++;
                        break;
                    
                    default:
                        break;
                }
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                {
                    amountTitles--;
                }
            }
            return arr;
        }


        class RadioButtonLsn : RadioButton
        {
            public int number { get; set; }
            public Lesson lsn { get; set; }
        }
    }
}
