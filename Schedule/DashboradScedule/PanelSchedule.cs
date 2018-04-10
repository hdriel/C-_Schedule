using Schedule.DashboradScedule;
using Schedule.Lessons;
using Schedule.ReadWriteFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schedule
{
    

    class PanelSchedule
    {
        // Round corner of panels - use as sending this function
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,      // x-coordinate of upper-left corner
            int nTopRect,       // y-coordinate of upper-left corner
            int nRightRect,     // x-coordinate of lower-right corner
            int nBottomRect,    // y-coordinate of lower-right corner
            int nWidthEllipse,  // height of ellipse
            int nHeightEllipse  // width of ellipse
         );

        // Default Value:
        public static int WIDTH_WINDOWS = 1040,
                          HEIGHT_WINDOWS = 746;

        static int HEIGHT_ROW = 30,
                   WIDTH_COL = 80 + THICKNESS_VR_SEPERADE,
                   THICKNESS_HR_SEPERADE = 1,
                   THICKNESS_VR_SEPERADE = 3,
                   SIZE_FONT_TEXT_DAY = 20,
                   SIZE_FONT_TEXT_HOURS = 15,
                   LOC_X_TABLE = 0,
                   LOC_Y_TABLE = 20,
                   START_HOUR = 8,
                   END_HOUR = 22,
                   SPACE_CARDS = 15,
                   PADDING_LEFT_LIST = 15;



        static int PNL_WIDTH_SCHDEULE = WIDTH_WINDOWS,
                   PNL_HEIGHT_SCHDEULE = HEIGHT_WINDOWS, // DOCK - Fill

                   PNL_WIDTH_COURSES = 200,
                   PNL_HEIGHT_COURSES = HEIGHT_WINDOWS, // DOCK - Left

                   PNL_WIDTH_TITLE_BOTTOM = WIDTH_WINDOWS,
                   PNL_HEIGHT_TITLE_BOTTOM = 55, // DOCK - Bottom

                   PNL_WIDTH_TITLE_TOP = WIDTH_WINDOWS,
                   PNL_HEIGHT_TITLE_TOP = 55, // DOCK - top

                   PNL_WIDTH_MAIN = WIDTH_WINDOWS,
                   PNL_HEIGHT_MAIN = HEIGHT_WINDOWS,
                   PNL_LOC_X_MAIN = 0,
                   PNL_LOC_Y_MAIN = 0;

        static string FONT_TEXT = "Guttman Hatzvi";
        static string[] DAYS = Lesson.fullDay; //{ "ראשון", "שני" , "שלישי" , "רבעי", "חמישי", "שישי"};
        static Color[] COLORS_COURSES = { (Color)System.Drawing.ColorTranslator.FromHtml("#FFFFFF") };
        
        static Color COLOR_TEXT_DAY = (Color)System.Drawing.ColorTranslator.FromHtml("#FFFFFF"),
                     COLOR_TEXT_HOURS = (Color)System.Drawing.ColorTranslator.FromHtml("#f1f74c"),
                     COLOR_SEPERADE = COLOR_TEXT_DAY;

        // ----------------------------------------------------------------------------------------------------
        // Variables 
        Panel pnl_Main,
              pnl_Schedule,             // contain the week schedule 
              pnl_CoursesListChecked,   // contain the button of courses name
              pnl_topTitle,             // contain the title of the panel
              pnl_bottomTitle;          // contain the sub-title of the panel (the user name)

        List<PanelLsn> pnl_Lessons;     // holder the lesson panel of the user selected from the list course panel

        public LessonList lessons; // the lessons that the user selected from the list course card

        Label[] days,                   // the days labels [sunday , monday, ...]
                hours,                  // the hours labels [ 08:00 , 09:00, ...]
                separation_rows,        // the row lines 
                separation_cols;        // the col lines
        Label titleList;
        List<PanelCollision> collisions;

        CourseCard[] cards;

        Form1 mainForm;

        // CTOR
        public PanelSchedule(Form1 mf,LessonList list = null)
        {
            mainForm = mf;
            if (list != null) lessons = list;
            else mockLessons();

            // To init the panels for including data in them
            init_panels_properties();

            // To init the titles ( the columns of the list )
            init_title_day();
            init_title_Hours();

            //initLessonListPanel left
            init_ListCourses();

            //init Lesson of schedule;
            setSceduleByList(lessons);

            addAllPanelToMainPanel();

            clickAllCards();
        }

        public void setNewList(LessonList list)
        {
            foreach (var pnl in pnl_Lessons)
            {
                pnl_Schedule.Controls.Remove(pnl);
            }
            pnl_CoursesListChecked.Controls.Clear();
            pnl_Lessons = new List<PanelLsn>();

            lessons = list;
            init_ListCourses();
            setSceduleByList(lessons);
            clickAllCards();
        }

        public void clickAllCards()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                for(int j = 0; j < cards[i].LessonsTypeLenght().Length; j++)
                {
                    while (cards[i].clickOnRadioButtonLesson_NextLecture()) { };
                    while (cards[i].clickOnRadioButtonLesson_NextPractice()) { };
                    while (cards[i].clickOnRadioButtonLesson_NextLabrary()) { };
                }
            }
            reorderLocationCards_callback();
        }
        public void ClickRightsRadioButtonsByLessonsSelected(List<Lesson> list)
        {
            for (int j = 0; j < list.Count; j++)
            {
                setSelectedLessons_callback(list[j], true, true);
            }
        }
        // The panel for show at the main fram of the program
        public Panel getPanel()
        {
            return pnl_Main;
        }

        // = Initializes =====================================================================================
        // Init the Panels' Properties
        // Add the panels at the end of setup/initializetion
        private void addAllPanelToMainPanel()
        {
            pnl_Main.Controls.Add(pnl_CoursesListChecked);
            pnl_Main.Controls.Add(pnl_bottomTitle);
            pnl_Main.Controls.Add(pnl_topTitle);
            pnl_Main.Controls.Add(pnl_Schedule);
            pnl_Schedule.BringToFront();
        }
        // Init the properties of the panels
        private void init_panels_properties()
        {
            pnl_Main                            = new Panel();
            pnl_Main.Location                   = new Point(PNL_LOC_X_MAIN, PNL_LOC_Y_MAIN);
            pnl_Main.BackColor                  = Color.Transparent;
            pnl_Main.Width                      = PNL_WIDTH_MAIN;
            pnl_Main.Height                     = PNL_HEIGHT_MAIN;
            pnl_Main.RightToLeft                = RightToLeft.Yes;
            pnl_Main.Dock                       = DockStyle.Fill;

            pnl_bottomTitle                     = new Panel();
            pnl_bottomTitle.Width               = PNL_WIDTH_TITLE_BOTTOM;
            pnl_bottomTitle.Height              = PNL_HEIGHT_TITLE_BOTTOM;
            pnl_bottomTitle.RightToLeft         = RightToLeft.Yes;
            pnl_bottomTitle.BackColor           = Color.Transparent;
            pnl_bottomTitle.Dock                = DockStyle.Bottom;
            
            pnl_CoursesListChecked              = new Panel();
            pnl_CoursesListChecked.Width        = PNL_WIDTH_COURSES;
            pnl_CoursesListChecked.Height       = PNL_HEIGHT_COURSES;
            pnl_CoursesListChecked.RightToLeft  = RightToLeft.Yes;
            pnl_CoursesListChecked.BackColor    = Color.Transparent;
            pnl_CoursesListChecked.AutoScroll   = true;
            pnl_CoursesListChecked.Dock         = DockStyle.Left;
           
            pnl_topTitle                        = new Panel();
            pnl_topTitle.Width                  = PNL_WIDTH_TITLE_TOP;
            pnl_topTitle.Height                 = PNL_HEIGHT_TITLE_TOP;
            pnl_topTitle.RightToLeft            = RightToLeft.Yes;
            pnl_topTitle.BackColor              = Color.Transparent;
            pnl_topTitle.Dock                   = DockStyle.Top;
           
            pnl_Schedule                        = new Panel();
            pnl_Schedule.Width                  = PNL_WIDTH_SCHDEULE/3;
            pnl_Schedule.Height                 = PNL_HEIGHT_SCHDEULE/3;
            pnl_Schedule.RightToLeft            = RightToLeft.Yes;
            pnl_Schedule.BackColor              = Color.Transparent;
            pnl_Schedule.Dock                   = DockStyle.Fill;
            pnl_Schedule.Width                  = PNL_WIDTH_SCHDEULE/3;
            pnl_Schedule.AutoScroll             = true;
        }
        // Mock data for display and debug
        private void mockLessons()
        {
            lessons = new LessonList();

            lessons = new LessonList(5);

            lessons[0] = new Lesson();
            lessons[0].courseName = "קומפילציה";
            lessons[0].lecturer = "אלכס צורקין";
            lessons[0].number = 1;
            lessons[0].className = "F201";
            lessons[0].day = "א";
            lessons[0].end = 12;
            lessons[0].start = 10;
            lessons[0].type = "ה";
            lessons[0].weekHour = 2;
            lessons[0].year = 2;

            lessons[1] = new Lesson();
            lessons[1].courseName = "קומפילציה";
            lessons[1].lecturer = "אלכס צורקין";
            lessons[1].number = 2;
            lessons[1].className = "F201";
            lessons[1].day = "ב";
            lessons[1].end = 13;
            lessons[1].start = 12;
            lessons[1].type = "ה";
            lessons[1].weekHour = 1;
            lessons[1].year = 2;

            lessons[2] = new Lesson();
            lessons[2].courseName = "בדיקות";
            lessons[2].lecturer = "אבישי";
            lessons[2].number = 1;
            lessons[2].className = "S100";
            lessons[2].day = "ב";
            lessons[2].end = 20;
            lessons[2].start = 17;
            lessons[2].type = "מ";
            lessons[2].weekHour = 3;
            lessons[2].year = 2;

            lessons[3] = new Lesson();
            lessons[3].courseName = "אלגוריתמים 2";
            lessons[3].lecturer = "נטליה ונטיק";
            lessons[3].number = 1;
            lessons[3].className = "S202";
            lessons[3].day = "ג";
            lessons[3].end = 12;
            lessons[3].start = 8;
            lessons[3].type = "מ";
            lessons[3].weekHour = 4;
            lessons[3].year = 4;

            lessons[4] = new Lesson();
            lessons[4].courseName = "אלגוריתמים 2";
            lessons[4].lecturer = "בוריס";
            lessons[4].number = 1;
            lessons[4].className = "כלניות";
            lessons[4].day = "ד";
            lessons[4].end = 18;
            lessons[4].start = 13;
            lessons[4].type = "ת";
            lessons[4].weekHour = 5;
            lessons[4].year = 4;
        }

        // Init the structure of the schdule
        // Days - columns
        private void init_seperation_cols()
        {
            separation_cols = new Label[DAYS.Length + 1];
            for (int i = 0; i < separation_cols.Length; i++)
            {
                separation_cols[i]              = new Label();
                separation_cols[i].AutoSize     = false;
                separation_cols[i].BackColor    = COLOR_SEPERADE;
                separation_cols[i].Width        = THICKNESS_VR_SEPERADE;
                separation_cols[i].Height       = pnl_Schedule.Height*3;
                if (i == 0)
                    separation_cols[i].Location = new Point(days[i].Location.X + days[i].Width , days[i].Location.Y);
                else if (i < separation_cols.Length - 1)
                    separation_cols[i].Location = new Point(days[i - 1].Location.X - THICKNESS_VR_SEPERADE, days[i - 1].Location.Y);
                else
                    separation_cols[i].Location = new Point(days[i - 2].Location.X - days[i-2].Width - 2*THICKNESS_VR_SEPERADE, days[i - 2].Location.Y);
                pnl_Schedule.Controls.Add(separation_cols[i]);
            }
        }
        private void init_title_day()
        {
            days = new Label[DAYS.Length];
            for (int i = 0; i < days.Length; i++)
            {
                days[i]             = new Label();
                days[i].Text        = DAYS[i];
                days[i].AutoSize    = false;
                days[i].Font        = new Font(FONT_TEXT, SIZE_FONT_TEXT_DAY, FontStyle.Bold);
                days[i].ForeColor   = COLOR_TEXT_DAY;
                days[i].BackColor   = Color.Transparent;
                days[i].Width       = pnl_Schedule.Width / 3;//WIDTH_COL - THICKNESS_VR_SEPERADE;
                days[i].Height      = 40;
                int locXfromEnd     = days[i].Width * (days.Length - 1);
                //days[i].BorderStyle = BorderStyle.Fixed3D;
                days[i].TextAlign   = ContentAlignment.MiddleCenter;

                if (i > 0)
                    days[i].Location = new Point(days[i - 1].Location.X - days[i - 1].Width - THICKNESS_VR_SEPERADE, days[i - 1].Location.Y);
                else
                    days[i].Location = new Point(locXfromEnd + days[i].Width / 3, LOC_Y_TABLE);
                pnl_Schedule.Controls.Add(days[i]);
                pnl_Schedule.Invalidate();
            }
            init_seperation_cols();
        }
        // Hours- rows
        private void init_seperation_rows()
        {
            separation_rows = new Label[hours.Length + 1];
            for (int i = 0; i < separation_rows.Length; i++)
            {
                separation_rows[i]              = new Label();
                separation_rows[i].AutoSize     = false;
                separation_rows[i].BackColor    = COLOR_SEPERADE;
                separation_rows[i].Width        =  (int)(pnl_Schedule.Width * 2.3);
                separation_rows[i].Height       = THICKNESS_HR_SEPERADE;
                if (i == 0)
                    separation_rows[i].Location = new Point(days[days.Length - 1].Location.X, hours[i].Location.Y - THICKNESS_HR_SEPERADE);
                else if (i < separation_rows.Length - 1)
                    separation_rows[i].Location = new Point(days[days.Length - 1].Location.X , hours[i - 1].Location.Y + hours[i - 1].Height  + THICKNESS_HR_SEPERADE);
                else
                    separation_rows[i].Location = new Point(days[days.Length - 1].Location.X, hours[i - 2].Location.Y + hours[i - 2].Height*2 + 2*THICKNESS_HR_SEPERADE);

                pnl_Schedule.Controls.Add(separation_rows[i]);
            }

            for (int i = 0; i < separation_cols.Length; i++)
            {
                separation_cols[i].Height       = separation_rows[separation_rows.Length - 1].Location.Y - separation_rows[separation_rows.Length - 1].Height - (THICKNESS_HR_SEPERADE*separation_rows.Length + 2);
                separation_cols[i].Invalidate();
            }
        }
        private void init_title_Hours()
        {
            hours = new Label[END_HOUR - START_HOUR + 1];
            for (int i = START_HOUR; i <= END_HOUR; i++)
            {
                int j               = i - START_HOUR;
                hours[j]            = new Label();
                hours[j].Text       = "";
                if (i < 10)
                    hours[j].Text   = "0";
                hours[j].Text       += i.ToString() + ":00";

                hours[j].AutoSize   = false;
                hours[j].Font       = new Font(FONT_TEXT, SIZE_FONT_TEXT_HOURS,FontStyle.Bold);
                hours[j].ForeColor  = COLOR_TEXT_DAY;
                hours[j].BackColor  = Color.Transparent;
                hours[j].Width      = (int)(pnl_Schedule.Width / 3.5);//WIDTH_COL - THICKNESS_VR_SEPERADE;
                hours[j].Height     = pnl_Schedule.Height / (7);//HEIGHT_ROW + THICKNESS_HR_SEPERADE;
                int locXfromEnd     = days[0].Location.X + days[0].Width + THICKNESS_HR_SEPERADE;
                int locYfromSunday  = days[0].Location.Y + days[0].Height + THICKNESS_HR_SEPERADE;
                //hours[j].BorderStyle= BorderStyle.Fixed3D;
                hours[j].TextAlign  = ContentAlignment.MiddleCenter;
                if(j == 0)
                {
                    hours[j].Location = new Point(locXfromEnd, locYfromSunday);
                }
                else
                {
                    hours[j].Location = new Point(days[0].Location.X + days[0].Width + THICKNESS_VR_SEPERADE, hours[j-1].Location.Y + hours[j-1].Height + 2*THICKNESS_HR_SEPERADE);
                }

                pnl_Schedule.Controls.Add(hours[j]);
                pnl_Schedule.Invalidate();
            }
            init_seperation_rows();
        }
        
        // Return array of courses names (cards)
        private string[] getCoursesName()
        {
            List<string> names = new List<string>();
            for (int i = 0; i < lessons.amount(); i++)
            {
                if(names.Find(x => x.Equals(lessons[i].courseName.ToString())) == null)
                {
                    names.Add(lessons[i].courseName);
                }
            }
            names.Sort();
            return names.ToArray();
        }
        // Get all the courses of the lessons to new List
        private LessonList getLessonsOfCourse(string courseName)
        {
            LessonList result = new LessonList();
            for (int i = 0; i < lessons.amount(); i++)
            {
                if (lessons[i].courseName.Equals(courseName))
                {
                    result.add(lessons[i]);
                }
            }
            return result;
        }
        // Init the list of courses , meaning the cards
        private void init_ListCourses()
        {
            titleList = new Label();
            titleList.AutoSize = false;
            titleList.ForeColor = Color.White;
            titleList.Width = pnl_CoursesListChecked.Width- PADDING_LEFT_LIST;
            titleList.Height = 30;
            titleList.Text = "קורסים במערכת";
            titleList.TextAlign = ContentAlignment.MiddleCenter;
            titleList.Location = new Point(PADDING_LEFT_LIST, 10);
            titleList.Font = new Font(FONT_TEXT, 17, FontStyle.Bold | FontStyle.Underline);
            pnl_CoursesListChecked.Controls.Add(titleList);

            string[] names = getCoursesName();
            if (names == null) return;

            cards = new CourseCard[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                if(i == 0)
                    cards[i] = new CourseCard(this, getLessonsOfCourse(names[i]), 5, titleList.Location.Y + titleList.Height + 10);
                else
                    cards[i] = new CourseCard(this, getLessonsOfCourse(names[i]), cards[i-1].getLocation().X, cards[i - 1].getLocation().Y + cards[i - 1].getCard().Height + 10);

                pnl_CoursesListChecked.Controls.Add(cards[i].getCard());
            }
        }
        // CALL BACK - FUNCTIONS
        // reLocation the cards , after any open/close card
        public void reorderLocationCards_callback()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                if(i == 0)
                    cards[i].getCard().Location = new Point(PADDING_LEFT_LIST, titleList.Location.Y + titleList.Height + SPACE_CARDS);
                else
                    cards[i].getCard().Location = new Point(cards[i-1].getCard().Location.X, cards[i - 1].getCard().Location.Y + cards[i - 1].getCard().Height  + SPACE_CARDS);
                cards[i].getCard().Invalidate();
            }
        }
        // need to work on this function, find the currect panels, and disable them
        public void enableCards_callback(Lesson lsn ,bool enable)
        {
            //Panel[] arr = pnl_Lessons.ToArray();
            for (int i = 0; i < lessons.amount(); i++)
            {
                if (lessons[i].courseName.Equals(lsn.courseName))
                {
                    PanelLsn p = foundPanelLesson(lessons[i]);
                    if(p != null) {
                        if (!enable)
                            p.Hide();
                        else
                            p.Show();
                        p.Invalidate();
                    }
                }
            }
        }
        // init the lesson that should be showing
        public void setSelectedLessons_callback(Lesson lsn = null, bool dontAddThisLesson = false,bool updateSource = false )
        {
            if(lsn != null)
            {
                PanelLsn pn = pnl_Lessons.Find(ps => ps.lsn.courseName.Equals(lsn.courseName) && ps.lsn.type.Equals(lsn.type));
                pnl_Lessons.Remove(pn);
                pnl_Schedule.Controls.Remove(pn);
                pnl_Schedule.Invalidate();
                for (int i = 0; i < lessons.amount(); i++)
                {
                    if (lessons[i].courseName.Equals(lsn.courseName) && lessons[i].type.Equals(lsn.type)) { 
                        lessons.remove(lessons[i]);
                        break;
                    }
                }

                if (!dontAddThisLesson)
                {
                    addLessonToPanel(lsn);
                    lessons.add(lsn);
                }
                if (updateSource)
                {
                    mainForm.checkedLessonsFromCourses = lessons.getList();
                    mainForm.dataProgram.checkedCourses = lessons.getList();
                }
                checkCollision(lessons);
            }
            else{
                List<Lesson> list = new List<Lesson>();
                for (int i = 0; i < cards.Length; i++)
                {
                    Lesson[] arr = cards[i].getSelectedLessons();
                    for (int j = 0; j < arr.Length; j++)
                    {
                        if (arr[j] != null)
                        {
                            list.Add(arr[j]);
                        }
                    }
                }
                LessonList temp = new LessonList(list.ToArray());
                setSceduleByList(temp);
                return;
            }
        }

        // Set lessons from the list parameter to the schedule
        private void setSceduleByList(LessonList list)
        {
            removeAllLessonsFromPanel(lessons);

            if (list == null)
                list = lessons;
            if (lessons == null) return;
            lessons = list;

            for (int i = 0; i < lessons.amount(); i++)
            {
                addLessonToPanel(list[i]);
            }
            checkCollision(list);
        }
        // Add panel of lesson to the panels array
        private void addPanelToArray(ref PanelLsn p)
        {
            if(pnl_Lessons == null)
            {
                pnl_Lessons = new List<PanelLsn>();
                pnl_Lessons.Add(p);
                return;
            }

            if (p == null)
                p = new PanelLsn();

            pnl_Lessons.Add(p);
        }
        // init the properties data of the panel Lesson
        private void initPanelLessonData(ref PanelLsn p, Lesson lsn)
        {
            if(lsn == null) return;
            if (p == null) p = new PanelLsn();

            p.lsn = lsn;
            int dayIndex = Array.IndexOf(DAYS, lsn.getFullDay());
            if (dayIndex >= 0) p.Width = (days[dayIndex].Width - 1);

            p.Height = (hours[lsn.end - START_HOUR].Location.Y + hours[lsn.end - START_HOUR].Height) - (hours[lsn.start - START_HOUR].Location.Y + hours[lsn.start - START_HOUR].Height) - 2 * THICKNESS_HR_SEPERADE;
            p.RightToLeft = RightToLeft.Yes;
            p.Location = new Point(days[dayIndex].Location.X + 1, (hours[lsn.start - START_HOUR].Location.Y) + 1);
            // round edges panel
            p.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, p.Width, p.Height, 20, 20));

            // the content lables
            Label[] lbl_lessons = new Label[3]; // name+type, lecturer, className//GrowLabel[] lbl_lessons = new GrowLabel[3]; // name+type, lecturer, className
            for (int k = 0; k < lbl_lessons.Length; k++)
            {
                lbl_lessons[k] = new GrowLabel();
                lbl_lessons[k].AutoSize = false;
                lbl_lessons[k].Height = p.Height / 3;
                lbl_lessons[k].Width = p.Width;
                lbl_lessons[k].BackColor = Color.Transparent;
                lbl_lessons[k].TextAlign = ContentAlignment.MiddleCenter;

                int size = 10;
                switch (lsn.weekHour)
                {
                    case 1: size = 9; break;
                    case 2: size = 11; break;
                    case 3: size = 11; break;
                    case 4: size = 11; break;
                    case 5: size = 11; break;
                    default:
                        size = 13;
                        break;
                }
                lbl_lessons[k].Font = new Font(FONT_TEXT, size);

                switch (k)
                {
                    case 0: // course name
                        lbl_lessons[k].Text = lsn.type + "-" + lsn.courseName;
                        lbl_lessons[k].Location = new Point(0, 0);
                        lbl_lessons[k].TextAlign = ContentAlignment.MiddleCenter;
                        lbl_lessons[k].Font = new Font(lbl_lessons[k].Font, FontStyle.Underline | FontStyle.Bold);
                        break;
                    case 1: // lecturer name
                        lbl_lessons[k].Text = lsn.lecturer;
                        lbl_lessons[k].Location = new Point(0, p.Height / 2 - lbl_lessons[k].Height / 2);
                        break;
                    case 2: // class name
                        lbl_lessons[k].Text = "כיתה: " + lsn.className;
                        lbl_lessons[k].Location = new Point(0, p.Height - lbl_lessons[k].Height);
                        break;
                    default:
                        break;
                }
                if (!lbl_lessons[k].Text.Equals(""))
                {
                    if (lbl_lessons[k].Width != days[dayIndex].Width - 1)
                    {
                        if (lsn.weekHour == 1 || lsn.weekHour == 2)
                        {
                            if (k == 0 || k == 2)
                                p.Controls.Add(lbl_lessons[k]);
                        }
                        else
                            p.Controls.Add(lbl_lessons[k]);
                    }
                    else
                    {
                        if (lsn.weekHour == 1 && k != 1) p.Controls.Add(lbl_lessons[k]);
                        else if (lsn.weekHour > 1)
                            p.Controls.Add(lbl_lessons[k]);
                    }

                }
                p.BringToFront();

                for (int i = 0; i < cards.Length; i++)
                {
                    if (cards[i].getCourseName().Equals(lsn.courseName))
                    {
                        p.BackColor = cards[i].getCard().BackColor;
                        break;
                    }
                }
            }

            p.Tag = lsn.courseName + " " + lsn.type.Substring(0, 1);
        }
        // add lesson panel to the week schedule panel
        private void addLessonToPanel(Lesson lsn)
        {
            if (lsn == null || lessons.getLessons() == null || (pnl_Lessons != null && 
                pnl_Lessons.Find(ps => ps.lsn.courseName.Equals(lsn.courseName) && ps.lsn.type.Equals(lsn.type) && ps.lsn.getShortDay().Equals(lsn.getShortDay()) && ps.lsn.start == lsn.start && ps.lsn.end == lsn.end) != null)) return;

            PanelLsn p = new PanelLsn();
            initPanelLessonData(ref p, lsn);
            addPanelToArray(ref p);

            pnl_Schedule.Controls.Add(p);
            pnl_Lessons.ForEach(delegateToBringToFront);
            pnl_Schedule.Invalidate();
        }
        // delegate function to bring panel on the scedule panel
        private void delegateToBringToFront(PanelLsn p)
        {
            p.BringToFront();
        }

        // Remove all the lessons list parameter 
        private void removeAllLessonsFromPanel(LessonList list)
        {
            if (list == null && lessons == null) return;
            else if (list == null)
                list = lessons;

            for (int i = 0; i < list.amount(); i++)
            {
                removeLessonFromPanel(list[i]);
            }
        }
        // remove one panel from the pannel array
        private void removePanelFromArray(PanelLsn p)
        {
            if (pnl_Lessons == null || p == null)
            {
                return;
            }

            pnl_Lessons.Remove(p);
        }
        // Find the panel on the schedule panel and return it
        private PanelLsn foundPanelLesson(Lesson lsn)
        {
            if (pnl_Lessons == null) return null;
            return pnl_Lessons.Find(p => p.lsn.sameValue(lsn));
        }
        // Remove one panel list from the schedule panel
        private bool removeLessonFromPanel(Lesson lsn)
        {
            if (lsn == null) return true;

            PanelLsn p = foundPanelLesson(lsn);
            if (p != null)
            {
                removePanelFromArray(p);
                pnl_Schedule.Controls.Remove(p);
                pnl_Schedule.Invalidate();
                return true;
            }
            return false;
        }

        private void bringAllPanelsToFront()
        {
            foreach (var pnl in pnl_Lessons)
            {
                pnl.BringToFront();
            } 
        }
        private void checkCollision(LessonList list)
        {
            /*
            for (int i = 0; i < list.amount(); i++)
            {
                removOneCollision(list[i]);
            }
            */
            removeCollisions();
            collisions = new List<PanelCollision>();

            List<Lesson[]> cols = new List<Lesson[]>();
            list.hasCollision(ref cols);
            Lesson[][] listCollisions = cols.ToArray();

            for (int i = 0; i < listCollisions.Length; i++)
            {
                int day = 0;
                switch (listCollisions[i][0].getShortDay())
                {
                    case "א": day = 0; break;
                    case "ב": day = 1; break;
                    case "ג": day = 2; break;
                    case "ד": day = 3; break;
                    case "ה": day = 4; break;
                    case "ו": day = 5; break;
                    default:
                        break;
                }
                addCollision(listCollisions, listCollisions[i][0].start, listCollisions[i][0].end, day);
            }
        }
        private void addCollision(Lesson[][] list, int start, int end, int day)
        {
            Panel p = new Panel();
            p.RightToLeft = RightToLeft.Yes;
            int indexDay = day;
            int widthBorderPanel = 6;
            p.Width = days[indexDay].Width + widthBorderPanel;
            int LocX = days[indexDay].Location.X;
            int LocY1 = hours[start - START_HOUR].Location.Y;
            int LocY2 = hours[end - START_HOUR].Location.Y;// + hours[end - START_HOUR].Height;
            p.Height = LocY2 - LocY1 + widthBorderPanel;
            p.Location = new Point(LocX - widthBorderPanel/2, LocY1- widthBorderPanel/2);
            p.BackColor = Color.Transparent;
            p.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, p.Width, p.Height, 25, 25));
            p.Paint += ((s,e)=> {
                int widthBorder = 5;
                Color colorBorder = Color.Red;
                ButtonBorderStyle styleBorder = ButtonBorderStyle.Solid;
                ControlPaint.DrawBorder(e.Graphics, p.ClientRectangle, colorBorder, widthBorder, styleBorder, colorBorder, widthBorder, styleBorder, colorBorder, widthBorder, styleBorder, colorBorder, widthBorder, styleBorder);
            });

            PanelCollision pc = new PanelCollision();
            pc.RightToLeft = RightToLeft.Yes;
            int IconWith = 20;
            pc.Width = p.Width + IconWith;
            pc.Height = p.Height;
            pc.Location = new Point(p.Location.X - IconWith/2, p.Location.Y);
            p.Location = new Point(0 + IconWith/2, 0);

            pc.BackColor = Color.Transparent;
            
            PictureBox pictureBoxInfo = new PictureBox();
            pictureBoxInfo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxInfo.Location = new System.Drawing.Point(0, 0);
            pictureBoxInfo.Image = new Bitmap(Schedule.Properties.Resources.Info, IconWith, IconWith);
            pictureBoxInfo.Name = "pictureBoxInfoCollision";
            pictureBoxInfo.Size = new System.Drawing.Size(IconWith, IconWith);
            pictureBoxInfo.BackColor = Color.Transparent;
            pc.Controls.Add(pictureBoxInfo);
            pc.Controls.Add(p);
            pictureBoxInfo.BringToFront();

            // set ballon tooltip for info - the lesson that collision
            ToolTip CollisionToolTip = new ToolTip();

            char rle_char = (char)0x202B;//(RLT) RLE embedding // put at all start lines (included after \n)
            

            
            CollisionToolTip.UseFading = true;
            CollisionToolTip.UseAnimation = true;
            CollisionToolTip.IsBalloon = true;
            CollisionToolTip.ShowAlways = true;

            CollisionToolTip.AutomaticDelay = 0;
            CollisionToolTip.AutoPopDelay = 10000;
            CollisionToolTip.InitialDelay = 100;
            CollisionToolTip.ReshowDelay = 500;
            
            string massage =  "";
            int j = 0;
            for (int i = 0 ; i < list.Length; i++)
            {
                int dayIndex = 0;
                switch (list[i][0].getShortDay())
                {
                    case "א": dayIndex = 0; break;
                    case "ב": dayIndex = 1; break;
                    case "ג": dayIndex = 2; break;
                    case "ד": dayIndex = 3; break;
                    case "ה": dayIndex = 4; break;
                    case "ו": dayIndex = 5; break;
                    default:
                        break;
                }

                if ((dayIndex == day && list[i][0].start >= start && list[i][0].end <= end) || 
                   (dayIndex == day && list[i][1].start >= start && list[i][1].end <= end))
                { 
                    massage += (j++ + 1) + ". " + list[i][0].ToString() + "--------------------\n" + rle_char;
                    massage += (j++ + 1) + ". " + list[i][1].ToString() + "--------------------\n" + rle_char;
                    pc.lsn1 = list[i][0];
                    pc.lsn2 = list[i][1];
                }
            }

            string titleToolTip = (j + " - שיעורים המתנגשים");
            titleToolTip = rle_char + titleToolTip;
            CollisionToolTip.ToolTipTitle = titleToolTip;
            massage = rle_char + massage;

            CollisionToolTip.SetToolTip(pictureBoxInfo, massage);
            CollisionToolTip.SetToolTip(p, massage);

            collisions.Add(pc);
            pnl_Schedule.Controls.Add(pc);
            pc.BringToFront();
            bringAllPanelsToFront();
        }

        private bool removOneCollision(Lesson lsn)
        {
            if (lsn == null || collisions == null) return false;
            foreach (var col in collisions)
            {
                if((col.lsn1 != null && lsn.sameValue(col.lsn1)) || (col.lsn2 != null && lsn.sameValue(col.lsn2)))
                {
                    pnl_Schedule.Controls.Remove(col);
                    pnl_Schedule.Invalidate();
                    collisions.Remove(col);
                    return true;
                }
            }
            return false;
        }
        
        private void removeCollisions()
        {
            if(collisions != null)
                foreach (var col in collisions)
                {
                    pnl_Schedule.Controls.Remove(col);
                }
            collisions = new List<PanelCollision>();
        }

        // ------------------------------------------------------------------------
        // panel that holder a lesson - to present it on the scdedule panel
        class PanelLsn : Panel
        {
            public Lesson lsn { get; set; }
        }

        class PanelCollision : Panel
        {
            public Lesson lsn1 { get; set; }
            public Lesson lsn2 { get; set; }
        }
    }
}

/*
        // dident used - For to wrapped text (enter at the end of the each row)
        /// <summary>
        /// Word wraps the given text to fit within the specified width.
        /// </summary>
        /// <param name="text">Text to be word wrapped</param>
        /// <param name="width">Width, in characters, to which the text
        /// should be word wrapped</param>
        /// <returns>The modified text</returns>
        public static string WordWrap(string text, int width)
        {
            int pos, next;
            StringBuilder sb = new StringBuilder();

            // Lucidity check
            if (width < 1)
                return text;

            // Parse each line of text
            for (pos = 0; pos < text.Length; pos = next)
            {
                // Find end of line
                int eol = text.IndexOf(Environment.NewLine, pos);
                if (eol == -1)
                    next = eol = text.Length;
                else
                    next = eol + Environment.NewLine.Length;

                // Copy this line of text, breaking into smaller lines as needed
                if (eol > pos)
                {
                    do
                    {
                        int len = eol - pos;
                        if (len > width)
                            len = BreakLine(text, pos, width);
                        sb.Append(text, pos, len);
                        sb.Append(Environment.NewLine);

                        // Trim whitespace following break
                        pos += len;
                        while (pos < eol && Char.IsWhiteSpace(text[pos]))
                            pos++;
                    } while (eol > pos);
                }
                else sb.Append(Environment.NewLine); // Empty line
            }
            return sb.ToString();
        }

        /// <summary>
        /// Locates position to break the given line so as to avoid
        /// breaking words.
        /// </summary>
        /// <param name="text">String that contains line of text</param>
        /// <param name="pos">Index where line of text starts</param>
        /// <param name="max">Maximum line length</param>
        /// <returns>The modified line length</returns>
        private static int BreakLine(string text, int pos, int max)
        {
            // Find last whitespace in line
            int i = max;
            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
                i--;

            // If no whitespace found, break at maximum length
            if (i < 0)
                return max;

            // Find start of whitespace
            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
                i--;

            // Return length of text before whitespace
            return i + 1;
        }
        */

/*
        // for draw shapes on the panel
        pnl_Lessons[i].Paint += ((s, e) =>
        {
            //With rounded corners
            Graphics v = e.Graphics;
            DrawRoundRect(v, Pens.Blue, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1, 10);
            //Without rounded corners
            e.Graphics.DrawRectangle(Pens.Blue, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
        });
                    
        public void DrawRoundRect(Graphics g, Pen p, float X, float Y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(X + radius, Y, X + width - (radius * 2), Y);
            gp.AddArc(X + width - (radius * 2), Y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(X + width, Y + radius, X + width, Y + height - (radius * 2));
            gp.AddArc(X + width - (radius * 2), Y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(X + width - (radius * 2), Y + height, X + radius, Y + height);
            gp.AddArc(X, Y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(X, Y + height - (radius * 2), X, Y + radius);
            gp.AddArc(X, Y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            g.DrawPath(p, gp);
            gp.Dispose();
        }
        */
