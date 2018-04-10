using Schedule.Lessons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Schedule.CoursesList
{

    // Collapse all : Ctrl + M + O
    // Open all     : Ctrl + M + L
    public class PanelCoursesList
    {
        // Default Value:
        public static int  WIDTH_WINDOWS = 1040,
                           HEIGHT_WINDOWS = 746;

        static int HEIGHT_ROW = 25,
                    ROW_SPACES = 14,
                    SCROLLBAR_VERTICAL_WIDTH = 5,
                    SPACE_CARDS = 5,
                    PADDING_LEFT_LIST = 20;

        static int  PNL_TITLE_WIDTH = 1020 , 
                    PNL_TITLE_HEIGHT= 34, 
                    LOC_PNL_TITLE_X = 08, 
                    LOC_PNL_TITLE_Y = 125,

                    PNL_LIST_WIDTH = PNL_TITLE_WIDTH, 
                    LOC_PNL_LIST_X = LOC_PNL_TITLE_X, 
                    LOC_PNL_LIST_Y = LOC_PNL_TITLE_Y + PNL_TITLE_HEIGHT + 5,
                    PNL_LIST_HEIGHT = 691 - LOC_PNL_LIST_Y - PNL_TITLE_HEIGHT,

                    PNL_ADD_WIDTH = 1020,
                    PNL_ADD_HEIGHT = 34,
                    LOC_PNL_ADD_X = 08,
                    LOC_PNL_ADD_Y = LOC_PNL_LIST_Y + PNL_LIST_HEIGHT,

                    PNL_MAIN_WIDTH = WIDTH_WINDOWS,
                    PNL_MAIN_HEIGHT = HEIGHT_WINDOWS, 
                    LOC_PNL_MAIN_X = 0, 
                    LOC_PNL_MAIN_Y = 0;
        static string FONT_TEXT = "Guttman Hatzvi";

        static Color COLOR_TITLE_HOVER_BACK = (Color)System.Drawing.ColorTranslator.FromHtml("#f1f74c"),
                     COLOR_TITLE_HOVER_FORE = (Color)System.Drawing.ColorTranslator.FromHtml("#000000"),
                     COLOR_TEXT_TITLE = (Color)System.Drawing.ColorTranslator.FromHtml("#a7c0e8"),
                     COLOR_TEXT = (Color)System.Drawing.ColorTranslator.FromHtml("#FFFFFF"),
                     BACK_COLOR_TEXT_FOR_EVEN_ROW = (Color)System.Drawing.ColorTranslator.FromHtml("#a6a7a8"),
                     BACK_COLOR_TEXT_FOR_ODD_ROW = (Color)System.Drawing.ColorTranslator.FromHtml("#373738");

        static string[] TITLES = { "בחר", "שורה", "שנה" , "שם קורס", "מרצה" ,"יום", "סיום", "התחלה","שש" , "כיתה", "מס", "מחק"};
        // ----------------------------------------------------------------------------------------------------
        // Variables 
        Panel pnl_Main, 
              pnl_courseList, 
              pnl_titleList, 
              pnl_addCourse;

        LessonList lessons = null;
        LessonList lessonChecks = new LessonList();
        Label lbl_checkedCourses = new Label(), lbl_empty_info;
        CourseItem[] cards;

        Form1 mainForm;

        // CTOR -----------------------------------------------------------------------------
        public PanelCoursesList(Form1 mf ,LessonList lessonsList = null)
        {
            mainForm = mf;

            // To init the panels for including data in them
            init_panels_properties();

            // Init lable of the status checked courses
            lbl_checkedCourses.RightToLeft = RightToLeft.Yes;
            lbl_checkedCourses.ForeColor = COLOR_TEXT;
            lbl_checkedCourses.Text = "רואים אותי?";
            lbl_checkedCourses.Location = new Point(402, 66);
            lbl_checkedCourses.Font = new Font(FONT_TEXT, 30, FontStyle.Underline | FontStyle.Bold);
            lbl_checkedCourses.AutoSize = true;


            // To get the lessons list from the parameter that passed to this CTOR , or Init in the Mocks data for debug
            if (lessonsList != null) lessons = lessonsList; // pass from another form to here
            //else
            //{
            //    DialogResult result = MessageBox.Show("כרגע אין שום נתונים שטענת מקובץ של רשימת קורסים, האם תרצה לטעון קבצים להמחשה בלבד?", "טעינת נתונים להמחשה", MessageBoxButtons.YesNo);
            //    if (result == DialogResult.Yes)
            //    {
            //        init_Mock(); // MOCK - STATIC DATA
            //    }
            //}

            // Init the data that we have in the Lesson List
            init_List();

            // to add the init data to the main panel for display them
            pnl_Main.Controls.Add(lbl_checkedCourses);
            pnl_Main.Controls.Add(pnl_courseList);

            // Init the title of the checked courses 
            updateChecksCourse();

            checkToSetEmptyInfo();
            reorderLocationCards_callback();
        }

        public void setNewList(LessonList list)
        {
            pnl_courseList.Controls.Clear();

            lessons = list;
            init_List();
            updateChecksCourse();
            checkToSetEmptyInfo();
            reorderLocationCards_callback();
        }

        // Get the lessons that User choose
        public LessonList getLessons()
        {
            return lessonChecks;
        }
        // Replace all the list with the lesson of the parameter LessonList
        public void setLessons(LessonList lessonsList)
        {
            removeAllDataFromPanel();
            lessons = lessonsList;
            init_List();
            checkToSetEmptyInfo();
        }
        // Remove all the data list and the background from the panel
        private void removeAllDataFromPanel()
        {
            for (int i = 0; lessons != null && i < lessons.amount(); i++)
                //removeCourseFromPanel(i);
            checkToSetEmptyInfo();
        }
        // Return the main panel of this page - to add this panel for the platform display
        public Panel getPanel()
        {
            return pnl_Main;
        }
        // Return the lessons that selected by user
        public LessonList getLessonsSelected()
        {
            return lessonChecks;
        }

        // = Initializes =====================================================================================
        // Init the Panels' Properties
        private void init_panels_properties()
        {
            // The main panel - contain the pnl_courseList ,pnl_titleList ,pnl_addCourse 
            pnl_Main = new Panel();
            pnl_Main.Location = new Point(LOC_PNL_MAIN_X, LOC_PNL_MAIN_Y);
            pnl_Main.BackColor = Color.Transparent;
            pnl_Main.Dock = DockStyle.Fill;

            // The courses List panel - contain list of the LessonList parameter
            pnl_courseList = new Panel();
            pnl_courseList.AutoScroll = true;
            pnl_courseList.Location = new Point(LOC_PNL_LIST_X, LOC_PNL_LIST_Y);
            pnl_courseList.Width = PNL_LIST_WIDTH;
            pnl_courseList.Height = PNL_LIST_HEIGHT;
            pnl_courseList.BackColor = Color.Transparent;
            pnl_courseList.AutoScroll = true;
            pnl_courseList.RightToLeft = RightToLeft.Yes;
            
            // The titles List Panel - contain the first row that represent what is each column
            pnl_titleList = new Panel();
            pnl_titleList.Location = new Point(LOC_PNL_TITLE_X, LOC_PNL_TITLE_Y);
            pnl_titleList.Width = PNL_TITLE_WIDTH;
            pnl_titleList.Height = PNL_TITLE_HEIGHT;
            pnl_titleList.BackColor = Color.Transparent;
            pnl_titleList.AutoScroll = true;
            pnl_titleList.RightToLeft = RightToLeft.Yes;
            pnl_titleList.AutoScrollPosition = new Point(LOC_PNL_TITLE_X - 5, LOC_PNL_LIST_Y);

            // The add Course panel - contain the row in the bottom main panel for adding new row of course
            pnl_addCourse = new Panel();
            pnl_addCourse.Location = new Point(LOC_PNL_ADD_X, LOC_PNL_ADD_Y);
            pnl_addCourse.Width = PNL_ADD_WIDTH;
            pnl_addCourse.Height = PNL_ADD_HEIGHT;
            pnl_addCourse.BackColor = Color.Transparent;
            pnl_addCourse.AutoScroll = true;
            pnl_addCourse.RightToLeft = RightToLeft.Yes;
            pnl_addCourse.AutoScrollPosition = new Point(LOC_PNL_TITLE_X - 5, LOC_PNL_LIST_Y);
        }
        // Get true if the scroolbar vertical is displyed , of false if not
        private bool scroolVisible()
        {
            return (0 + ROW_SPACES + (lessons.amount() * HEIGHT_ROW + (lessons.amount() - 1) * ROW_SPACES)) > pnl_courseList.Height;
        }
        
        // Some data mock for debug
        private void init_Mock()
        {
            lessons = new LessonList();

            lessons = new LessonList(12);
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
            lessons[1].day = "א";
            lessons[1].end = 14;
            lessons[1].start = 12;
            lessons[1].type = "ה";
            lessons[1].weekHour = 2;
            lessons[1].year = 2;

            lessons[2] = new Lesson();
            lessons[2].courseName = "קומפילציה";
            lessons[2].lecturer = "אבישי";
            lessons[2].number = 1;
            lessons[2].className = "S100";
            lessons[2].day = "שני";
            lessons[2].end = 10;
            lessons[2].start = 8;
            lessons[2].type = "מ";
            lessons[2].weekHour = 2;
            lessons[2].year = 2;

            lessons[3] = new Lesson();
            lessons[3].courseName = "אלגוריתמים 2";
            lessons[3].lecturer = "נטליה ונטיק";
            lessons[3].number = 1;
            lessons[3].className = "S202";
            lessons[3].day = "ה";
            lessons[3].end = 10;
            lessons[3].start = 8;
            lessons[3].type = "ה";
            lessons[3].weekHour = 2;
            lessons[3].year = 4;

            lessons[4] = new Lesson();
            lessons[4].courseName = "אלגוריתמים 2";
            lessons[4].lecturer = "בוריס";
            lessons[4].number = 1;
            lessons[4].className = "כלניות";
            lessons[4].day = "ד";
            lessons[4].end = 10;
            lessons[4].start = 8;
            lessons[4].type = "ה";
            lessons[4].weekHour = 2;
            lessons[4].year = 4;

            lessons[5] = new Lesson();
            lessons[5].courseName = "בדיקות";
            lessons[5].lecturer = "הדס חסידים";
            lessons[5].number = 1;
            lessons[5].className = "A30";
            lessons[5].day = "ב";
            lessons[5].end = 16;
            lessons[5].start = 13;
            lessons[5].type = "ת";
            lessons[5].weekHour = 3;
            lessons[5].year = 3;

            lessons[6] = new Lesson();
            lessons[6].courseName = "בדיקות";
            lessons[6].lecturer = "אלן סלומון";
            lessons[6].number = 2;
            lessons[6].className = "P132";
            lessons[6].day = "ב";
            lessons[6].end = 10;
            lessons[6].start = 8;
            lessons[6].type = "ת";
            lessons[6].weekHour = 2;
            lessons[6].year = 3;

            lessons[7] = new Lesson();
            lessons[7].courseName = "בדיקות";
            lessons[7].lecturer = "אלן סלומון";
            lessons[7].number = 3;
            lessons[7].className = "P135";
            lessons[7].day = "א";
            lessons[7].end = 21;
            lessons[7].start = 17;
            lessons[7].type = "ת";
            lessons[7].weekHour = 4;
            lessons[7].year = 3;

            lessons[8] = new Lesson();
            lessons[8].courseName = "בדיקות";
            lessons[8].lecturer = "נורית גלה";
            lessons[8].number = 1;
            lessons[8].className = "null";
            lessons[8].day = "ד";
            lessons[8].end = 10;
            lessons[8].start = 8;
            lessons[8].type = "ה";
            lessons[8].weekHour = 2;
            lessons[8].year = 3;

            lessons[9] = new Lesson();
            lessons[9].courseName = "שפת C";
            lessons[9].lecturer = "סבטלנה טנייה";
            lessons[9].number = 1;
            lessons[9].className = "NULL";
            lessons[9].day = "א";
            lessons[9].end = 10;
            lessons[9].start = 8;
            lessons[9].type = "ה";
            lessons[9].weekHour = 2;
            lessons[9].year = 3;

            lessons[10] = new Lesson();
            lessons[10].courseName = "שפת C";
            lessons[10].lecturer = "סבטלנה טנייה";
            lessons[10].number = 1;
            lessons[10].className = "NULL";
            lessons[10].day = "א";
            lessons[10].end = 10;
            lessons[10].start = 8;
            lessons[10].type = "ה";
            lessons[10].weekHour = 2;
            lessons[10].year = 3;

            lessons[11] = new Lesson();
            lessons[11].courseName = "שפת C";
            lessons[11].lecturer = "סבטלנה טנייה";
            lessons[11].number = 1;
            lessons[11].className = "NULL";
            lessons[11].day = "א";
            lessons[11].end = 10;
            lessons[11].start = 8;
            lessons[11].type = "ה";
            lessons[11].weekHour = 2;
            lessons[11].year = 3;
        }
        // Init the data of the list in rows in the panel 
        private void init_List()
        {
            // check if there is data to init
            if (lessons == null || lessons.amount() <= 0) return;

            string[] names = getCoursesName();
            if (names == null) return;
            cards = new CourseItem[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                if (i == 0)
                    cards[i] = new CourseItem(this, getLessonsOfCourse(names[i]), 5, 0 + 10);
                else
                    cards[i] = new CourseItem(this, getLessonsOfCourse(names[i]), cards[i - 1].getLocation().X, cards[i - 1].getLocation().Y + cards[i - 1].getCard().Height + 10);

                pnl_courseList.Controls.Add(cards[i].getCard());
            }
            
            checkToSetEmptyInfo();
        }
        // Return array of courses names (cards)
        private string[] getCoursesName()
        {
            List<string> names = new List<string>();
            for (int i = 0; i < lessons.amount(); i++)
            {
                if (names.Find(x => x.Equals(lessons[i].courseName.ToString())) == null)
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
        
        // Init the empty info, if there isn't any lesson in the list
        private void checkToSetEmptyInfo()
        {
            if (lbl_empty_info == null)
            {
                lbl_empty_info = new Label();
                lbl_empty_info.Text = "לא נמצא שום שיעור לתצוגה";
                lbl_empty_info.Font = new Font(FONT_TEXT, 30, FontStyle.Underline | FontStyle.Bold);
                lbl_empty_info.ForeColor = Color.DarkRed;
                lbl_empty_info.BackColor = Color.Transparent;
                lbl_empty_info.RightToLeft = RightToLeft.Yes;
                lbl_empty_info.AutoSize = true;
                lbl_empty_info.BringToFront();
                Point p = new Point((pnl_courseList.Width / 2) - (lbl_empty_info.Width * 2 + lbl_empty_info.Width / 2),
                                    (lbl_empty_info.Height * 3));
                lbl_empty_info.Location = p;
                pnl_courseList.Controls.Add(lbl_empty_info);
            }
            if (lessons == null || lessons.amount() == 0)
            {
                lbl_empty_info.Visible = true;
                lbl_checkedCourses.Visible = false;
                lbl_empty_info.Invalidate();
            }
            else
            {
                lbl_checkedCourses.Visible = true;
                lbl_empty_info.Visible = false;
                lbl_empty_info.Invalidate();
            }

        }
        private void updateChecksCourse()
        {
            lbl_checkedCourses.Text = "נבחרו: " + lessonChecks.amount() + " שיעורים.";
            if(mainForm.checkedCourses == null || lessonChecks != mainForm.checkedCourses) {
                mainForm.checkedCourses = lessonChecks;
                mainForm.dataProgram.checkedCourses = lessonChecks.getList();
                mainForm.newCheckingCoursesDone = true;
            }
        }
        // Set selection on certain Lessons
        public void setSelectionByLessonsList(LessonList list)
        {
            if (list == null || list.amount() <= 0 || lessons == null) return;
            for (int j = 0;  j < lessons.amount() ; j++)
            {
                for (int i = 0; i < list.amount(); i++)
                {
                    if(list[i].sameValue(lessons[j]))
                    {
                        for (int k = 0; k < cards.Length; k++)
                        {
                            if (cards[k].getCourseName().Equals(list[i].courseName))
                            {
                                if (cards[k].getChecked() != true)
                                {
                                    cards[k].Checked(true);
                                }
                                break;
                            }
                        }
                        lessonChecks.add(lessons[j]);
                        break;
                    }
                }
            }
            updateChecksCourse();
        }

        // ==================================================================================================
        // Enable/Disenble the display of the scroolbar vertical of the panel list 
        private void ScrollVisiblePanels()
        {
            if (scroolVisible()) // display
            {
                pnl_titleList.Location = new Point(LOC_PNL_TITLE_X + SCROLLBAR_VERTICAL_WIDTH + 5, LOC_PNL_TITLE_Y);
                pnl_titleList.Invalidate();

                pnl_courseList.Location = new Point(LOC_PNL_LIST_X + SCROLLBAR_VERTICAL_WIDTH, LOC_PNL_LIST_Y);
                pnl_courseList.Invalidate();

                pnl_addCourse.Location = new Point(LOC_PNL_ADD_X + SCROLLBAR_VERTICAL_WIDTH + 5, LOC_PNL_ADD_Y);
                pnl_addCourse.Invalidate();
            }
            else // not display
            {
                pnl_titleList.Location = new Point(LOC_PNL_TITLE_X, LOC_PNL_TITLE_Y);
                pnl_titleList.Invalidate();

                pnl_courseList.Location = new Point(LOC_PNL_LIST_X , LOC_PNL_LIST_Y);
                pnl_courseList.Invalidate();

                pnl_addCourse.Location = new Point(LOC_PNL_ADD_X, LOC_PNL_ADD_Y);
                pnl_addCourse.Invalidate();
            }
        }

        // == help method to add and remove course row from panel =========================================
        
        // -----------------------------------------------------------------------------


        // CALL BACK - FUNCTIONS
        // reLocation the cards , after any open/close card
        public void reorderLocationCards_callback()
        {
            for (int i = 0; cards != null && i < cards.Length; i++)
            {
                if (i == 0)
                    cards[i].getCard().Location = new Point(PADDING_LEFT_LIST, 0 + SPACE_CARDS);
                else
                    cards[i].getCard().Location = new Point(cards[i - 1].getCard().Location.X, cards[i - 1].getCard().Location.Y + cards[i - 1].getCard().Height + SPACE_CARDS);
                cards[i].getCard().Invalidate();
            }
        }
        // init the lesson that should be showing
        public void setSelectedLessons_callback()
        {
            List<Lesson> list = new List<Lesson>();
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i].getChecked())
                {
                    Lesson[] arr = cards[i].getSelectedlessonList();
                    for (int j = 0; arr != null && j < arr.Length; j++)
                    {
                        if (arr[j] != null)
                        {
                            list.Add(arr[j]);
                        }
                    }
                }
            }
            lessonChecks = new LessonList(list.ToArray());
            updateChecksCourse();
        }

    }
}
