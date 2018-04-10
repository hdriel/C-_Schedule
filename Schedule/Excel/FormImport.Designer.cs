namespace Schedule
{
    partial class FormImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            BunifuAnimatorNS.Animation animation3 = new BunifuAnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImport));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panelExcel = new System.Windows.Forms.Panel();
            this.lbl_coursesFounded = new System.Windows.Forms.Label();
            this.CB_sheets = new System.Windows.Forms.ComboBox();
            this.btn_approve = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_cleanExcelData = new Bunifu.Framework.UI.BunifuFlatButton();
            this.dataGridView = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.btn_inport_excel = new Bunifu.Framework.UI.BunifuImageButton();
            this.panelImport = new System.Windows.Forms.Panel();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.btnExcel_animator = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.panelExcel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_inport_excel)).BeginInit();
            this.panelImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // panelExcel
            // 
            this.panelExcel.Controls.Add(this.lbl_coursesFounded);
            this.panelExcel.Controls.Add(this.CB_sheets);
            this.panelExcel.Controls.Add(this.btn_approve);
            this.panelExcel.Controls.Add(this.btn_cleanExcelData);
            this.panelExcel.Controls.Add(this.dataGridView);
            this.panelExcel.Controls.Add(this.btn_inport_excel);
            this.btnExcel_animator.SetDecoration(this.panelExcel, BunifuAnimatorNS.DecorationType.None);
            this.panelExcel.Location = new System.Drawing.Point(3, -9);
            this.panelExcel.Name = "panelExcel";
            this.panelExcel.Size = new System.Drawing.Size(1040, 746);
            this.panelExcel.TabIndex = 1;
            // 
            // lbl_coursesFounded
            // 
            this.btnExcel_animator.SetDecoration(this.lbl_coursesFounded, BunifuAnimatorNS.DecorationType.None);
            this.lbl_coursesFounded.Font = new System.Drawing.Font("Guttman Hatzvi", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl_coursesFounded.ForeColor = System.Drawing.Color.White;
            this.lbl_coursesFounded.Location = new System.Drawing.Point(574, 688);
            this.lbl_coursesFounded.Name = "lbl_coursesFounded";
            this.lbl_coursesFounded.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_coursesFounded.Size = new System.Drawing.Size(450, 46);
            this.lbl_coursesFounded.TabIndex = 21;
            this.lbl_coursesFounded.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CB_sheets
            // 
            this.btnExcel_animator.SetDecoration(this.CB_sheets, BunifuAnimatorNS.DecorationType.None);
            this.CB_sheets.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.CB_sheets.FormattingEnabled = true;
            this.CB_sheets.ItemHeight = 24;
            this.CB_sheets.Location = new System.Drawing.Point(258, 12);
            this.CB_sheets.Name = "CB_sheets";
            this.CB_sheets.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CB_sheets.Size = new System.Drawing.Size(312, 32);
            this.CB_sheets.TabIndex = 20;
            this.CB_sheets.Text = "בחר גיליון";
            this.CB_sheets.SelectedIndexChanged += new System.EventHandler(this.CB_sheets_SelectedIndexChanged);
            // 
            // btn_approve
            // 
            this.btn_approve.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_approve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_approve.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_approve.BorderRadius = 0;
            this.btn_approve.ButtonText = "אשר גיליון";
            this.btn_approve.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel_animator.SetDecoration(this.btn_approve, BunifuAnimatorNS.DecorationType.None);
            this.btn_approve.DisabledColor = System.Drawing.Color.Gray;
            this.btn_approve.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_approve.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_approve.Iconimage")));
            this.btn_approve.Iconimage_right = null;
            this.btn_approve.Iconimage_right_Selected = null;
            this.btn_approve.Iconimage_Selected = null;
            this.btn_approve.IconMarginLeft = 0;
            this.btn_approve.IconMarginRight = 0;
            this.btn_approve.IconRightVisible = true;
            this.btn_approve.IconRightZoom = 0D;
            this.btn_approve.IconVisible = true;
            this.btn_approve.IconZoom = 50D;
            this.btn_approve.IsTab = false;
            this.btn_approve.Location = new System.Drawing.Point(149, 12);
            this.btn_approve.Name = "btn_approve";
            this.btn_approve.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_approve.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.btn_approve.OnHoverTextColor = System.Drawing.Color.Black;
            this.btn_approve.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_approve.selected = false;
            this.btn_approve.Size = new System.Drawing.Size(105, 32);
            this.btn_approve.TabIndex = 19;
            this.btn_approve.Text = "אשר גיליון";
            this.btn_approve.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_approve.Textcolor = System.Drawing.Color.White;
            this.btn_approve.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_approve.Click += new System.EventHandler(this.btn_approve_Click);
            // 
            // btn_cleanExcelData
            // 
            this.btn_cleanExcelData.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.btn_cleanExcelData.BackColor = System.Drawing.Color.Firebrick;
            this.btn_cleanExcelData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cleanExcelData.BorderRadius = 0;
            this.btn_cleanExcelData.ButtonText = "נקה";
            this.btn_cleanExcelData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcel_animator.SetDecoration(this.btn_cleanExcelData, BunifuAnimatorNS.DecorationType.None);
            this.btn_cleanExcelData.DisabledColor = System.Drawing.Color.Gray;
            this.btn_cleanExcelData.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_cleanExcelData.Iconimage = ((System.Drawing.Image)(resources.GetObject("btn_cleanExcelData.Iconimage")));
            this.btn_cleanExcelData.Iconimage_right = null;
            this.btn_cleanExcelData.Iconimage_right_Selected = null;
            this.btn_cleanExcelData.Iconimage_Selected = null;
            this.btn_cleanExcelData.IconMarginLeft = 0;
            this.btn_cleanExcelData.IconMarginRight = 0;
            this.btn_cleanExcelData.IconRightVisible = true;
            this.btn_cleanExcelData.IconRightZoom = 0D;
            this.btn_cleanExcelData.IconVisible = true;
            this.btn_cleanExcelData.IconZoom = 50D;
            this.btn_cleanExcelData.IsTab = false;
            this.btn_cleanExcelData.Location = new System.Drawing.Point(55, 12);
            this.btn_cleanExcelData.Name = "btn_cleanExcelData";
            this.btn_cleanExcelData.Normalcolor = System.Drawing.Color.Firebrick;
            this.btn_cleanExcelData.OnHovercolor = System.Drawing.Color.Red;
            this.btn_cleanExcelData.OnHoverTextColor = System.Drawing.Color.Black;
            this.btn_cleanExcelData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_cleanExcelData.selected = false;
            this.btn_cleanExcelData.Size = new System.Drawing.Size(90, 32);
            this.btn_cleanExcelData.TabIndex = 18;
            this.btn_cleanExcelData.Text = "נקה";
            this.btn_cleanExcelData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cleanExcelData.Textcolor = System.Drawing.Color.White;
            this.btn_cleanExcelData.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cleanExcelData.Click += new System.EventHandler(this.btn_cleanExcelData_Click);
            // 
            // dataGridView
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(102)))), ((int)(((byte)(142)))));
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.SeaGreen;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.btnExcel_animator.SetDecoration(this.dataGridView, BunifuAnimatorNS.DecorationType.None);
            this.dataGridView.DoubleBuffered = true;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.HeaderBgColor = System.Drawing.Color.SeaGreen;
            this.dataGridView.HeaderForeColor = System.Drawing.Color.SeaGreen;
            this.dataGridView.Location = new System.Drawing.Point(8, 63);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView.Size = new System.Drawing.Size(1016, 613);
            this.dataGridView.TabIndex = 9;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // btn_inport_excel
            // 
            this.btn_inport_excel.BackColor = System.Drawing.Color.SeaGreen;
            this.btnExcel_animator.SetDecoration(this.btn_inport_excel, BunifuAnimatorNS.DecorationType.None);
            this.btn_inport_excel.Image = ((System.Drawing.Image)(resources.GetObject("btn_inport_excel.Image")));
            this.btn_inport_excel.ImageActive = null;
            this.btn_inport_excel.Location = new System.Drawing.Point(8, 8);
            this.btn_inport_excel.Name = "btn_inport_excel";
            this.btn_inport_excel.Size = new System.Drawing.Size(39, 36);
            this.btn_inport_excel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_inport_excel.TabIndex = 8;
            this.btn_inport_excel.TabStop = false;
            this.btn_inport_excel.Zoom = 10;
            this.btn_inport_excel.Click += new System.EventHandler(this.btn_inport_excel_Click);
            this.btn_inport_excel.MouseEnter += new System.EventHandler(this.btn_inport_excel_MouseEnter);
            this.btn_inport_excel.MouseLeave += new System.EventHandler(this.btn_inport_excel_MouseLeave);
            // 
            // panelImport
            // 
            this.panelImport.Controls.Add(this.panelExcel);
            this.btnExcel_animator.SetDecoration(this.panelImport, BunifuAnimatorNS.DecorationType.None);
            this.panelImport.Location = new System.Drawing.Point(1, 12);
            this.panelImport.Name = "panelImport";
            this.panelImport.Size = new System.Drawing.Size(1040, 746);
            this.panelImport.TabIndex = 1;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panelExcel;
            this.bunifuDragControl1.Vertical = true;
            // 
            // btnExcel_animator
            // 
            this.btnExcel_animator.AnimationType = BunifuAnimatorNS.AnimationType.ScaleAndHorizSlide;
            this.btnExcel_animator.Cursor = null;
            animation3.AnimateOnlyDifferences = true;
            animation3.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.BlindCoeff")));
            animation3.LeafCoeff = 0F;
            animation3.MaxTime = 1F;
            animation3.MinTime = 0F;
            animation3.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.MosaicCoeff")));
            animation3.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation3.MosaicShift")));
            animation3.MosaicSize = 0;
            animation3.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            animation3.RotateCoeff = 0F;
            animation3.RotateLimit = 0F;
            animation3.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.ScaleCoeff")));
            animation3.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.SlideCoeff")));
            animation3.TimeCoeff = 0F;
            animation3.TransparencyCoeff = 0F;
            this.btnExcel_animator.DefaultAnimation = animation3;
            // 
            // FormImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(102)))), ((int)(((byte)(142)))));
            this.ClientSize = new System.Drawing.Size(1040, 746);
            this.Controls.Add(this.panelImport);
            this.btnExcel_animator.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormImport";
            this.Text = "Form2";
            this.panelExcel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_inport_excel)).EndInit();
            this.panelImport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panelImport;
        private System.Windows.Forms.Panel panelExcel;
        private Bunifu.Framework.UI.BunifuCustomDataGrid dataGridView;
        private Bunifu.Framework.UI.BunifuImageButton btn_inport_excel;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuFlatButton btn_cleanExcelData;
        private Bunifu.Framework.UI.BunifuFlatButton btn_approve;
        private System.Windows.Forms.ComboBox CB_sheets;
        private BunifuAnimatorNS.BunifuTransition btnExcel_animator;
        private System.Windows.Forms.Label lbl_coursesFounded;
    }
}