using Schedule.CoursesList;
using Schedule.Lessons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Schedule
{
    class CourseItem
    {
        static int WIDTH = 1000,
                   HIEGHT = 41;

        static int W_LONG = 178,
                    W_SHORT = 70,
                    W_MEDIUM = 103,
                    W_CHAR = 50;

        static int PROP_CARD_BORDER_RADIUS = 0,
                    PROP_CARD_SIZE_CLOSE_WIDTH = WIDTH,
                    PROP_CARD_SIZE_CLOSE_HIEGHT = HIEGHT,
                    PROP_TITLE_HIEGHT = 28, 
                    PROP_PERSENR_HR_SEPERATION = 90,
                    COL_SPACE = 10,
                    HEIGHT_ROW_ITEM = 20;
                   

        static string FONT_TEXT = "";
        static int FONT_SIZE_TITLE = 11,
                   FONT_SIZE_SUBTITLE = 9;
        static string[] LABEL_TITLES = { "מס\"ד", "מרצה", "יום", "סיום", "התחלה", "שש", "כיתה" };

        static int amount = 0;

        List<Lesson> lessonSelected;
        LessonList lessonList;

        Bunifu.Framework.UI.BunifuCards card;
        bool openCard = false;
        Point locationCard;

        LableEditable title, year;
        Label LINE_TITLE = new Label();

        Panel[] groups;
        LableEditable[][][] lessonsLables;
        Label[] LINE_SUBTITLES = new Label[2];
        CheckButtonLsn checkButtons;

        int id, max_height = HIEGHT;
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

        CheckButton btn_edit;

        PanelCoursesList panelCoursesList;

        public CourseItem(PanelCoursesList panelCoursesList, LessonList course, int x, int y)
        {
            if(course == null)
                return;
            
            lessonList = course;
            lessonList.sortBy("type", true);
            locationCard = new Point(x, y);

            id = amount;
            lessonSelected = new List<Lesson>();

            this.panelCoursesList = panelCoursesList;

            init_card(amount);

            init_AdditionsTitleCard();
            init_lessonList();

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
            if(lessonList != null)
                return lessonList[0].courseName;
            return null;
        }

        public Lesson[] getSelectedlessonList()
        {
            return lessonSelected.ToArray();
        }
               
        private void init_card(int id)
        {
            card = new Bunifu.Framework.UI.BunifuCards();
            card.AutoSize = false;
            card.BorderRadius = PROP_CARD_BORDER_RADIUS;
            card.Font = new Font(FONT_TEXT, FONT_SIZE_SUBTITLE);
            card.RightToLeft = RightToLeft.Yes;
            if( id % 2 == 0) { 
                card.color = colors[id % colors.GetLength(0), 0];
                card.BackColor = colors[id % colors.GetLength(0), 1];
            }
            else { 
                card.color = colors[id % colors.GetLength(0), 0];
                card.BackColor = colors[id % colors.GetLength(0), 1];
            }
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
                panelCoursesList.reorderLocationCards_callback();
            });
            
            // init the title of the card
            init_title(lessonList[0].courseName, lessonList[0].getYearLetter());
        }

        private void init_title(string text, string txtyear)
        {
            year = new LableEditable("שנה - " + txtyear, "שנה");
            year.Font(new Font(FONT_TEXT, FONT_SIZE_TITLE, FontStyle.Bold | FontStyle.Underline));
            year.BackColor(Color.Transparent);
            year.ForeColor(Color.Black);
            year.Width(W_MEDIUM);
            year.Height(PROP_TITLE_HIEGHT);
            year.Location(new Point(WIDTH - 50 - year.Width(), 8));
            year.ChangeState();
            year.label.Click += ((sender, e) => {
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
                panelCoursesList.reorderLocationCards_callback();

            });
            card.Controls.Add(year.label);
            card.Controls.Add(year.textbox);


            int width_edit_button = 5 + W_MEDIUM + COL_SPACE;
            title = new LableEditable(text, "שם הקורס");
            title.Font(new Font(FONT_TEXT, FONT_SIZE_TITLE, FontStyle.Bold | FontStyle.Underline));
            title.BackColor(Color.Transparent);
            title.ForeColor(Color.Black);
            title.Width(year.Location().X - width_edit_button - COL_SPACE);
            title.Height(PROP_TITLE_HIEGHT);
            title.Location(new Point(width_edit_button + 5, 8));
            title.ChangeState();
            title.label.Click += ((sender, e) => {
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
                panelCoursesList.reorderLocationCards_callback();
                
            });
            card.Controls.Add(title.label);
            card.Controls.Add(title.textbox);

            LINE_TITLE.Width = WIDTH;
            LINE_TITLE.Height = 2;
            LINE_TITLE.Text = "";
            LINE_TITLE.BackColor = colors[id % colors.GetLength(0), 0];
            LINE_TITLE.Location = new Point(0, HIEGHT - LINE_TITLE.Height);
            card.Controls.Add(LINE_TITLE);
        }

        private void init_AdditionsTitleCard()
        {
            checkButtons = new CheckButtonLsn();
            checkButtons.lsn = lessonList;
            checkButtons.Checked = false;
            checkButtons.Location = new Point(WIDTH - checkButtons.Width*2 , PROP_TITLE_HIEGHT - checkButtons.Height + 5);
            checkButtons.OnChange += ((s,e)=> {
                CheckButtonLsn cb = s as CheckButtonLsn;
                if (cb.Checked)
                {
                    for(int i = 0; i < lessonList.amount(); i++)
                    {
                        lessonSelected.Add(lessonList[i]);
                    }
                }
                else
                {
                    lessonSelected = new List<Lesson>();
                }
                panelCoursesList.setSelectedLessons_callback();
            });
            card.Controls.Add(checkButtons);

            btn_edit = new CheckButton();
            btn_edit.Text = "עריכה";
            btn_edit.AutoSize = false;
            btn_edit.Width = W_MEDIUM;
            btn_edit.Height = PROP_TITLE_HIEGHT;
            btn_edit.Font = new Font(FONT_TEXT, 12, FontStyle.Bold);
            btn_edit.BackColor = Color.Transparent;
            btn_edit.ForeColor = colors[id % colors.GetLength(0), 0];
            btn_edit.FlatStyle = FlatStyle.Flat;
            btn_edit.Location = new Point(5, btn_edit.Height/3 - 1);
            btn_edit.btn_checked = false;
            btn_edit.id = id;

            btn_edit.Click += ((s,e)=> {
                CheckButton btn = s as CheckButton;
                btn.btn_checked = !btn.btn_checked;
                if (btn.btn_checked)
                {
                    btn_edit.BackColor = colors[id % colors.GetLength(0), 0];
                    btn_edit.ForeColor = Color.Transparent;
                    btn_edit.Text = "שמירה";
                }
                else
                {
                    btn_edit.BackColor = Color.Transparent;
                    btn_edit.ForeColor = colors[id % colors.GetLength(0), 0];
                    btn_edit.Text = "עריכה";
                }
                title.ChangeState();
                year.ChangeState();
                changeEditState();
            });

            card.Controls.Add(btn_edit);
            btn_edit.BringToFront();
        }

        private void changeEditState()
        {
            for (int i = 0; lessonsLables != null && i < lessonsLables.Length; i++)
            {
                for (int j = 0; lessonsLables[i] != null && j < lessonsLables[i].Length; j++)
                {
                    for (int k = 0; lessonsLables[i][j] != null && k < lessonsLables[i][j].Length; k++)
                    {
                        lessonsLables[i][j][k].ChangeState();
                    }
                }
            }
        }

        private void addAllLabelsToPanel()
        {
            for (int i = 0; lessonsLables != null && i < lessonsLables.Length; i++)
            {
                if (lessonsLables[i] != null)
                {
                    for (int j = 0; j < lessonsLables[i].Length; j++)
                    {
                        if (lessonsLables[i][j] != null)
                        {
                            for (int k = 0; k < lessonsLables[i][j].Length ; k++)
                            {
                                lessonsLables[i][j][k].addToControl(groups[i]);
                                lessonsLables[i][j][k].textbox.BringToFront();
                                lessonsLables[i][j][k].label.BringToFront();
                            }
                        }
                    }
                }
            }
        }
        private void init_lessonList()
        {
            // get amount of lesson in each category
            List<List<Lesson>> lessonsByTypes = null;
            int[] arrsLength = getAmountOfTypes(ref lessonsByTypes);
            int heightLableTitleGroup = 0;
            // init 3 groupBoxes for: 
            groups = new Panel[lessonsByTypes.Count];
            for (int i = 0; i < groups.Length; i++)
            {
                groups[i]           = new Panel();
                groups[i].AutoSize  = false;
                groups[i].Width     = WIDTH - 50;
                groups[i].Height    = 25;
                groups[i].Anchor    = AnchorStyles.Top | AnchorStyles.Right;
                groups[i].BackColor = Color.Transparent;
                groups[i].ForeColor = Color.Black;
                groups[i].Font      = new Font(FONT_TEXT, FONT_SIZE_SUBTITLE);
                groups[i].RightToLeft = RightToLeft.Yes;

                Label l     = new Label();
                l.Width     = groups[i].Width;
                l.Height    = 20;
                l.Font      = new Font(FONT_TEXT, FONT_SIZE_SUBTITLE , FontStyle.Underline);
                l.Text = TITLES[i];
                l.Location = new Point(0, 2);
                heightLableTitleGroup = l.Location.Y + l.Height + 2;
                groups[i].Controls.Add(l);
            }

            lessonsLables = new LableEditable[lessonsByTypes.Count][][];
            for (int i = 0; i < lessonsByTypes.Count; i++)
            {
                if (lessonsByTypes[i].Count > 0)
                {
                    lessonsLables[i] = new LableEditable[lessonsByTypes[i].Count][];
                    for (int ii = 0; ii < lessonsLables[i].Length; ii++)
                    {
                        lessonsLables[i][ii] = new LableEditable[LABEL_TITLES.Length];
                    }
                }
            }
            
            // init the properties and data
            LINE_SUBTITLES = new Label[TITLES.Length - 1];
            for (int i = 0; i < lessonsByTypes.Count; i++)
            {
                for (int s = 0 ; s < lessonsByTypes[i].Count; s++)
                {
                    int widthOfAllTitles = 0;
                    for (int a = LABEL_TITLES.Length - 1; a >= 0; a--)
                    {
                        switch (a)
                        {
                            case 0: // number
                                lessonsLables[i][s][a] = new LableEditable(lessonsByTypes[i][s].number.ToString(), LABEL_TITLES[a]);
                                lessonsLables[i][s][a].Width(W_CHAR);
                                break;
                            case 1: // lecturer
                                lessonsLables[i][s][a] = new LableEditable(lessonsByTypes[i][s].lecturer, LABEL_TITLES[a]);
                                lessonsLables[i][s][a].Width(W_LONG);
                                break;
                            case 2: // day
                                lessonsLables[i][s][a] = new LableEditable(lessonsByTypes[i][s].getShortDay(), LABEL_TITLES[a]);
                                lessonsLables[i][s][a].Width(W_SHORT);
                                break;
                            case 3: // end
                                lessonsLables[i][s][a] = new LableEditable(lessonsByTypes[i][s].getEndHour(), LABEL_TITLES[a]);
                                lessonsLables[i][s][a].Width(W_SHORT);
                                break;
                            case 4: // start
                                lessonsLables[i][s][a] = new LableEditable(lessonsByTypes[i][s].getStartHour(), LABEL_TITLES[a]);
                                lessonsLables[i][s][a].Width(W_SHORT);
                                break;
                            case 5: // hours
                                lessonsLables[i][s][a] = new LableEditable(lessonsByTypes[i][s].weekHour.ToString(), LABEL_TITLES[a]);
                                lessonsLables[i][s][a].Width(W_SHORT);
                                break;
                            case 6: // classes
                                lessonsLables[i][s][a] = new LableEditable(lessonsByTypes[i][s].className, LABEL_TITLES[a]);
                                lessonsLables[i][s][a].Width(W_MEDIUM);
                                break;
                        }
                        widthOfAllTitles += lessonsLables[i][s][a].Width();
                        lessonsLables[i][s][a].label.BackColor = Color.Transparent;
                        lessonsLables[i][s][a].textbox.BackColor = Color.WhiteSmoke;
                        lessonsLables[i][s][a].ChangeState();
                        lessonsLables[i][s][a].Location(new Point(0, 0));

                        if (lessonsLables[i][s][3] != null && lessonsLables[i][s][4] != null && lessonsLables[i][s][5] != null)
                        {
                            lessonsLables[i][s][3].updateHoursInputRoles(lessonsLables[i][s][4], lessonsLables[i][s][3], lessonsLables[i][s][5]);
                            lessonsLables[i][s][4].updateHoursInputRoles(lessonsLables[i][s][4], lessonsLables[i][s][3], lessonsLables[i][s][5]);
                        }
                    }

                    for (int b = lessonsLables[i][s].Length-1; b >= 0; b--)
                    {
                        int locX = 0; 
                        int locY = 0;
                        locX = b == LABEL_TITLES.Length - 1 ? groups[i].Width - widthOfAllTitles - 100 : lessonsLables[i][s][b + 1].Location().X + lessonsLables[i][s][b + 1].Width() + COL_SPACE;
                        locY = b < LABEL_TITLES.Length - 1 && s != 0 ? lessonsLables[i][s - 1][b + 1].Location().Y + HEIGHT_ROW_ITEM + 2 : heightLableTitleGroup;
                            
                        lessonsLables[i][s][b].Location(new Point(locX, locY));
                        lessonsLables[i][s][b].label.Invalidate();
                        lessonsLables[i][s][b].textbox.Invalidate();
                    }
                    groups[i].Height = lessonsLables[i][s] == null ? heightLableTitleGroup + 10 : heightLableTitleGroup + 10 + (lessonsLables[i].Length)*(HEIGHT_ROW_ITEM + 2);
                }
                if (i == 0)
                {
                    groups[i].Location = new Point(5, LINE_TITLE.Location.Y + LINE_TITLE.Height + 10);
                }
                else
                {
                    groups[i].Location = new Point(5, LINE_SUBTITLES[i - 1].Location.Y + LINE_SUBTITLES[i - 1].Height + 2);
                }
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
                card.Controls.Add(groups[i]);
            }

            // add seperation line to the next type lesson
            max_height = groups[groups.Length - 1].Location.Y + groups[groups.Length - 1].Height + 15;

            addAllLabelsToPanel();
        }

        private int[] getAmountOfTypes(ref List<List<Lesson>> lessonsByType)
        {
            int[] arr = new int[3] { 0, 0, 0 };

            List<Lesson> lecturers = new List<Lesson>();
            List<Lesson> labraries = new List<Lesson>();
            List<Lesson> practices = new List<Lesson>();

            for (int i = 0; i < lessonList.amount(); i++)
            {
                switch (lessonList[i].type)
                {
                    case "ה":
                    case "הרצאה":
                        arr[0]++;
                        lecturers.Add(lessonList[i]);
                        break;

                    case "מ":
                    case "מעבדה":
                        arr[1]++;
                        labraries.Add(lessonList[i]);
                        break;

                    case "ת":
                    case "תרגול":
                        arr[2]++;
                        practices.Add(lessonList[i]);
                        break;
                    
                    default:
                        break;
                }
            }
            lessonsByType = new List<List<Lesson>>();
            lessonsByType.Add(lecturers);
            lessonsByType.Add(labraries);
            lessonsByType.Add(practices);
            return arr;
        }


        public void Checked(bool check)
        {
            checkButtons.Checked = check;
        }
        public bool getChecked()
        {
            return checkButtons.Checked;
        }
        // ------------------------------------------------------------
        class CheckButtonLsn : Bunifu.Framework.UI.BunifuCheckbox
        {
            public LessonList lsn { get; set; }
        }
        class LableEditable 
        {
            private string role { get; set; }
            private bool edit { get; set; }
            public Label label { get; set; }
            public TextBox textbox { get; set; }

            public LableEditable(string text, string role, int x=0, int y=0)
            {
                this.role = role;
                label = new Label();
                textbox = new TextBox();

                ForeColor(Color.Black);
                BackColor(Color.White);
                RightToLeft();
                Anchor();
                Font(new Font(FONT_TEXT, FONT_SIZE_TITLE, FontStyle.Bold | FontStyle.Underline));
                Location(new Point(x, y));
                TextAlign(ContentAlignment.MiddleLeft);
                Text(text);
                edit = false;

                Height(HEIGHT_ROW_ITEM);
                Width(50);


                textbox.GotFocus += new EventHandler((sender, e) => {
                    // Remove Text
                    TextBox textBoxTitle = sender as TextBox;
                    if (String.IsNullOrWhiteSpace(textBoxTitle.Text) || textBoxTitle.Text.Equals(role))
                    {
                        textBoxTitle.Text = "";
                    }
                    textBoxTitle.ForeColor = Color.Black;
                });
                // when the check box lost focus , set the text placeholder in Gray color and add the placeholder text if is empty
                textbox.LostFocus += new EventHandler((sender, e) => {
                    // Add PlaceHoder Text
                    TextBox textBoxTitle = sender as TextBox;
                    if (String.IsNullOrWhiteSpace(textBoxTitle.Text))
                    {
                        textBoxTitle.Text = role;
                        textBoxTitle.ForeColor = Color.Gray;
                    }
                });
               
                switch (role)
                {
                    case "מס\"ד":
                        textbox.KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            digitsPress(sender, e, tb, 2);
                        });
                        textbox.ReadOnly = true;
                        textbox.Enabled = false;
                        break;

                    case "מרצה":
                        textbox.KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            //charsPress(sender, e, tb);
                        });
                        break;

                    case "יום":
                        textbox.KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            dayPress(sender, e, tb);
                        });
                        TextAlign(ContentAlignment.MiddleCenter);
                        break;

                    case "סיום":
                        textbox.KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            hourPress(sender, e, tb);
                        });
                        TextAlign(ContentAlignment.MiddleCenter);
                        break;

                    case "התחלה":
                        textbox.KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            hourPress(sender, e, tb);
                        });
                        TextAlign(ContentAlignment.MiddleCenter);
                        break;

                    case "שש":
                        textbox.KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            digitsPress(sender, e, tb, 1);
                        });
                        textbox.ReadOnly = true;
                        textbox.Enabled = false;
                        TextAlign(ContentAlignment.MiddleCenter);
                        break;

                    case "כיתה":
                        textbox.KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;

                        });
                        break;

                    case "שנה":
                        try {
                            string yearnumertext = textbox.ToString();
                            switch (Convert.ToInt32(yearnumertext))
                            {  // 1. year
                                case 1: textbox.Text = "א"; break;
                                case 2: textbox.Text = "ב"; break;
                                case 3: textbox.Text = "ג"; break;
                                case 4: textbox.Text = "ד"; break;
                            }
                        }
                        catch
                        {
                        }
                        textbox.TextAlign = HorizontalAlignment.Center;
                        textbox.KeyPress += ((sender, e) =>
                        {
                            TextBox tb = sender as TextBox;
                            yearPress(sender, e, tb);
                        });
                        TextAlign(ContentAlignment.MiddleCenter);
                        break;

                    case "שם הקורס":
                        textbox.KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            nameCourseInListPress(sender, e, tb);
                        });
                        break;
                    default:
                        break;
                }

                
            }
            public void ForeColor(Color c)
            {
                label.ForeColor = c;
                textbox.ForeColor = c;
                // if textbox.text == "" || textbox.text == title sub[i] 
            }
            public void BackColor(Color c)
            {
                label.BackColor = c;
                //textbox.BackColor = c;
                // if textbox.text == "" || textbox.text == title sub[i] 
            }
            public void Anchor(AnchorStyles a1 = AnchorStyles.Right, AnchorStyles a2 = AnchorStyles.Top)
            {
                label.Anchor = a1| a2;
                textbox.Anchor = a1 | a2;
                // if textbox.text == "" || textbox.text == title sub[i] 
            }
            public void Text(string text)
            {
                if(text == null)
                {
                    label.Text = "";
                    textbox.Text = role;
                }
                else
                {
                    label.Text = text;
                    textbox.Text = text;
                }
            }
            public string Text()
            {
                return label.Text;
            }
            public void RightToLeft(RightToLeft y = System.Windows.Forms.RightToLeft.Yes)
            {
                label.RightToLeft = y;
                textbox.RightToLeft = y;
            }
            public void TextAlign(ContentAlignment ta = ContentAlignment.MiddleLeft)
            {
                label.TextAlign = ta;
                if(ta == ContentAlignment.MiddleCenter) { 
                    textbox.TextAlign = HorizontalAlignment.Center;
                }
                else{
                    textbox.TextAlign = HorizontalAlignment.Left;
                }
            }
            public void Font(Font f)
            {
                label.Font = f;
                textbox.Font = f;
            }
            public void Location(Point p)
            {
                label.Location = new Point(p.X, p.Y);
                textbox.Location = new Point(p.X, p.Y);
            }
            public Point Location()
            {
                return label.Location;
            }
            public void Width(int w)
            {
                label.AutoSize = false;
                label.Width = w;
                textbox.AutoSize = false;
                textbox.Width = w;
            }
            public int Width()
            {
                return label.Width;
            }
            public void Height(int h)
            {
                label.AutoSize = false;
                label.Height = h;
                textbox.AutoSize = false;
                textbox.Height = h;
            }
            public int Height()
            {
                return label.Height;
            }
            public void AutoSize(bool auto = false)
            {
                label.AutoSize = auto;
                textbox.AutoSize = auto;
            }

            public void addToControl(Control p)
            {
                p.Controls.Add(label);
                p.Controls.Add(textbox);
            }
            public void ChangeState()
            {
                edit = !edit;
                label.Visible = !edit;
                label.Visible = edit;
            }


            public void updateHoursInputRoles(LableEditable start, LableEditable end, LableEditable hours)
            {
                
                if (role.Equals("סיום"))
                {
                    textbox.KeyPress += ((sender, e) => {
                        bool getTouch = false;
                        TextBox tb = sender as TextBox;
                        hourPress(sender, e, tb);
                        
                        TextBox t_end = this.textbox;
                        TextBox t_start = start.textbox;
                        TextBox t_hours = hours.textbox;
                        if (!getTouch && t_end.Text.Length >= 3 && t_start.Text.Length > 0 && !t_start.Text.Equals(start.role))
                        {
                            getTouch = true;
                            if (t_start.Text.Length > 2 && t_end.Text.Length > 2)
                            {
                                int start_hours_num = Convert.ToInt32(t_start.Text.Substring(0, 2));
                                int end_hours_num = Convert.ToInt32(t_end.Text.Substring(0, 2));
                                if (end_hours_num - start_hours_num <= 0)
                                {
                                    t_end.Text = "";
                                }
                                else
                                {
                                    t_hours.Text = (end_hours_num - start_hours_num).ToString();
                                    t_hours.ForeColor = Color.Black;
                                }
                            }
                        }
                        else
                        {
                            getTouch = false;
                        }
                        
                    });
                }
                else if (role.Equals("התחלה"))
                {
                    textbox.KeyPress += ((sender, e) => {
                        bool getTouch = false;
                        TextBox tb = sender as TextBox;
                        hourPress(sender, e, tb);
                      
                        TextBox t_end = end.textbox;
                        TextBox t_start = this.textbox;
                        TextBox t_hours = hours.textbox;

                        if (!getTouch && t_start.Text.Length >= 3 && t_end.Text.Length > 0 && !t_end.Text.Equals(end.role))
                        {
                            getTouch = true;
                            if (t_start.Text.Length > 2 && t_end.Text.Length > 2)
                            {

                                int start_hours_num = Convert.ToInt32(t_start.Text.Substring(0, 2));
                                int end_hours_num = Convert.ToInt32(t_end.Text.Substring(0, 2));
                                if (end_hours_num - start_hours_num <= 0)
                                {
                                    t_start.Text = "";
                                }
                                else
                                {
                                    t_hours.Text = (end_hours_num - start_hours_num).ToString();
                                    t_hours.ForeColor = Color.Black;
                                }
                            }
                        }
                        else
                        {
                            getTouch = false;
                        }
                    });
                }
            }

            // Handler keypress event for the textbox in the add new course panel
            private void digitsPress(object sender, KeyPressEventArgs e, TextBox t, int amountDigits)
            {
                if (t.TextLength >= amountDigits)
                {
                    if (!char.IsControl(e.KeyChar) && (e.KeyChar != '\b'))
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) //&& (e.KeyChar != '.'))
                    {
                        e.Handled = true;
                    }

                    /*
                    // only allow one decimal point
                    if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                    {
                        e.Handled = true;
                    }
                    */
                }
            }
            private void charsPress(object sender, KeyPressEventArgs e, TextBox t)
            {
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar)) //&& (e.KeyChar != '.'))
                    {
                        e.Handled = true;
                    }
                }
            }
            private void nameCourseInListPress(object sender, KeyPressEventArgs e, TextBox t)
            {
                int startIndex = 0;
                if (t.TextLength <= 4 && t.TextLength > 0)
                {

                    if (e.KeyChar == '\b')
                    {
                        t.Text = "";
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        t.Text += " ";
                        startIndex = t.TextLength > 0 ? t.TextLength : 0;
                        t.SelectionStart = startIndex;
                        return;
                    }
                }
                else if (t.TextLength == 0)
                {
                    switch (e.KeyChar.ToString())
                    {
                        case "1":
                        case "ה":
                            t.Text = "ה - ";
                            break;
                        case "2":
                        case "מ":
                            t.Text = "מ - ";
                            break;
                        case "3":
                        case "ת":
                            t.Text = "ת - ";
                            break;
                        default:
                            t.Text = "";
                            break;
                    }
                    startIndex = t.TextLength > 0 ? t.TextLength : 0;
                    t.SelectionStart = startIndex;
                    e.Handled = true;
                    return;
                }
            }
            private void yearPress(object sender, KeyPressEventArgs e, TextBox t)
            {
                if (t.TextLength == 1 && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar) && (e.KeyChar != '\b') && !(e.KeyChar >= 'א' && e.KeyChar <= 'ד') && !(e.KeyChar >= '1' && e.KeyChar <= '4'))
                {
                    e.Handled = true;
                }
                else if (e.KeyChar >= '1' && e.KeyChar <= '4')
                {
                    switch (e.KeyChar)
                    {
                        case '1': e.KeyChar = 'א'; break;
                        case '2': e.KeyChar = 'ב'; break;
                        case '3': e.KeyChar = 'ג'; break;
                        case '4': e.KeyChar = 'ד'; break;
                    }
                }
            }
            private void hourPress(object sender, KeyPressEventArgs e, TextBox t)
            {
                if (t.TextLength >= 5)
                {
                    if (!char.IsControl(e.KeyChar) && (e.KeyChar != '\b'))
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        t.Text = t.Text.Substring(0, 2);
                        t.SelectionStart = 2;
                    }
                }
                else
                {
                    if (t.TextLength == 0 && e.KeyChar != '\b' && e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2')
                    {
                        e.Handled = true;
                    }
                    if (t.TextLength == 1 && e.KeyChar != '\b')
                    {
                        if (t.Text[0] == '0' && !(e.KeyChar >= '8' && e.KeyChar <= '9'))
                        {
                            e.Handled = true;
                        }
                        else if (t.Text[0] == '2' && !(e.KeyChar >= '0' && e.KeyChar <= '2'))
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            t.Text = t.Text + e.KeyChar.ToString() + ":00";
                            e.Handled = true;
                        }
                    }
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar == '\b')
                    {
                        e.Handled = true;
                    }
                }

            }
            private void typePress(object sender, KeyPressEventArgs e, TextBox t)
            {
                if (t.TextLength == 1 && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar) && (e.KeyChar != '\b') && !(e.KeyChar == 'ת' || e.KeyChar == 'ה' || e.KeyChar == 'מ') && !(e.KeyChar >= '1' && e.KeyChar <= '3'))
                {
                    e.Handled = true;
                }
                switch (e.KeyChar)
                {
                    case '1': e.KeyChar = 'ה'; break;
                    case '2': e.KeyChar = 'מ'; break;
                    case '3': e.KeyChar = 'ת'; break;
                }
            }
            private void dayPress(object sender, KeyPressEventArgs e, TextBox t)
            {
                if (t.TextLength == 1 && e.KeyChar != '\b')
                {
                    e.Handled = true;
                }
                if (!char.IsControl(e.KeyChar) && (e.KeyChar != '\b') && !(e.KeyChar >= 'א' && e.KeyChar <= 'ו') && !(e.KeyChar >= '1' && e.KeyChar <= '6'))
                {
                    e.Handled = true;
                }
                switch (e.KeyChar)
                {
                    case '1': e.KeyChar = 'א'; break;
                    case '2': e.KeyChar = 'ב'; break;
                    case '3': e.KeyChar = 'ג'; break;
                    case '4': e.KeyChar = 'ד'; break;
                    case '5': e.KeyChar = 'ה'; break;
                    case '6': e.KeyChar = 'ו'; break;
                }
            }

        }
        class CheckButton : Button
        {
            public bool btn_checked { get; set; }
            public int id { get; set; }
            GraphicsPath GetRoundPath(RectangleF Rect, int radius)
            {
                float r2 = radius / 2f;
                GraphicsPath GraphPath = new GraphicsPath();

                GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
                GraphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
                GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
                GraphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
                GraphPath.AddArc(Rect.X + Rect.Width - radius,
                                 Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
                GraphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
                GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
                GraphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);

                GraphPath.CloseFigure();
                return GraphPath;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
                GraphicsPath GraphPath = GetRoundPath(Rect, 20);

                this.Region = new Region(GraphPath);
                Color c;
                if (!btn_checked)
                {
                    c = colors[id % colors.GetLength(0), 0];
                }
                else
                {
                    c = Color.Black;
                }
                using (Pen pen = new Pen(c, 1.75f))
                {
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPath(pen, GraphPath);
                }
            }
        }
    }
}
