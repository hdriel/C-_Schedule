namespace Schedule
{
    partial class Form1
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
            BunifuAnimatorNS.Animation animation1 = new BunifuAnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            BunifuAnimatorNS.Animation animation2 = new BunifuAnimatorNS.Animation();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.slidemenu = new System.Windows.Forms.Panel();
            this.btn_listCourses2 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_setting = new Bunifu.Framework.UI.BunifuFlatButton();
            this.link_icon8 = new System.Windows.Forms.LinkLabel();
            this.btn_home = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_courses = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_import = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_export = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_save = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_exit_out = new Bunifu.Framework.UI.BunifuFlatButton();
            this.btn_menu = new Bunifu.Framework.UI.BunifuImageButton();
            this.logo = new System.Windows.Forms.PictureBox();
            this.header = new System.Windows.Forms.Panel();
            this.btn_minimize = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuImageButton2 = new Bunifu.Framework.UI.BunifuImageButton();
            this.btn_exit = new Bunifu.Framework.UI.BunifuImageButton();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.lbl_NameUser = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.lbl_title_selected = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.LogoAnimator = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.PanelAnimator = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.slidemenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_menu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_exit)).BeginInit();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // slidemenu
            // 
            this.slidemenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.slidemenu.Controls.Add(this.btn_listCourses2);
            this.slidemenu.Controls.Add(this.btn_setting);
            this.slidemenu.Controls.Add(this.link_icon8);
            this.slidemenu.Controls.Add(this.btn_home);
            this.slidemenu.Controls.Add(this.btn_courses);
            this.slidemenu.Controls.Add(this.btn_import);
            this.slidemenu.Controls.Add(this.btn_export);
            this.slidemenu.Controls.Add(this.btn_save);
            this.slidemenu.Controls.Add(this.btn_exit_out);
            this.slidemenu.Controls.Add(this.btn_menu);
            this.slidemenu.Controls.Add(this.logo);
            this.PanelAnimator.SetDecoration(this.slidemenu, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.slidemenu, BunifuAnimatorNS.DecorationType.None);
            this.slidemenu.Dock = System.Windows.Forms.DockStyle.Right;
            this.slidemenu.Location = new System.Drawing.Point(1040, 46);
            this.slidemenu.Name = "slidemenu";
            this.slidemenu.Size = new System.Drawing.Size(50, 746);
            this.slidemenu.TabIndex = 0;
            // 
            // btn_listCourses2
            // 
            this.btn_listCourses2.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_listCourses2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_listCourses2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_listCourses2.BorderRadius = 0;
            this.btn_listCourses2.ButtonText = "      רשימות שיעורים";
            this.btn_listCourses2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.btn_listCourses2, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_listCourses2, BunifuAnimatorNS.DecorationType.None);
            this.btn_listCourses2.DisabledColor = System.Drawing.Color.Gray;
            this.btn_listCourses2.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_listCourses2.Iconimage = global::Schedule.Properties.Resources.layers;
            this.btn_listCourses2.Iconimage_right = null;
            this.btn_listCourses2.Iconimage_right_Selected = null;
            this.btn_listCourses2.Iconimage_Selected = null;
            this.btn_listCourses2.IconMarginLeft = 0;
            this.btn_listCourses2.IconMarginRight = 0;
            this.btn_listCourses2.IconRightVisible = true;
            this.btn_listCourses2.IconRightZoom = 0D;
            this.btn_listCourses2.IconVisible = true;
            this.btn_listCourses2.IconZoom = 65D;
            this.btn_listCourses2.IsTab = true;
            this.btn_listCourses2.Location = new System.Drawing.Point(0, 249);
            this.btn_listCourses2.Name = "btn_listCourses2";
            this.btn_listCourses2.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_listCourses2.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_listCourses2.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_listCourses2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_listCourses2.selected = false;
            this.btn_listCourses2.Size = new System.Drawing.Size(307, 48);
            this.btn_listCourses2.TabIndex = 16;
            this.btn_listCourses2.Text = "      רשימות שיעורים";
            this.btn_listCourses2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_listCourses2.Textcolor = System.Drawing.Color.Silver;
            this.btn_listCourses2.TextFont = new System.Drawing.Font("Guttman Hatzvi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_listCourses2.Click += new System.EventHandler(this.btn_listCourses2_Click);
            // 
            // btn_setting
            // 
            this.btn_setting.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_setting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_setting.BorderRadius = 0;
            this.btn_setting.ButtonText = "      הגדרות";
            this.btn_setting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.btn_setting, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_setting, BunifuAnimatorNS.DecorationType.None);
            this.btn_setting.DisabledColor = System.Drawing.Color.Gray;
            this.btn_setting.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_setting.Iconimage = global::Schedule.Properties.Resources.setting;
            this.btn_setting.Iconimage_right = null;
            this.btn_setting.Iconimage_right_Selected = null;
            this.btn_setting.Iconimage_Selected = null;
            this.btn_setting.IconMarginLeft = 0;
            this.btn_setting.IconMarginRight = 0;
            this.btn_setting.IconRightVisible = true;
            this.btn_setting.IconRightZoom = 0D;
            this.btn_setting.IconVisible = true;
            this.btn_setting.IconZoom = 65D;
            this.btn_setting.IsTab = true;
            this.btn_setting.Location = new System.Drawing.Point(0, 465);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_setting.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_setting.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_setting.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_setting.selected = false;
            this.btn_setting.Size = new System.Drawing.Size(307, 48);
            this.btn_setting.TabIndex = 15;
            this.btn_setting.Text = "      הגדרות";
            this.btn_setting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_setting.Textcolor = System.Drawing.Color.Silver;
            this.btn_setting.TextFont = new System.Drawing.Font("Guttman Hatzvi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // link_icon8
            // 
            this.link_icon8.AutoSize = true;
            this.LogoAnimator.SetDecoration(this.link_icon8, BunifuAnimatorNS.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.link_icon8, BunifuAnimatorNS.DecorationType.None);
            this.link_icon8.Font = new System.Drawing.Font("Kristen ITC", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_icon8.ForeColor = System.Drawing.Color.White;
            this.link_icon8.LinkColor = System.Drawing.Color.White;
            this.link_icon8.Location = new System.Drawing.Point(1, 722);
            this.link_icon8.Name = "link_icon8";
            this.link_icon8.Size = new System.Drawing.Size(49, 18);
            this.link_icon8.TabIndex = 13;
            this.link_icon8.TabStop = true;
            this.link_icon8.Text = "icons8";
            this.link_icon8.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_icon8_LinkClicked_1);
            // 
            // btn_home
            // 
            this.btn_home.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_home.BorderRadius = 0;
            this.btn_home.ButtonText = "      מסך ראשי";
            this.btn_home.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.btn_home, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_home, BunifuAnimatorNS.DecorationType.None);
            this.btn_home.DisabledColor = System.Drawing.Color.Gray;
            this.btn_home.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_home.Iconimage = global::Schedule.Properties.Resources.home;
            this.btn_home.Iconimage_right = null;
            this.btn_home.Iconimage_right_Selected = null;
            this.btn_home.Iconimage_Selected = null;
            this.btn_home.IconMarginLeft = 0;
            this.btn_home.IconMarginRight = 0;
            this.btn_home.IconRightVisible = true;
            this.btn_home.IconRightZoom = 0D;
            this.btn_home.IconVisible = true;
            this.btn_home.IconZoom = 70D;
            this.btn_home.IsTab = true;
            this.btn_home.Location = new System.Drawing.Point(0, 141);
            this.btn_home.Name = "btn_home";
            this.btn_home.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_home.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_home.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_home.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_home.selected = true;
            this.btn_home.Size = new System.Drawing.Size(307, 48);
            this.btn_home.TabIndex = 12;
            this.btn_home.Text = "      מסך ראשי";
            this.btn_home.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_home.Textcolor = System.Drawing.Color.Silver;
            this.btn_home.TextFont = new System.Drawing.Font("Guttman Hatzvi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // btn_courses
            // 
            this.btn_courses.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_courses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_courses.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_courses.BorderRadius = 0;
            this.btn_courses.ButtonText = "      רשימות קורסים";
            this.btn_courses.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.btn_courses, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_courses, BunifuAnimatorNS.DecorationType.None);
            this.btn_courses.DisabledColor = System.Drawing.Color.Gray;
            this.btn_courses.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_courses.Iconimage = global::Schedule.Properties.Resources.layers;
            this.btn_courses.Iconimage_right = null;
            this.btn_courses.Iconimage_right_Selected = null;
            this.btn_courses.Iconimage_Selected = null;
            this.btn_courses.IconMarginLeft = 0;
            this.btn_courses.IconMarginRight = 0;
            this.btn_courses.IconRightVisible = true;
            this.btn_courses.IconRightZoom = 0D;
            this.btn_courses.IconVisible = true;
            this.btn_courses.IconZoom = 65D;
            this.btn_courses.IsTab = true;
            this.btn_courses.Location = new System.Drawing.Point(0, 195);
            this.btn_courses.Name = "btn_courses";
            this.btn_courses.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_courses.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_courses.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_courses.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_courses.selected = false;
            this.btn_courses.Size = new System.Drawing.Size(307, 48);
            this.btn_courses.TabIndex = 11;
            this.btn_courses.Text = "      רשימות קורסים";
            this.btn_courses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_courses.Textcolor = System.Drawing.Color.Silver;
            this.btn_courses.TextFont = new System.Drawing.Font("Guttman Hatzvi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_courses.Click += new System.EventHandler(this.btn_courses_Click);
            // 
            // btn_import
            // 
            this.btn_import.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_import.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_import.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_import.BorderRadius = 0;
            this.btn_import.ButtonText = "      ייבוא";
            this.btn_import.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.btn_import, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_import, BunifuAnimatorNS.DecorationType.None);
            this.btn_import.DisabledColor = System.Drawing.Color.Gray;
            this.btn_import.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_import.Iconimage = global::Schedule.Properties.Resources.import;
            this.btn_import.Iconimage_right = null;
            this.btn_import.Iconimage_right_Selected = null;
            this.btn_import.Iconimage_Selected = null;
            this.btn_import.IconMarginLeft = 0;
            this.btn_import.IconMarginRight = 0;
            this.btn_import.IconRightVisible = true;
            this.btn_import.IconRightZoom = 0D;
            this.btn_import.IconVisible = true;
            this.btn_import.IconZoom = 70D;
            this.btn_import.IsTab = true;
            this.btn_import.Location = new System.Drawing.Point(-1, 303);
            this.btn_import.Name = "btn_import";
            this.btn_import.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_import.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_import.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_import.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_import.selected = false;
            this.btn_import.Size = new System.Drawing.Size(307, 48);
            this.btn_import.TabIndex = 10;
            this.btn_import.Text = "      ייבוא";
            this.btn_import.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_import.Textcolor = System.Drawing.Color.Silver;
            this.btn_import.TextFont = new System.Drawing.Font("Guttman Hatzvi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // btn_export
            // 
            this.btn_export.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_export.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_export.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_export.BorderRadius = 0;
            this.btn_export.ButtonText = "      ייצוא";
            this.btn_export.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.btn_export, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_export, BunifuAnimatorNS.DecorationType.None);
            this.btn_export.DisabledColor = System.Drawing.Color.Gray;
            this.btn_export.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_export.Iconimage = global::Schedule.Properties.Resources.export;
            this.btn_export.Iconimage_right = null;
            this.btn_export.Iconimage_right_Selected = null;
            this.btn_export.Iconimage_Selected = null;
            this.btn_export.IconMarginLeft = 0;
            this.btn_export.IconMarginRight = 0;
            this.btn_export.IconRightVisible = true;
            this.btn_export.IconRightZoom = 0D;
            this.btn_export.IconVisible = true;
            this.btn_export.IconZoom = 70D;
            this.btn_export.IsTab = true;
            this.btn_export.Location = new System.Drawing.Point(-1, 357);
            this.btn_export.Name = "btn_export";
            this.btn_export.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_export.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_export.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_export.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_export.selected = false;
            this.btn_export.Size = new System.Drawing.Size(307, 48);
            this.btn_export.TabIndex = 9;
            this.btn_export.Text = "      ייצוא";
            this.btn_export.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_export.Textcolor = System.Drawing.Color.Silver;
            this.btn_export.TextFont = new System.Drawing.Font("Guttman Hatzvi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // btn_save
            // 
            this.btn_save.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.BorderRadius = 0;
            this.btn_save.ButtonText = "      שמירה וטעינה";
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.btn_save, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_save, BunifuAnimatorNS.DecorationType.None);
            this.btn_save.DisabledColor = System.Drawing.Color.Gray;
            this.btn_save.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_save.Iconimage = global::Schedule.Properties.Resources.save2;
            this.btn_save.Iconimage_right = null;
            this.btn_save.Iconimage_right_Selected = null;
            this.btn_save.Iconimage_Selected = null;
            this.btn_save.IconMarginLeft = 0;
            this.btn_save.IconMarginRight = 0;
            this.btn_save.IconRightVisible = true;
            this.btn_save.IconRightZoom = 0D;
            this.btn_save.IconVisible = true;
            this.btn_save.IconZoom = 65D;
            this.btn_save.IsTab = true;
            this.btn_save.Location = new System.Drawing.Point(0, 411);
            this.btn_save.Name = "btn_save";
            this.btn_save.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_save.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_save.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_save.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_save.selected = false;
            this.btn_save.Size = new System.Drawing.Size(307, 48);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "      שמירה וטעינה";
            this.btn_save.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Textcolor = System.Drawing.Color.Silver;
            this.btn_save.TextFont = new System.Drawing.Font("Guttman Hatzvi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_exit_out
            // 
            this.btn_exit_out.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.btn_exit_out.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_exit_out.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_exit_out.BorderRadius = 0;
            this.btn_exit_out.ButtonText = "      יציאה";
            this.btn_exit_out.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PanelAnimator.SetDecoration(this.btn_exit_out, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_exit_out, BunifuAnimatorNS.DecorationType.None);
            this.btn_exit_out.DisabledColor = System.Drawing.Color.Gray;
            this.btn_exit_out.Iconcolor = System.Drawing.Color.Transparent;
            this.btn_exit_out.Iconimage = global::Schedule.Properties.Resources.shutdown;
            this.btn_exit_out.Iconimage_right = null;
            this.btn_exit_out.Iconimage_right_Selected = null;
            this.btn_exit_out.Iconimage_Selected = null;
            this.btn_exit_out.IconMarginLeft = 0;
            this.btn_exit_out.IconMarginRight = 0;
            this.btn_exit_out.IconRightVisible = true;
            this.btn_exit_out.IconRightZoom = 0D;
            this.btn_exit_out.IconVisible = true;
            this.btn_exit_out.IconZoom = 65D;
            this.btn_exit_out.IsTab = true;
            this.btn_exit_out.Location = new System.Drawing.Point(0, 519);
            this.btn_exit_out.Name = "btn_exit_out";
            this.btn_exit_out.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_exit_out.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.btn_exit_out.OnHoverTextColor = System.Drawing.Color.Red;
            this.btn_exit_out.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btn_exit_out.selected = false;
            this.btn_exit_out.Size = new System.Drawing.Size(307, 48);
            this.btn_exit_out.TabIndex = 7;
            this.btn_exit_out.Text = "      יציאה";
            this.btn_exit_out.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_exit_out.Textcolor = System.Drawing.Color.Silver;
            this.btn_exit_out.TextFont = new System.Drawing.Font("Guttman Hatzvi", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btn_exit_out.Click += new System.EventHandler(this.btn_exit_out_Click);
            this.btn_exit_out.MouseEnter += new System.EventHandler(this.btn_exit_out_MouseEnter);
            this.btn_exit_out.MouseLeave += new System.EventHandler(this.btn_exit_out_MouseLeave);
            this.btn_exit_out.MouseHover += new System.EventHandler(this.btn_exit_out_MouseHover);
            // 
            // btn_menu
            // 
            this.btn_menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(51)))), ((int)(((byte)(79)))));
            this.PanelAnimator.SetDecoration(this.btn_menu, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_menu, BunifuAnimatorNS.DecorationType.None);
            this.btn_menu.Image = ((System.Drawing.Image)(resources.GetObject("btn_menu.Image")));
            this.btn_menu.ImageActive = null;
            this.btn_menu.Location = new System.Drawing.Point(10, 6);
            this.btn_menu.Name = "btn_menu";
            this.btn_menu.Size = new System.Drawing.Size(30, 30);
            this.btn_menu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_menu.TabIndex = 1;
            this.btn_menu.TabStop = false;
            this.btn_menu.Zoom = 10;
            this.btn_menu.Click += new System.EventHandler(this.btn_menu_Click);
            // 
            // logo
            // 
            this.LogoAnimator.SetDecoration(this.logo, BunifuAnimatorNS.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.logo, BunifuAnimatorNS.DecorationType.None);
            this.logo.Image = global::Schedule.Properties.Resources.backroung;
            this.logo.Location = new System.Drawing.Point(64, 6);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(226, 129);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 0;
            this.logo.TabStop = false;
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.header.Controls.Add(this.btn_minimize);
            this.header.Controls.Add(this.bunifuImageButton2);
            this.header.Controls.Add(this.btn_exit);
            this.header.Controls.Add(this.bunifuCustomLabel1);
            this.PanelAnimator.SetDecoration(this.header, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.header, BunifuAnimatorNS.DecorationType.None);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(1090, 46);
            this.header.TabIndex = 1;
            // 
            // btn_minimize
            // 
            this.btn_minimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.PanelAnimator.SetDecoration(this.btn_minimize, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_minimize, BunifuAnimatorNS.DecorationType.None);
            this.btn_minimize.Image = ((System.Drawing.Image)(resources.GetObject("btn_minimize.Image")));
            this.btn_minimize.ImageActive = null;
            this.btn_minimize.Location = new System.Drawing.Point(48, 1);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(45, 44);
            this.btn_minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_minimize.TabIndex = 14;
            this.btn_minimize.TabStop = false;
            this.btn_minimize.Zoom = 10;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            this.btn_minimize.MouseEnter += new System.EventHandler(this.btn_minimize_MouseEnter);
            this.btn_minimize.MouseLeave += new System.EventHandler(this.btn_minimize_MouseLeave);
            // 
            // bunifuImageButton2
            // 
            this.bunifuImageButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.PanelAnimator.SetDecoration(this.bunifuImageButton2, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.bunifuImageButton2, BunifuAnimatorNS.DecorationType.None);
            this.bunifuImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("bunifuImageButton2.Image")));
            this.bunifuImageButton2.ImageActive = null;
            this.bunifuImageButton2.Location = new System.Drawing.Point(1041, 2);
            this.bunifuImageButton2.Name = "bunifuImageButton2";
            this.bunifuImageButton2.Size = new System.Drawing.Size(47, 43);
            this.bunifuImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bunifuImageButton2.TabIndex = 13;
            this.bunifuImageButton2.TabStop = false;
            this.bunifuImageButton2.Zoom = 10;
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(117)))), ((int)(((byte)(237)))));
            this.PanelAnimator.SetDecoration(this.btn_exit, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.btn_exit, BunifuAnimatorNS.DecorationType.None);
            this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
            this.btn_exit.ImageActive = null;
            this.btn_exit.Location = new System.Drawing.Point(0, 0);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(46, 45);
            this.btn_exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btn_exit.TabIndex = 3;
            this.btn_exit.TabStop = false;
            this.btn_exit.Zoom = 10;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            this.btn_exit.MouseEnter += new System.EventHandler(this.btn_exit_MouseEnter);
            this.btn_exit.MouseLeave += new System.EventHandler(this.btn_exit_MouseLeave);
            // 
            // bunifuCustomLabel1
            // 
            this.LogoAnimator.SetDecoration(this.bunifuCustomLabel1, BunifuAnimatorNS.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.bunifuCustomLabel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Guttman Hatzvi", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.bunifuCustomLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(255)))));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(862, 6);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(185, 34);
            this.bunifuCustomLabel1.TabIndex = 2;
            this.bunifuCustomLabel1.Text = "מערכת שעות";
            this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.header;
            this.bunifuDragControl1.Vertical = true;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lbl_NameUser);
            this.panelMain.Controls.Add(this.lbl_title_selected);
            this.PanelAnimator.SetDecoration(this.panelMain, BunifuAnimatorNS.DecorationType.None);
            this.LogoAnimator.SetDecoration(this.panelMain, BunifuAnimatorNS.DecorationType.None);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 46);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1040, 746);
            this.panelMain.TabIndex = 2;
            // 
            // lbl_NameUser
            // 
            this.lbl_NameUser.BackColor = System.Drawing.Color.Transparent;
            this.LogoAnimator.SetDecoration(this.lbl_NameUser, BunifuAnimatorNS.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.lbl_NameUser, BunifuAnimatorNS.DecorationType.None);
            this.lbl_NameUser.Font = new System.Drawing.Font("Guttman Hatzvi", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl_NameUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(122)))), ((int)(((byte)(162)))));
            this.lbl_NameUser.Location = new System.Drawing.Point(0, 691);
            this.lbl_NameUser.Name = "lbl_NameUser";
            this.lbl_NameUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_NameUser.Size = new System.Drawing.Size(310, 55);
            this.lbl_NameUser.TabIndex = 4;
            this.lbl_NameUser.Text = "הדריאל בנג\'ו";
            this.lbl_NameUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_title_selected
            // 
            this.LogoAnimator.SetDecoration(this.lbl_title_selected, BunifuAnimatorNS.DecorationType.None);
            this.PanelAnimator.SetDecoration(this.lbl_title_selected, BunifuAnimatorNS.DecorationType.None);
            this.lbl_title_selected.Font = new System.Drawing.Font("Guttman Hatzvi", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lbl_title_selected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(122)))), ((int)(((byte)(162)))));
            this.lbl_title_selected.Location = new System.Drawing.Point(517, 0);
            this.lbl_title_selected.Name = "lbl_title_selected";
            this.lbl_title_selected.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbl_title_selected.Size = new System.Drawing.Size(523, 55);
            this.lbl_title_selected.TabIndex = 7;
            this.lbl_title_selected.Text = "מסך ראשי";
            this.lbl_title_selected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LogoAnimator
            // 
            this.LogoAnimator.AnimationType = BunifuAnimatorNS.AnimationType.ScaleAndRotate;
            this.LogoAnimator.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(30);
            animation1.RotateCoeff = 0.5F;
            animation1.RotateLimit = 0.2F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            this.LogoAnimator.DefaultAnimation = animation1;
            // 
            // PanelAnimator
            // 
            this.PanelAnimator.AnimationType = BunifuAnimatorNS.AnimationType.Mosaic;
            this.PanelAnimator.Cursor = null;
            animation2.AnimateOnlyDifferences = true;
            animation2.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.BlindCoeff")));
            animation2.LeafCoeff = 0F;
            animation2.MaxTime = 1F;
            animation2.MinTime = 0F;
            animation2.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicCoeff")));
            animation2.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicShift")));
            animation2.MosaicSize = 20;
            animation2.Padding = new System.Windows.Forms.Padding(30);
            animation2.RotateCoeff = 0F;
            animation2.RotateLimit = 0F;
            animation2.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.ScaleCoeff")));
            animation2.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.SlideCoeff")));
            animation2.TimeCoeff = 0F;
            animation2.TransparencyCoeff = 0F;
            this.PanelAnimator.DefaultAnimation = animation2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(102)))), ((int)(((byte)(142)))));
            this.ClientSize = new System.Drawing.Size(1090, 792);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.slidemenu);
            this.Controls.Add(this.header);
            this.LogoAnimator.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.PanelAnimator.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.slidemenu.ResumeLayout(false);
            this.slidemenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_menu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btn_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_exit)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel slidemenu;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel header;
        private Bunifu.Framework.UI.BunifuImageButton btn_menu;
        private Bunifu.Framework.UI.BunifuImageButton btn_exit;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private Bunifu.Framework.UI.BunifuFlatButton btn_exit_out;
        private Bunifu.Framework.UI.BunifuFlatButton btn_home;
        private Bunifu.Framework.UI.BunifuFlatButton btn_courses;
        private Bunifu.Framework.UI.BunifuFlatButton btn_import;
        private Bunifu.Framework.UI.BunifuFlatButton btn_export;
        private Bunifu.Framework.UI.BunifuFlatButton btn_save;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton2;
        private BunifuAnimatorNS.BunifuTransition LogoAnimator;
        private BunifuAnimatorNS.BunifuTransition PanelAnimator;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_NameUser;
        private Bunifu.Framework.UI.BunifuImageButton btn_minimize;
        private Bunifu.Framework.UI.BunifuCustomLabel lbl_title_selected;
        private System.Windows.Forms.LinkLabel link_icon8;
        private System.Windows.Forms.Panel panelMain;
        private Bunifu.Framework.UI.BunifuFlatButton btn_setting;
        private Bunifu.Framework.UI.BunifuFlatButton btn_listCourses2;
    }
}

