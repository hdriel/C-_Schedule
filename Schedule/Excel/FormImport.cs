using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

using Excel = Microsoft.Office.Interop.Excel;       //Microsoft Excel 14 object in references-> COM tab
using System.Runtime.InteropServices;
using OfficeOpenXml;
using Schedule.Excel;
using Schedule.Lessons;

namespace Schedule
{
    public partial class FormImport : Form
    {
        string fileName;
        DataSet dataSetExcel;
        DataTable dtSelected;
        string[] sheets;
        Form1 mainForm;
        LessonList lessons;

        public FormImport(Form1 mf)
        {
            InitializeComponent();
            mainForm = mf;
            lessons = mainForm.importCourses;
            if (lessons != null)
            {
                btn_cleanExcelData.Visible = true;
            }
            updateStateComponentsImport();
        }

        

        private void updateStateComponentsImport()
        {
            if (dataSetExcel == null)
            {
                dataGridView.Visible = false;
                btn_cleanExcelData.Visible = false;
                btn_approve.Visible = false;
                CB_sheets.Visible = false;
                lbl_coursesFounded.Visible = false;
            }
            else
            {
                dataGridView.Visible = true;
                btn_cleanExcelData.Visible = true;
                btn_approve.Visible = true;
                CB_sheets.Visible = true;
                lbl_coursesFounded.Visible = true;
                lbl_coursesFounded.BringToFront();
            }
            moveExcelBtn();
        }

        private void CB_sheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView.DataSource = dataSetExcel.Tables[CB_sheets.SelectedIndex];
                updateStateComponentsImport();
            }
            catch
            {

            }
        }

        private void selectedRowToDatatable()
        {
            dtSelected = new DataTable();
            foreach (DataGridViewColumn column in dataGridView.Columns)
                dtSelected.Columns.Add(column.Name, column.CellType); //better to have cell type
            for (int i = 0; i < dataGridView.SelectedRows.Count; i++)
            {
                dtSelected.Rows.Add();
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    dtSelected.Rows[i][j] = dataGridView.SelectedRows[i].Cells[j].Value;
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        // ---------------------------------------------------------------------------------------------------

        private void btn_inport_excel_Click(object sender, EventArgs e)
        {
            try
            {
                fileName = ExcelOperation.getExcelFileFromUser();
                dataSetExcel = ExcelOperation.ImportExcelXLS(fileName);
                sheets = ExcelOperation.GetExcelSheetNames();

                CB_sheets.DataSource = sheets;
                CB_sheets.Invalidate();
                dataGridView.DataSource = dataSetExcel.Tables[0];
                dataGridView.Invalidate();
                updateStateComponentsImport();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }
        }

        private void moveExcelBtn()
        {
            if (dataSetExcel == null)
            {
                btn_inport_excel.Visible = true;
                btn_inport_excel.Width = 190;
                btn_inport_excel.Height = 190;
                btn_inport_excel.BackColor = Color.Transparent;
                btn_inport_excel.Location = new Point(panelExcel.Width / 2 - (btn_inport_excel.Width / 2), panelExcel.Height / 2 - btn_inport_excel.Height / 2);

                //btnExcel_animator.ShowSync(btn_inport_excel);
                btn_inport_excel.Invalidate();
            }
            else
            {
                btn_inport_excel.Visible = true;
                btn_inport_excel.Width = 39;
                btn_inport_excel.Height = 36;
                btn_inport_excel.BackColor = Color.SeaGreen;
                btn_inport_excel.Location = new Point(8, 8);

                //btnExcel_animator.ShowSync(btn_inport_excel);
                btn_inport_excel.Invalidate();
            }
        }

        private void btn_approve_Click(object sender, EventArgs e)
        {
            if(dataSetExcel.Tables[CB_sheets.SelectedIndex] != null) {
                if(dataGridView.CurrentCell != null)
                {
                    selectedRowToDatatable();
                    if (dtSelected != null)
                    {
                        lessons = ExcelOperation.parserExcelFile(dtSelected);
                        dtSelected = null;
                    }
                    else
                    {
                        lessons = ExcelOperation.parserExcelFile(dataSetExcel.Tables[CB_sheets.SelectedIndex]);
                        dtSelected = null;
                    }
                }
                else
                {
                    lessons = ExcelOperation.parserExcelFile(dataSetExcel.Tables[CB_sheets.SelectedIndex]);
                    dtSelected = null;
                }

                if(lessons == null || lessons.amount() == 0)
                {
                    string title = "לא נמצאו תוצאות";
                    string massage = "לא נמצאו תוצאות לפי בחירת תאים , שם לב שאת/ה מסמן גם כן את הכותרות של התאים (למשל כמו , שם קורס, מרצה וכדומה) במידה וסימנת ולא נמצא כלום, האם תרצה לבצע חיפוש של נתונים לפי הדף כולו?";
                    DialogResult res = MessageBox.Show(massage, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (res == DialogResult.OK)
                    {
                        lessons = ExcelOperation.parserExcelFile(dataSetExcel.Tables[CB_sheets.SelectedIndex]);
                        dtSelected = null;
                    }
                   // else if(res == DialogResult.Cancel)
                   // {

                   // }
                }
                if (lessons.amount() > 0) { 
                    lbl_coursesFounded.Text = "נמצאו " + lessons.amount() + " רשומות!";
                    lbl_coursesFounded.ForeColor = Color.White;
                }
                else { 
                    lbl_coursesFounded.Text = "לא נמצאו רשומות";
                    lbl_coursesFounded.ForeColor = Color.Red;
                }

                mainForm.dataProgram.importCourses = lessons.getLessons().ToList();
                mainForm.importCourses = lessons;
                mainForm.ImportNewDataFromFile = true;
            }
        }

        private void btn_cleanExcelData_Click(object sender, EventArgs e)
        {
            fileName = "";
            dataSetExcel = null;
            sheets = null;
            CB_sheets.DataSource = sheets;
            dataGridView.DataSource = null;
            updateStateComponentsImport();
        }
        
        private void btn_inport_excel_MouseEnter(object sender, EventArgs e)
        {
            btn_inport_excel.BackColor = Color.SeaGreen;
        }

        private void btn_inport_excel_MouseLeave(object sender, EventArgs e)
        {
            if(btn_inport_excel.Width == 190)
            {
                btn_inport_excel.BackColor = Color.Transparent;
            }
        }
    }
}
