using Schedule.Lessons;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Schedule
{

    // Collapse all : Ctrl + M + O
    // Open all     : Ctrl + M + L
    public class CoursesListBackup
    {
        // Default Value:
        public static int  WIDTH_WINDOWS = 1040,
                           HEIGHT_WINDOWS = 746;

        static int HEIGHT_ROW = 25,
                    ROW_SPACES = 14,
                    SIZE_FONT_TEXT = 10,
                    SIZE_FONT_TEXT_TITLE = 13,
                    MAX_SIZE_FONT_TEXT = 20,
                    MIN_SIZE_FONT_TEXT = 8,
                    COL_SPACE = 4,
                    SCROLLBAR_VERTICAL_WIDTH = 5;

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

        bool[] sortOpposite;

        LessonList lessons = null;
        LessonList lessonChecks = new LessonList();

        TextBox[][] editLables;
        TextBox[] addCourseLables;

        Label[] hrs , 
                labelsBackgroungEvenRow , 
                lableMenuList;

        Label lbl_checkedCourses = new Label(), lbl_empty_info;

        Button[] deleteButtons;
        Button btn_edit, btn_cancelEdit, btn_save, btn_addNewCourse;
        
        Bunifu.Framework.UI.BunifuCheckbox[] checkButtons;

        // CTOR -----------------------------------------------------------------------------
        public CoursesListBackup(LessonList lessonsList = null)
        {
            // To init the panels for including data in them
            init_panels_properties();

            // Init lable of the status checked courses
            lbl_checkedCourses.RightToLeft = RightToLeft.Yes;
            lbl_checkedCourses.ForeColor = COLOR_TEXT;
            lbl_checkedCourses.Location = new Point(402, 66);
            lbl_checkedCourses.Font = new Font(FONT_TEXT, 30, FontStyle.Underline | FontStyle.Bold);
            lbl_checkedCourses.AutoSize = true;
            
            // To init the titles ( the columns of the list )
            init_Titles();

            // To get the lessons list from the parameter that passed to this CTOR , or Init in the Mocks data for debug
            if (lessonsList != null) lessons = lessonsList; // pass from another form to here
            else                     init_Mock();           // MOCK - STATIC DATA

            // Init the row of adding a new course , that display in the bottom place of the panel
            init_AddNewCourseRow();

            // Init the data that we have in the Lesson List
            init_List();

            // init the Buttons of the Edit, Save, Cancel
            init_buttons();

            // dont work on pnl_Main
            // to Zoom in/out the text in the list
            pnl_Main.PreviewKeyDown += new PreviewKeyDownEventHandler((sender, e) =>
            {
                lbl_checkedCourses.Text = "click on " + e.KeyData.ToString();
                switch (e.KeyData)
                {
                    case Keys.Add:
                        btn_zoomIn_Click(sender, e); // need to update for all labels and textbox
                        break;
                    case Keys.Subtract:
                        btn_zoomOut_Click(sender, e); // need to update for all labels and textbox
                        break;
                }
            });

            // to add the init data to the main panel for display them
            pnl_Main.Controls.Add(lbl_checkedCourses);
            pnl_Main.Controls.Add(pnl_courseList);
            pnl_Main.Controls.Add(pnl_titleList);
            pnl_Main.Controls.Add(pnl_addCourse);

            // Init the title of the checked courses 
            updateChecksCourse();

            checkToSetEmptyInfo();
            btn_edit.Visible = true;
            btn_save.Visible = false;
            btn_cancelEdit.Visible = false;
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
                removeCourseFromPanel(i);
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
        // Init the titles of the columns for the lesson list 
        private void init_Titles()
        {
            // init the size of the array of labels title , depend on the string title in the DEFAULT VARIABLE
            lableMenuList = new Label[TITLES.Length];

            // init each title with there properties and size and text
            for (int i = 0; i < lableMenuList.Length; i++)
            {
                lableMenuList[i] = new Label();
                lableMenuList[i].ForeColor = COLOR_TEXT_TITLE;
                lableMenuList[i].Font = new Font(FONT_TEXT, SIZE_FONT_TEXT_TITLE, FontStyle.Underline | FontStyle.Bold);
                lableMenuList[i].Text = TITLES[i];
                lableMenuList[i].AutoSize = false;
                lableMenuList[i].Height = PNL_TITLE_HEIGHT-3;
                lableMenuList[i].TextAlign = ContentAlignment.MiddleCenter;
                lableMenuList[i].BorderStyle = BorderStyle.Fixed3D;

                switch (i)
                {
                    case 0:  lableMenuList[i].Width = 45;   break;  // numRow
                    case 1:  lableMenuList[i].Width = 45;   break;  // checkbox
                    case 2:  lableMenuList[i].Width = 45;   break;  // year 
                    case 3:  lableMenuList[i].Width = 178;  break;  // name course
                    case 4:  lableMenuList[i].Width = 178;  break;  // lecturer
                    case 5:  lableMenuList[i].Width = 74;   break;  // day
                    case 6:  lableMenuList[i].Width = 70;   break;  // end
                    case 7:  lableMenuList[i].Width = 70;   break;  // start
                    case 8:  lableMenuList[i].Width = 46;   break;  // hours
                    case 9:  lableMenuList[i].Width = 103;  break;  // classes
                    case 10: lableMenuList[i].Width = 45;   break;  // number 
                    case 11: lableMenuList[i].Width = 50;   break;  // delete
                    default: lableMenuList[i].AutoSize = true; break;
                }

                // click event on the specific titles
                lableMenuList[i].Click += new EventHandler((sender, ev) =>
                {
                    Label clickedLabel = sender as Label;
                    int j = findLableInTitleList(clickedLabel);
                    string sortby = "";
                    switch (j)
                    {
                        case 0: sortby = ""; break;
                        case 2: sortby = "year"; break;
                        case 3: sortby = "course"; break;
                        case 4: sortby = "lecturer"; break;
                        case 5: sortby = "day"; break;
                        case 6: sortby = "end"; break;
                        case 7: sortby = "start"; break;
                        case 8: sortby = "hours"; break;
                        case 9: sortby = "class"; break;                      
                        default:
                            break;
                    }
                    switch (j)
                    {
                        case 0: // checkButton
                        case 2: // year
                        case 3: // course
                        case 4: // lecturer
                        case 5: // day
                        case 6: // end
                        case 7: // start
                        case 8: // hours
                        case 9: // class
                            MouseClickTitleLabel(clickedLabel, sortby);
                            break;
                        default:
                            break;
                    }
                });

                // Mouse enter event to the specific titles -  set color
                lableMenuList[i].MouseEnter += new EventHandler((sender, ev) =>
                {
                    Label clickedLabel = sender as Label;
                    int j = findLableInTitleList(clickedLabel);
                    switch (j)
                    {
                        case 0: // checkButton
                        case 2: // year
                        case 3: // course
                        case 4: // lecturer
                        case 5: // day
                        case 6: // end
                        case 7: // start
                        case 8: // hours
                        case 9: // class
                            mouseEnterTitleLabe(lableMenuList[j]);
                            break;
                        default:
                            break;
                    }
                });

                // Mouse enter event from the specific titles - return transperent color
                lableMenuList[i].MouseLeave += new EventHandler((sender, ev) =>
                {
                    Label clickedLabel = sender as Label;
                    int j = findLableInTitleList(clickedLabel);
                    switch (j)
                    {
                        case 0: // checkButton
                        case 2: // year
                        case 3: // course
                        case 4: // lecturer
                        case 5: // day
                        case 6: // end
                        case 7: // start
                        case 8: // hours
                        case 9: // class
                            mouseLeaveTitleLabe(lableMenuList[j]);
                            break;
                        default:
                            break;
                    }
                });
            }
            // init the location of the title and of the each columns list depends on the width of the titles
            int titleLocationY = 0;
            lableMenuList[lableMenuList.Length-1].Location = new Point(22, titleLocationY);
            for (int k = lableMenuList.Length - 2; k >= 0; k--)
                lableMenuList[k].Location = new Point(lableMenuList[k + 1].Location.X + lableMenuList[k + 1].Width + COL_SPACE, titleLocationY);

            // add each title to the panel of the Title List Panel
            for (int i = 0; i < lableMenuList.Length; i++)
                pnl_titleList.Controls.Add(lableMenuList[i]);
            
            // init the toggle click of the buttons variables
            sortOpposite = new bool[lableMenuList.Length];
            for (int i = 0; i < sortOpposite.Length; i++)
                sortOpposite[i] = false;
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
            lessons[8].className = "";
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
            lessons[9].className = "";
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
            lessons[10].className = "";
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
            lessons[11].className = "";
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

            // initilize the variable to edit them
            checkButtons = new Bunifu.Framework.UI.BunifuCheckbox[lessons.amount()]; // the check button for adding course to the list checked course
            editLables = new TextBox[lessons.amount()][]; // the data of the list , for displaying and for editing
            deleteButtons = new Button[lessons.amount()]; // the delete buttons to remove course row from the general list
            hrs = new Label[lessons.amount()]; // the horizontal line for seperating between rows

            // init the rows of the course list 
            for (int i = 0; i < lessons.amount(); i++)
            {
                editLables[i] = new TextBox[lableMenuList.Length - 2];
                addCourseToPanel(i);
            }
            checkToSetEmptyInfo();
        }
        // Init the row of adding a new course 
        private void init_AddNewCourseRow()
        {
            addCourseLables = new TextBox[lableMenuList.Length - 3 + 1]; // the row to adding a new course to the general list

            for (int i = 0, k = 1; i < addCourseLables.Length; i++, k++)
            {
                addCourseLables[i] = new TextBox();
                addCourseLables[i].Multiline = true;
                addCourseLables[i].AutoSize = false;
                addCourseLables[i].Location = new Point(lableMenuList[k].Location.X, 2);
                addCourseLables[i].Height = lableMenuList[k].Height;
                if(i == 0)  addCourseLables[i].Width = 60;
                else        addCourseLables[i].Width = lableMenuList[k].Width;
                addCourseLables[i].RightToLeft = RightToLeft.Yes;
                addCourseLables[i].Visible = true;
                addCourseLables[i].WordWrap = true;
                addCourseLables[i].ScrollBars = ScrollBars.Vertical;
                addCourseLables[i].Font = new Font(FONT_TEXT, 12, FontStyle.Bold);
                addCourseLables[i].Invalidate();
                addCourseLables[i].Tag = i + 1;
                addCourseLables[i].BackColor = COLOR_TEXT_TITLE;
                addCourseLables[i].ForeColor = Color.Gray;

                string typeTitle = "ה/מ/ת";
                // when the check box get focus , set the text in black color and remove the placeholder text
                addCourseLables[i].GotFocus += new EventHandler((sender, e) => {
                    // Remove Text
                    TextBox textBoxTitle = sender as TextBox;
                    if (String.IsNullOrWhiteSpace(textBoxTitle.Text) || textBoxTitle.Text.Equals(TITLES[(int)(textBoxTitle.Tag)]) || textBoxTitle.Text.Equals(typeTitle))
                    {
                        textBoxTitle.Text = "";
                    }
                    textBoxTitle.ForeColor = Color.Black;
                });
                // when the check box lost focus , set the text placeholder in Gray color and add the placeholder text if is empty
                addCourseLables[i].LostFocus += new EventHandler((sender, e) => {
                    // Add PlaceHoder Text
                    TextBox textBoxTitle = sender as TextBox;
                    if (String.IsNullOrWhiteSpace(textBoxTitle.Text))
                    {
                        if((int)(textBoxTitle.Tag) == 1) textBoxTitle.Text = typeTitle;
                        else                             textBoxTitle.Text = TITLES[(int)(textBoxTitle.Tag)];
                        textBoxTitle.ForeColor = Color.Gray;
                    }
                });
                addCourseLables[i].ScrollBars = ScrollBars.Horizontal;
                if(i == 0) addCourseLables[i].Text = typeTitle;
                else addCourseLables[i].Text = TITLES[k];

                bool getTouch = false;
                switch (i)
                {
                    case 0: // type
                        addCourseLables[i].KeyPress += ((sender, e) =>
                        {
                            TextBox tb = sender as TextBox;
                            typePress(sender, e, tb);
                        });
                        break;
                    case 1: // year
                        addCourseLables[i].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            yearPress(sender, e, tb);
                        });
                        break;
                    case 2: // name
                        addCourseLables[i].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            //charsPress(sender, e, tb);
                        }); 
                        break;
                    case 3: // lecturer
                        addCourseLables[i].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            //charsPress(sender, e, tb);
                        });
                        break;
                    case 4: // day
                        addCourseLables[i].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            dayPress(sender, e, tb);
                        });
                        break;
                    case 5: // end
                        addCourseLables[i].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            hourPress(sender, e, tb);
                            if(!getTouch && addCourseLables[5].Text.Length >= 3 && addCourseLables[6].Text.Length > 0 && !addCourseLables[6].Text.Equals(TITLES[(int)(addCourseLables[6].Tag)]))
                            {
                                getTouch = true;
                                if (addCourseLables[6].Text.Length > 2 && addCourseLables[5].Text.Length > 2)
                                { 
                                    int start_hours_num = Convert.ToInt32(addCourseLables[6].Text.Substring(0, 2));
                                    int end_hours_num = Convert.ToInt32(addCourseLables[5].Text.Substring(0, 2));
                                    if (end_hours_num - start_hours_num <= 0)
                                    {
                                        addCourseLables[5].Text = "";
                                    }
                                    else
                                    {
                                        addCourseLables[7].Text = (end_hours_num - start_hours_num).ToString();
                                        addCourseLables[7].ForeColor = Color.Black;
                                    }
                                }
                            }
                            else
                            {
                                getTouch = false;
                            }
                        });
                        break;
                    case 6: // start
                        addCourseLables[i].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            hourPress(sender, e, tb);
                            if (!getTouch && addCourseLables[6].Text.Length >= 3 && addCourseLables[5].Text.Length > 0 && !addCourseLables[5].Text.Equals(TITLES[(int)(addCourseLables[5].Tag)]))
                            {
                                getTouch = true;
                                if (addCourseLables[6].Text.Length > 2  && addCourseLables[5].Text.Length > 2)
                                {
                                    
                                    int start_hours_num = Convert.ToInt32(addCourseLables[6].Text.Substring(0, 2));
                                    int end_hours_num = Convert.ToInt32(addCourseLables[5].Text.Substring(0, 2));
                                    if (end_hours_num - start_hours_num <= 0)
                                    {
                                        addCourseLables[6].Text = "";
                                    }
                                    else
                                    {
                                        addCourseLables[7].Text = (end_hours_num - start_hours_num).ToString();
                                        addCourseLables[7].ForeColor = Color.Black;
                                    }
                                }
                            }
                            else
                            {
                                getTouch = false;
                            }
                        });
                        break;
                    case 7: // hours
                        addCourseLables[i].ReadOnly = true;
                        addCourseLables[i].Enabled = false;
                        break; 
                    case 8: // class
                        addCourseLables[i].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            //charsPress(sender, e, tb);
                        });
                        break;
                    case 9: // num lesson
                        addCourseLables[i].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            digitsPress(sender, e, tb, 2);
                        });
                        break;
                }

                // add the textbox to the Add Course Panel
                pnl_addCourse.Controls.Add(addCourseLables[i]);
            }
            // the add button for the new course
            btn_addNewCourse = new Button();
            btn_addNewCourse.Text = "הוסף";
            btn_addNewCourse.Height = lableMenuList[lableMenuList.Length - 1].Height;
            btn_addNewCourse.Width = lableMenuList[lableMenuList.Length - 1].Width;
            btn_addNewCourse.Location = new Point(lableMenuList[lableMenuList.Length - 1].Location.X, 2);
            // the click event - add the course to the list
            btn_addNewCourse.Click += new EventHandler((s, e) => {
                try
                {
                    Button btn = s as Button;
                    string courseName = "", type = "", lecturer = "", day = "", className = "";
                    int h_start = 0, h_end = 0, h = 0, year = 0, number = 0;
                    for (int m = 0; m < addCourseLables.Length; m++)
                    {
                        switch (m)
                        {
                            case 0: // type
                                type = addCourseLables[m].Text;
                                break;
                            case 1: // year
                                switch (addCourseLables[m].Text)
                                {
                                    case "א": year = 1;  break;
                                    case "ב": year = 2; break;
                                    case "ג": year = 3; break;
                                    case "ד": year = 4; break;
                                    default:
                                        break;
                                }
                                break;
                            case 2: // name
                                courseName = addCourseLables[m].Text;                               break;
                            case 3: // lecturer
                                lecturer =  addCourseLables[m].Text;                                break;
                            case 4: // day
                                day = addCourseLables[m].Text;                                      break;
                            case 5: // end
                                h_end = Convert.ToInt32(addCourseLables[m].Text.Substring(0, 2));   break;
                            case 6: // start
                                h_start = Convert.ToInt32(addCourseLables[m].Text.Substring(0, 2)); break;
                            case 7: // hours
                                h = h_end - h_start;                                                break;
                            case 8: // class
                                className = addCourseLables[m].Text;                                break;
                            case 9: // num lesson
                                number = Convert.ToInt32(addCourseLables[m].Text);                  break;
                        }
                    }
                    Lesson lsn = new Lesson(courseName, type, number, lecturer, day, h_start, h_end,h, year, className);
                    LessonList temp;
                    if (lessons != null) {
                        temp = new LessonList(lessons.getLessons());
                    }
                    else
                    {
                        temp = new LessonList();
                    }
                    temp = temp + lsn;
                    MessageBox.Show("השיעור לקורס התווסף בהצלחה", "הוסף", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.addCourseToPanel(lessons.amount());
                    this.setLessons(temp);
                    lessons = temp;
                       
                    for (int m = 0 ; m < addCourseLables.Length; m++)
                    {
                        addCourseLables[m].Text = "";
                        addCourseLables[m].Focus();
                    }
                    btn.Focus();
                }
                catch (Exception exp)
                {
                    DialogResult res = MessageBox.Show(exp.Message, "נתונים שגויים הוקלדו", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        // MessageBox.Show("לחצת על כפתור OK");
                        // Some task…  
                    }
                }
            });
            // add the button to the Add Course Panel
            pnl_addCourse.Controls.Add(btn_addNewCourse);
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

                if (btn_edit != null)
                    btn_edit.Visible = false;
                if (btn_save != null)
                    btn_save.Visible = false;
                if (btn_cancelEdit != null)
                    btn_cancelEdit.Visible = false;
            }
            else
            {
                lbl_checkedCourses.Visible = true;
                lbl_empty_info.Visible = false;
                lbl_empty_info.Invalidate();

                if (btn_edit != null) { 
                    btn_edit.Visible = true;
                }
            }

        }
        // Set selection on certain Lessons
        public void setSelectionByLessonsList(LessonList list)
        {
            if (list == null || list.amount() <= 0) return;
            for (int j = 0; j < lessons.amount() ; j++)
            {
                for (int i = 0; i < list.amount(); i++)
                {
                    if(list[i].sameValue(lessons[j]))
                    {
                        checkButtons[j].Checked = true;
                        lessonChecks.add(lessons[j]);
                        break;
                    }
                }
            }
            updateChecksCourse();
        }

        // Handler keypress event for the textbox in the add new course panel
        private void digitsPress(object sender, KeyPressEventArgs e,TextBox t, int amountDigits)
        {
            if(t.TextLength >= amountDigits)
            {
                if (!char.IsControl(e.KeyChar) && (e.KeyChar != '\b'))
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) ) //&& (e.KeyChar != '.'))
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
            else if(t.TextLength == 0)
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
                    case '1': e.KeyChar = 'א';  break;
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
                    t.Text = t.Text.Substring(0,2);
                    t.SelectionStart = 2;
                }
            }
            else
            {
                if(t.TextLength == 0 && e.KeyChar != '\b' && e.KeyChar != '0' && e.KeyChar != '1' && e.KeyChar != '2')
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

        // Init the buttons - Edit ,Save, Cancel
        private void init_buttons()
        {
            int X = 10, Y = 10;
            btn_edit = initButtonImage(btn_edit, new Bitmap(Schedule.Properties.Resources.Edit_Property, 54, 54) , new Point(X, Y) , 54);
            btn_cancelEdit = initButtonImage(btn_cancelEdit, new Bitmap(Schedule.Properties.Resources.Cancel_Subscription, 54, 54), new Point(2*X + btn_edit.Width, Y), 54);
            btn_save = initButtonImage(btn_save, new Bitmap(Schedule.Properties.Resources.Save_Close, 54, 54), new Point(X, Y), 54);
        }
        // init the properties of the button image
        private Button initButtonImage(Button b, Bitmap bit, Point loc, int size)
        {
            if (b == null) b = new Button();
            b.Image = bit;
            b.FlatAppearance.BorderSize = 0;
            b.Width = size + 2;
            b.Height = size + 2;
            b.FlatStyle = FlatStyle.Flat;
            b.BackgroundImageLayout = ImageLayout.Zoom;
            b.BackColor = Color.Transparent;
            b.Location = loc;
            pnl_Main.Controls.Add(b);
            b.Click += new EventHandler(btn_edit_Click);
            return b;
        }
        // ==================================================================================================
          
        // Add check button to the row in the list for the index lesson 
        private void addCheckButton(Bunifu.Framework.UI.BunifuCheckbox b, Label guide, int index)
        {
            if (lessonChecks.exist(lessons[index]))
                b.Checked = true;
            else
                b.Checked = false;

            b.Width = HEIGHT_ROW;
            b.Height = HEIGHT_ROW;

            b.Location = new Point(guide.Location.X + guide.Width / 2 - b.Width / 2, guide.Location.Y + ROW_SPACES + (index * HEIGHT_ROW + (index - 1) * ROW_SPACES) + HEIGHT_ROW / 2 - b.Height / 2);
            
            // On Change state event
            b.OnChange += (s, e) =>
            {
                if (b.Checked)
                {
                    lessonChecks.add(lessons[index]);
                }
                else
                {
                    lessonChecks.remove(lessons[index]);
                }
                updateChecksCourse();
            };
            // add the button to the panel
            pnl_courseList.Controls.Add(b);
        }
        // Add delete button to the row in the list for the index lesson 
        private void addDeleteButton(Button b, Label guide, int index)
        {
            int imageSize = 25;
            b.Image = new Bitmap(Schedule.Properties.Resources.minus, imageSize, imageSize);
            b.FlatAppearance.BorderSize = 0;
            b.Width = imageSize+2;
            b.Height = imageSize+2;
            b.FlatStyle = FlatStyle.Flat;
            b.BackgroundImageLayout = ImageLayout.Zoom;
            b.BackColor = Color.Transparent;
            b.Location = new Point(guide.Location.X + guide.Width / 2 - b.Width / 2,
               -2 + guide.Location.Y + ROW_SPACES + (index * HEIGHT_ROW + (index - 1) * ROW_SPACES));

            // Click event 
            b.Click += (s, e) =>
            {
                LessonList update = new LessonList(lessons.amount() - 1);
                lessonChecks.remove(lessons[index]);
                updateChecksCourse();

                for (int i = index; i < lessons.amount(); i++)
                {
                    removeCourseFromPanel(i);
                }

                for (int k = 0, i = 0; i < lessons.amount(); i++)
                {
                    if (i != index)
                    {
                        update[k++] = lessons[i];
                    }
                }
                lessons = update;

                for (int i = index; i < lessons.amount(); i++)
                {
                    addCourseToPanel(i);
                }
                checkToSetEmptyInfo();
            };
            // Mouse enter event to the specific titles -  set color
            b.MouseEnter += new EventHandler((sender, ev) =>
            {
                Button btn = sender as Button;
                if (!editState)
                {
                    string colorDelete = "#ff3f93";

                    Label l = labelsBackgroungEvenRow[index];
                    l.BackColor = (Color)System.Drawing.ColorTranslator.FromHtml(colorDelete);
                    l.Invalidate();

                    for (int j = 0; j < editLables[index].Length; j++)
                    {
                        editLables[index][j].BackColor = (Color)System.Drawing.ColorTranslator.FromHtml(colorDelete);
                        editLables[index][j].Invalidate();
                    }

                    btn.BackColor = (Color)System.Drawing.ColorTranslator.FromHtml(colorDelete);
                    btn.Invalidate();
                }
            });

            // Mouse enter event from the specific titles - return transperent color
            b.MouseLeave += new EventHandler((sender, ev) =>
            {
                Button btn = sender as Button;
                if (!editState)
                {
                    Label l = labelsBackgroungEvenRow[index];
                    if (index % 2 == 1) l.BackColor = BACK_COLOR_TEXT_FOR_EVEN_ROW;
                    else l.BackColor = BACK_COLOR_TEXT_FOR_ODD_ROW;
                    l.Invalidate();

                    for (int j = 0; j < editLables[index].Length; j++)
                    {
                        if (index % 2 == 1) editLables[index][j].BackColor = BACK_COLOR_TEXT_FOR_EVEN_ROW;
                        else editLables[index][j].BackColor = BACK_COLOR_TEXT_FOR_ODD_ROW;
                        editLables[index][j].Invalidate();
                    }

                    btn.BackColor = Color.Transparent;
                    btn.Invalidate();
                }
            });
            // add the button to the panel
            pnl_courseList.Controls.Add(b);
        }
        
        // remove a course row from panel
        private void removeCourseFromPanel(int index)
        {
            // remove all the textbox in the row index
            if (index < editLables.Length) { 
                for (int i = 0; i < editLables[index].Length; i++)
                {
                    pnl_courseList.Controls.Remove(editLables[index][i]);
                }
            }
            // remove the delete buttons
            if (index < deleteButtons.Length) { 
                pnl_courseList.Controls.Remove(deleteButtons[index]);
            }
            // remove the checkbox button
            if (index < checkButtons.Length) { 
                pnl_courseList.Controls.Remove(checkButtons[index]);
            }
            removeBackgroundRow();
        }
        // add a course row from panel
        private void addCourseToPanel(int i)
        {
            if(i >= lessons.amount())
            {
                int len = lessons.amount();
                TextBox[][] temp_editLables = new TextBox[len][];
                Bunifu.Framework.UI.BunifuCheckbox[] temp_checkButtons = new Bunifu.Framework.UI.BunifuCheckbox[len];
                Button[] temp_deleteButtons = new Button[len];

                for (int k = 0; k < lessons.amount() - 1; k++)
                {
                    temp_editLables[k] = editLables[k];
                    temp_checkButtons[k] = checkButtons[k];
                    temp_deleteButtons[k] = deleteButtons[k];
                }
                
                temp_editLables[len - 1] = new TextBox[lableMenuList.Length - 2];
                editLables = temp_editLables;

                temp_checkButtons[len - 1] = new Bunifu.Framework.UI.BunifuCheckbox();
                checkButtons = temp_checkButtons;

                temp_deleteButtons[len - 1] = new Button();
                deleteButtons = temp_deleteButtons;
                
                i--;
            }
            // init the textbox columns 
            for (int j = 0, k = 1; j < editLables[i].Length; j++, k++)
            {
                editLables[i][j] = new TextBox();
                editLables[i][j].Tag = j + 1;
                editLables[i][j].TextAlign = HorizontalAlignment.Left;

                string typeTitle = "ה/מ/ת";
                // when the check box get focus , set the text in black color and remove the placeholder text
                editLables[i][j].GotFocus += new EventHandler((sender, e) => {
                    // Remove Text
                    TextBox textBoxTitle = sender as TextBox;
                    if (editState)
                    {
                        if (String.IsNullOrWhiteSpace(textBoxTitle.Text) || textBoxTitle.Text.Equals(TITLES[(int)(textBoxTitle.Tag)]) || textBoxTitle.Text.Equals(typeTitle))
                        {
                            textBoxTitle.Text = "";
                        }
                        textBoxTitle.ForeColor = Color.Black;
                    }
                });
                // when the check box lost focus , set the text placeholder in Gray color and add the placeholder text if is empty
                editLables[i][j].LostFocus += new EventHandler((sender, e) => {
                    // Add PlaceHoder Text
                    TextBox textBoxTitle = sender as TextBox;
                    if (editState)
                    {
                        if (String.IsNullOrWhiteSpace(textBoxTitle.Text))
                        {
                            if ((int)(textBoxTitle.Tag) == 3) textBoxTitle.Text = typeTitle + " - " +TITLES[(int)(textBoxTitle.Tag)];
                            else textBoxTitle.Text = TITLES[(int)(textBoxTitle.Tag)];
                            textBoxTitle.ForeColor = Color.Gray;
                        }
                    }
                    
                });
                editLables[i][j].ScrollBars = ScrollBars.Horizontal;
                if (i == 0) editLables[i][j].Text = typeTitle;
                else editLables[i][j].Text = TITLES[k];

                bool getTouch = false;

                switch (j)
                {
                    case 0: // 0. number row
                        editLables[i][j].Text = (1 + i).ToString();
                        editLables[i][j].ReadOnly = true;
                        editLables[i][j].Enabled = false;
                        break;
                    case 1: // 1. year
                        switch (lessons[i].year)
                        {  // 1. year
                            case 1: editLables[i][j].Text = "א"; break;
                            case 2: editLables[i][j].Text = "ב"; break;
                            case 3: editLables[i][j].Text = "ג"; break;
                            case 4: editLables[i][j].Text = "ד"; break;
                        }
                        editLables[i][j].TextAlign = HorizontalAlignment.Center;
                        editLables[i][j].KeyPress += ((sender, e) =>
                        {
                            TextBox tb = sender as TextBox;
                            yearPress(sender, e, tb);
                        });
                        break;
                    case 2: // 2. course name (+ type lesson)
                        editLables[i][j].Text = lessons[i].type + " - " + lessons[i].courseName.ToString();

                        editLables[i][j].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            nameCourseInListPress(sender, e, tb);
                        });
                        break;
                    case 3: // 3. lecturer
                        editLables[i][j].Text = lessons[i].lecturer.ToString();
                        editLables[i][j].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            //charsPress(sender, e, tb);
                        });
                        break;
                    case 4: // 4. day in one char
                        editLables[i][j].Text = lessons[i].getShortDay().ToString();
                        editLables[i][j].TextAlign = HorizontalAlignment.Center;
                        editLables[i][j].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            dayPress(sender, e, tb);
                        });
                        break;
                    case 5: // 5. the end time
                        editLables[i][j].Text = lessons[i].getEndHour();
                        editLables[i][j].TextAlign = HorizontalAlignment.Center;
                        editLables[i][j].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            hourPress(sender, e, tb);
                            TextBox t_end = editLables[i][5];
                            TextBox t_start = editLables[i][6];
                            TextBox t_hours = editLables[i][7];
                            if (!getTouch && t_end.Text.Length >= 3 && t_start.Text.Length > 0 && !t_start.Text.Equals(TITLES[(int)(t_start.Tag)]))
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
                        break;
                    case 6: // 6. the start time
                        editLables[i][j].Text = lessons[i].getStartHour();
                        editLables[i][j].TextAlign = HorizontalAlignment.Center;
                        editLables[i][j].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            hourPress(sender, e, tb);
                            TextBox t_end = editLables[i][5];
                            TextBox t_start = editLables[i][6];
                            TextBox t_hours = editLables[i][7];

                            if (!getTouch && t_start.Text.Length >= 3 && t_end.Text.Length > 0 && !t_end.Text.Equals(TITLES[(int)(t_end.Tag)]))
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
                        break;
                    case 7: // 7. the amount hours lesson
                        editLables[i][j].Text = lessons[i].weekHour.ToString();
                        editLables[i][j].TextAlign = HorizontalAlignment.Center;
                        editLables[i][j].ReadOnly = true;
                        editLables[i][j].Enabled = false;
                        break;
                    case 8: // 8. the place name of the class
                        editLables[i][j].Text = lessons[i].className.ToString();
                        editLables[i][j].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            
                        });
                        break;
                    case 9: // 9. the number of the lesson
                        editLables[i][j].Text = lessons[i].number.ToString();
                        editLables[i][j].TextAlign = HorizontalAlignment.Center;
                        editLables[i][j].KeyPress += ((sender, e) => {
                            TextBox tb = sender as TextBox;
                            digitsPress(sender, e, tb, 2);
                        });
                        break;

                }

                editLables[i][j] = initColCourse(editLables[i][j], lableMenuList[j + 1], i);
                pnl_courseList.Controls.Add(editLables[i][j]);
            }

            
            addBackgroundRow(i);

            // update visible of the vertical scroolbar panel list and title
            ScrollVisiblePanels();
        }

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
        // Remove the background row and his unter seperade line 
        private void removeBackgroundRow()
        {
            pnl_courseList.Controls.Remove(labelsBackgroungEvenRow[labelsBackgroungEvenRow.Length - 1]);
            pnl_courseList.Controls.Remove(hrs[hrs.Length - 1]);

            // update the array with the less row item
            labelsBackgroungEvenRow = removeItemLable(labelsBackgroungEvenRow);
            hrs = removeItemLable(hrs);
            
            // update visible of the vertical scroolbar panel list and title
            ScrollVisiblePanels();
        }
        // Add the background row and his unter seperade line 
        private void addBackgroundRow(int index)
        {
            // -- Add hr line under the row course ------
            Label hr = new Label();
            hr.BorderStyle = BorderStyle.Fixed3D;
            hr.Height = 2;
            hr.Width = (lableMenuList[0].Location.X + lableMenuList[0].Width) - lableMenuList[lableMenuList.Length - 1].Location.X;
            Label guide = lableMenuList[lableMenuList.Length - 1];
            hr.Location = new Point(guide.Location.X ,
                                    guide.Location.Y + HEIGHT_ROW + index * ( HEIGHT_ROW + ROW_SPACES) + ROW_SPACES / 2);
            pnl_courseList.Controls.Add(hr);

            hrs = addItemLable(hrs, hr);
            // ------------------------------------------

            // Add check button from rigth side 
            checkButtons[index] = new Bunifu.Framework.UI.BunifuCheckbox();
            addCheckButton(checkButtons[index], lableMenuList[0], index);

            // Add delete button from rigth side 
            deleteButtons[index] = new Button();
            addDeleteButton(deleteButtons[index], lableMenuList[lableMenuList.Length - 1], index);

            // Add background for row 
            labelsBackgroungEvenRow = addItemLable(labelsBackgroungEvenRow, new Label());
            pnl_courseList.Controls.Add(setBackgroundForRow(index));

            // update visible of the vertical scroolbar panel list and title
            ScrollVisiblePanels();
        }
        // Set the properties of the background row
        private Label setBackgroundForRow(int i , string color = "")
        {
            Label lbl_rightest = lableMenuList[0];
            Label lbl_leftest = lableMenuList[lableMenuList.Length - 1];
            Label l = labelsBackgroungEvenRow[i];

            l.RightToLeft = RightToLeft.Yes;
            l.BackColor = BACK_COLOR_TEXT_FOR_EVEN_ROW;
            l.AutoSize = false;
            l.Height = HEIGHT_ROW;
            l.Width = lbl_rightest.Location.X + lbl_rightest.Width - lbl_leftest.Location.X;
            l.SendToBack();
            l.Location = calcLocationItem(lbl_leftest, i);

            if (!color.Equals(""))
            {
                l.BackColor = (Color)System.Drawing.ColorTranslator.FromHtml(color);
            }
            else
            {
                if (i % 2 == 1)
                    l.BackColor = BACK_COLOR_TEXT_FOR_EVEN_ROW;
                else
                    l.BackColor = BACK_COLOR_TEXT_FOR_ODD_ROW;
            }
            l.Invalidate();
            return l;
        }
        
        // init the properties of the columns in the row
        private TextBox initColCourse(TextBox t, Label guide, int index)
        {
            // properties of TextBox
            if (t == null) t = new TextBox();
            t.Multiline = true;
            t.WordWrap = true;
            t.BorderStyle = BorderStyle.None; 

            t.AutoSize = false;
            t.Height = HEIGHT_ROW;
            t.Width = guide.Width;

            t.RightToLeft = RightToLeft.Yes;
            t.Font = new Font(FONT_TEXT, SIZE_FONT_TEXT, FontStyle.Bold);
            t.ReadOnly = true;
            t.Visible = true;

            if (index % 2 == 1) t.BackColor = BACK_COLOR_TEXT_FOR_EVEN_ROW;
            else t.BackColor = BACK_COLOR_TEXT_FOR_ODD_ROW;
            t.ForeColor = COLOR_TEXT;

            t.Location = calcLocationItem(guide, index);

            int MaxChars = t.Width / 10; //suppose that's the maximum
            if (t.Text.Count() > MaxChars)  t.ScrollBars = ScrollBars.Vertical;
            else                            t.ScrollBars = ScrollBars.None;

            t.TextChanged += ((s, e) => {
                TextBox tb = s as TextBox;
                if (tb.Text.Count() > MaxChars)
                    tb.ScrollBars = ScrollBars.Vertical;
                else
                    tb.ScrollBars = ScrollBars.None;
                tb.Invalidate();
            });

            t.Invalidate();
            return t;
        }
        // ================================================================================================

        // == GENERIC HELP METHOD =========================================================================
        // Remove Item from the labels array , default is the last item
        private Label[] removeItemLable(Label[] arr, int index = -1)
        {
            if (arr == null)
                return arr;

            if (index < 0 || index > arr.Length - 1)
                index = arr.Length - 1;

            Label[] temp = new Label[arr.Length - 1];
            for (int i = 0, k = 0; i < arr.Length; i++)
                if (i != index)
                    temp[k++] = arr[i];

            return temp;
        }
        // Add a new item (Label) to the labels array
        private Label[] addItemLable(Label[] arr, Label l)
        {
            if (arr == null)
            {
                arr = new Label[1];
                arr[0] = l;
                return arr;
            }

            Label[] temp = new Label[arr.Length + 1];
            for (int i = 0; i < arr.Length; i++)
                temp[i] = arr[i];
            temp[temp.Length - 1] = l;
            return temp;
        }
        // Update the text of the checked course
        private void updateChecksCourse()
        {
            lbl_checkedCourses.Text = "נבחרו: " + lessonChecks.amount() + " שיעורים.";
        }
        // return the location of the item , depend of the index 
        private Point calcLocationItem(Label guide, int index)
        {
            return new Point(guide.Location.X, guide.Location.Y  + ROW_SPACES + (index * HEIGHT_ROW + (index - 1) * ROW_SPACES));
        }
        // The Mouse Click method for the titles columns
        private void MouseClickTitleLabel(Label l, string sortType = "")
        {
            int index = findLableInTitleList(l);
            if (index >= 0)
            {
                sortOpposite[index] = !sortOpposite[index];

                if (index == 0)
                {
                    for (int i = 0; i < checkButtons.Length; i++)
                    {
                        if (sortOpposite[index])
                        {
                            if (checkButtons[i].Checked == false)
                            {
                                checkButtons[i].Checked = true;
                                lessonChecks.add(lessons[i]);
                            }
                        }
                        else
                        {
                            if (checkButtons[i].Checked == true)
                            {
                                checkButtons[i].Checked = false;
                                lessonChecks.remove(lessons[i]);
                            }
                        }
                    }
                    updateChecksCourse();
                }
                else
                {
                    for (int i = 0; i < lessons.amount(); i++)
                    {
                        removeCourseFromPanel(i);
                    }
                    lessons.sortBy(sortType, sortOpposite[index]);
                    for (int i = 0; i < lessons.amount(); i++)
                    {
                        addCourseToPanel(i);
                    }
                }
            }
        }
        // Return the index of the label from the lableMenuList
        private int findLableInTitleList(Label l)
        {
            for (int i = 0; i < lableMenuList.Length; i++)
            {
                if (lableMenuList[i] == l)
                {
                    return i;
                }
            }
            return -1;
        }
        // the Mouse Enter method for set background to title
        private void mouseEnterTitleLabe(Label l, string colorElse = "")
        {
            if (colorElse.Equals(""))
                l.BackColor = COLOR_TITLE_HOVER_BACK;
            else
            {
                l.BackColor = (Color)System.Drawing.ColorTranslator.FromHtml(colorElse);
            }
            l.ForeColor = COLOR_TITLE_HOVER_FORE;
        }
        // the Mouse Leave method for return background to title
        private void mouseLeaveTitleLabe(Label l)
        {
            l.BackColor = Color.Transparent;
            l.ForeColor = COLOR_TEXT_TITLE;
        }
        // ================================================================================================

        bool editState = false;
        // == EDIT , ZOOM-IN , ZOOM-OUT ===================================================================
        // The Click Event for the Save, Edit, Cancel buttons
        private void btn_edit_Click(object sender, EventArgs e)
        {
            Button btn_clicked = sender as Button;
            changeEditState(btn_clicked);
        }
        // The edit state changes - enable edit and visible buttons
        private void changeEditState(Button buttonClick)
        {
            // save the last position of the vertical scrool state 
            int v   = pnl_courseList.VerticalScroll.Value;
            Point p = pnl_courseList.AutoScrollPosition;
            
            // reset the location of the position of vertical scrool - to change state
            pnl_courseList.VerticalScroll.Value = 0;
            pnl_courseList.AutoScrollPosition = new Point(0, 0);

            // change the state of the edit state
            editState = !editState;

            // move on all the rows
            for (int i = 0; i < editLables.Length; i++)
            {
                // move on all the columns
                for (int j = 0; j < editLables[i].Length; j++)
                {
                    if(buttonClick == btn_edit)
                    {
                        editLables[i][j].ReadOnly = false;
                        editLables[i][j].BackColor = Color.White;
                        editLables[i][j].ForeColor = Color.Black;
                        editLables[i][j].Invalidate();
                    }
                    else if(buttonClick == btn_save || buttonClick == btn_cancelEdit)
                    {
                        editLables[i][j].ReadOnly = true;
                        if (i % 2 == 1) editLables[i][j].BackColor = BACK_COLOR_TEXT_FOR_EVEN_ROW;
                        else editLables[i][j].BackColor = BACK_COLOR_TEXT_FOR_ODD_ROW;
                        editLables[i][j].ForeColor = COLOR_TEXT;
                        editLables[i][j].Invalidate();
                        if(buttonClick == btn_save)
                        {
                            // need to paese, and change all the lesson that checked in checkedListCourse
                        }
                    }

                    btn_save.Visible = editState;
                    btn_cancelEdit.Visible = btn_save.Visible;
                    btn_edit.Visible = !btn_save.Visible;

                    // Change the Visible of the Delete and CheckBox buttons
                    visibleButtonsDeleteAndChecked(editState);
                }
            }
            
            // return the last position that we had , before the updates here
            pnl_courseList.VerticalScroll.Value = v;
            pnl_courseList.AutoScrollPosition = p;
        }
        // Change the Visible of the Delete and CheckBox buttons
        private void visibleButtonsDeleteAndChecked(bool visible)
        {
            for (int i = 0; i < deleteButtons.Length; i++)
            {
                deleteButtons[i].Visible = !visible;
                checkButtons[i].Visible = !visible;
            }
        }
        // ZoomIn text of the course list 
        private void btn_zoomIn_Click(object sender, EventArgs e)
        {
            if (SIZE_FONT_TEXT + 2 < MAX_SIZE_FONT_TEXT)
            {
                SIZE_FONT_TEXT = SIZE_FONT_TEXT + 2;
                for (int i = 0; i < editLables.Length; i++)
                {
                    for (int j = 0; j < editLables[i].Length; j++)
                    {
                        editLables[i][j].Font = new Font(FONT_TEXT, SIZE_FONT_TEXT, FontStyle.Bold);
                        editLables[i][j].Invalidate();

                        editLables[i][j].Font = new Font(FONT_TEXT, SIZE_FONT_TEXT, FontStyle.Bold);
                        editLables[i][j].Invalidate();
                    }
                }
            }
        }
        // ZoomOut text of the course list 
        private void btn_zoomOut_Click(object sender, EventArgs e)
        {
            if (SIZE_FONT_TEXT - 2 > MIN_SIZE_FONT_TEXT)
            {
                SIZE_FONT_TEXT = SIZE_FONT_TEXT - 2;
                for (int i = 0; i < editLables.Length; i++)
                {
                    for (int j = 0; j < editLables[i].Length; j++)
                    {
                        editLables[i][j].Font = new Font(FONT_TEXT, SIZE_FONT_TEXT, FontStyle.Bold);
                        editLables[i][j].Invalidate();

                        editLables[i][j].Font = new Font(FONT_TEXT, SIZE_FONT_TEXT, FontStyle.Bold);
                        editLables[i][j].Invalidate();
                    }
                }
            }
        }
        // -----------------------------------------------------------------------------
    }
}
