using Schedule.Excel;
using Schedule.CoursesList;
using System;
using System.Drawing;
using System.Windows.Forms;
using Schedule.Lessons;
using Schedule.ReadWriteFiles;
using Schedule.SaveAndLoad;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace Schedule
{
    public partial class Form1 : Form 
    {
        PanelCoursesList panel_CourseList_cards;        // Page - panel of the list of courses in cards to choose 
        PanelCoursesListBackup panel_CourseList_array;  // Page - panel of the list of lessons in ListArray to choose 
        FormImport form_Import;                         // Page - panel of the  Import From Excel Page
        PanelSavements panel_savements;                 // Page - panel of the savements as Panel
        PanelSchedule panel_Scheduler;                  // Page - panel of the weekly schedule lessons 

        Panel pnl_coursList; 
        Panel pnl_schedule;
        Panel pnl_savement;

        Bitmap iconExitRed;
        Bitmap iconExitWhite;

        public LessonList importCourses { get; set; }         // init this list from import and pass it to courses list
        public bool  ImportNewDataFromFile { get; set; }// let know if import some data from the page import
        public bool newCheckingCoursesDone { get; set; }// let know if import some data from the page import
        public LessonList checkedCourses { get; set; }  // init this list from course list page and pass it to schedule page
        public List<Lesson> checkedLessonsFromCourses { get; set; }
        public Data dataProgram { get; set; }                              // The data to save and load

        public Form1()
        {
            InitializeComponent();
            this.RightToLeftLayout = true;

            checkedLessonsFromCourses = new List<Lesson>();

            PanelCoursesList.WIDTH_WINDOWS = panelMain.Width; 
            PanelCoursesList.HEIGHT_WINDOWS = panelMain.Height;
            form_Import = new FormImport(this);
            lbl_NameUser.SendToBack();

            Data dataRef = new Data();
            dataProgram = dataRef.LoadData();
            updateDateFromSavement();

            // setup at first home page schedule
            enablePanel(btn_home);
            panel_Scheduler         = new PanelSchedule(this);
            panel_CourseList_cards  = new PanelCoursesList(this, importCourses);
            panel_CourseList_array = new PanelCoursesListBackup(importCourses);
            panel_savements = new PanelSavements(this);

            updateDateFromSavement();
            
            if (newCheckingCoursesDone)
            {
                if (checkedCourses == null) checkedCourses = new LessonList();
                panel_Scheduler.setNewList(checkedCourses);
                panel_Scheduler.ClickRightsRadioButtonsByLessonsSelected(checkedLessonsFromCourses);
                newCheckingCoursesDone = false;
            }
            

            pnl_schedule = panel_Scheduler.getPanel();
            panelMain.Controls.Add(pnl_schedule);

            Bunifu.Framework.UI.BunifuFlatButton ext = btn_exit_out;
            iconExitRed = new Bitmap(Schedule.Properties.Resources.ShutdownRed, btn_exit_out.Iconimage.Size.Width, btn_exit_out.Iconimage.Size.Height);
            iconExitWhite = new Bitmap(Schedule.Properties.Resources.shutdown, btn_exit_out.Iconimage.Size.Width, btn_exit_out.Iconimage.Size.Height);
            ImportNewDataFromFile = false;
            newCheckingCoursesDone = false;
        }
             
        private void btn_menu_Click(object sender, EventArgs e)
        {
            if (slidemenu.Width == 50)
            {
                slidemenu.Visible = false;
                slidemenu.Width = 300;
                PanelAnimator.ShowSync(slidemenu);
                LogoAnimator.ShowSync(logo);
            }
            else
            {
                LogoAnimator.Hide(logo);
                slidemenu.Visible = false;
                slidemenu.Width = 50;
                PanelAnimator.ShowSync(slidemenu);
            }
        }
       
        private void btn_home_Click(object sender, EventArgs e)
        {
            // clear screen to preper this page
            enablePanel(btn_home);

            // By add Panel to Panel
            pnl_schedule = panel_Scheduler.getPanel();
            if (newCheckingCoursesDone) { 
                panel_Scheduler.setNewList(checkedCourses);
                newCheckingCoursesDone = false;
            }
            panelMain.Controls.Add(pnl_schedule);
        }
        private void btn_courses_Click(object sender, EventArgs e)
        {
            enablePanel(btn_courses);

            // By add Panel to Panel
            if (ImportNewDataFromFile) { 
                panel_CourseList_cards.setNewList(importCourses);
                panel_CourseList_array.setNewList(importCourses);
                ImportNewDataFromFile = false;
            }
            panel_CourseList_cards.setSelectionByLessonsList(importCourses);
            pnl_coursList = panel_CourseList_cards.getPanel();
            panelMain.Controls.Add(pnl_coursList);
        }
        private void btn_listCourses2_Click(object sender, EventArgs e)
        {
            enablePanel(btn_listCourses2);

            // By add Panel to Panel
            if (ImportNewDataFromFile)
            {
                panel_CourseList_cards.setNewList(importCourses);
                panel_CourseList_array.setNewList(importCourses);
                ImportNewDataFromFile = false;
            }
            panel_CourseList_array.setSelectionByLessonsList(importCourses);
            pnl_coursList = panel_CourseList_array.getPanel();
            panelMain.Controls.Add(pnl_coursList);
        }
        private void btn_import_Click(object sender, EventArgs e)
        {
            enablePanel(btn_import);
            
            // By add Form to Panel 
            form_Import.TopLevel = false; // important!
            panelMain.Controls.Add(form_Import);
            form_Import.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form_Import.Dock = DockStyle.Fill;
            form_Import.Show();
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            enablePanel(btn_save);
            // By add Panel to Panel
            dataProgram.checkedCourses = checkedCourses.getList();
            dataProgram.checkedLessonsFromCourses = checkedLessonsFromCourses;
            dataProgram.importCourses = importCourses.getList();

            panel_savements.dataSavement = dataProgram;
            pnl_savement = panel_savements.panel;
            panelMain.Controls.Add(pnl_savement);
        }
        private void btn_export_Click(object sender, EventArgs e)
        {
            enablePanel(btn_export);
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            saveData();
            this.Close();
        }
        private void btn_setting_Click(object sender, EventArgs e)
        {
            enablePanel(btn_setting);
        }
        private void link_icon8_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://icons8.com/");
        }

        //-----------------------------------------------------------------------------------------------------------

        private void btn_exit_out_Click(object sender, EventArgs e)
        {
            saveData();
            this.Close();
        }
        private void btn_exit_MouseLeave(object sender, EventArgs e)
        {
            btn_exit.BackColor = Color.Transparent;
        }
        private void btn_exit_MouseEnter(object sender, EventArgs e)
        {
            btn_exit.BackColor = Color.Red;
        }

        private void btn_minimize_MouseEnter(object sender, EventArgs e)
        {
            btn_minimize.BackColor = Color.DarkSlateBlue;
        }
        private void btn_minimize_MouseLeave(object sender, EventArgs e)
        {
            btn_minimize.BackColor = Color.Transparent;
        }
        private void btn_minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void enablePanel(Bunifu.Framework.UI.BunifuFlatButton btn = null)
        {
            panelMain.Controls.Remove(pnl_schedule);
            panelMain.Controls.Remove(pnl_coursList);
            panelMain.Controls.Remove(form_Import);
            panelMain.Controls.Remove(pnl_savement);

            if (btn == btn_home)
            {
                lbl_title_selected.Text = "דף ראשי";
            }
            else if (btn == btn_courses)
            {
                lbl_title_selected.Text = "רשימת קורסים";
            }
            else if (btn == btn_import)
            {
                lbl_title_selected.Text = "ייבוא";
            }
            else if (btn == btn_save)
            {
                lbl_title_selected.Text = "שמירה / טעינה";
            }
            else if (btn == btn_setting)
            {
                lbl_title_selected.Text = "הגדרות";
            }
            else if (btn == btn_export)
            {
                lbl_title_selected.Text = "ייצוא";
            }
            else if (btn == btn_listCourses2)
            {
                lbl_title_selected.Text = "רשימת שיעורים";
            }
            
        }

        private void btn_exit_out_MouseEnter(object sender, EventArgs e)
        {
            btn_exit_out.Iconimage = iconExitRed;
            btn_exit_out.ForeColor = Color.Red;
        }
        private void btn_exit_out_MouseHover(object sender, EventArgs e)
        {
            btn_exit_out_MouseEnter(sender, e);
        }
        private void btn_exit_out_MouseLeave(object sender, EventArgs e)
        {
            btn_exit_out.Iconimage = iconExitWhite;
            btn_exit_out.ForeColor = Color.Silver;
        }

        private void saveData()
        {
            dataProgram.checkedCourses = checkedCourses.getList();
            dataProgram.checkedLessonsFromCourses = checkedLessonsFromCourses;
            dataProgram.importCourses = importCourses.getList();

            dataProgram.SaveData();
            //RW_Savements.WriteJSON(panel_savements.saves);
        }

        public void updateDateFromSavement()
        {
            checkedCourses = new LessonList(dataProgram.checkedCourses.ToArray());
            newCheckingCoursesDone = true;
            checkedLessonsFromCourses = dataProgram.checkedLessonsFromCourses != null ? dataProgram.checkedLessonsFromCourses: new List<Lesson>();
            newCheckingCoursesDone = true;
            importCourses = new LessonList(dataProgram.importCourses.ToArray());
        }
    }
}
