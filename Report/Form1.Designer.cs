namespace Report
{
    partial class FormListCountStudent
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent ()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormListCountStudent));
            this.comboBoxFil = new System.Windows.Forms.ComboBox();
            this.comboBoxSpec = new System.Windows.Forms.ComboBox();
            this.checkBoxStudent_inv = new System.Windows.Forms.CheckBox();
            this.buttonAddNewString = new System.Windows.Forms.Button();
            this.buttonAddStingPattern = new System.Windows.Forms.Button();
            this.buttonEditString = new System.Windows.Forms.Button();
            this.buttonDeleteSelected = new System.Windows.Forms.Button();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.ColumnCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.стоимостныеГруппыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.группыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нормативыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.базовыеНормативыЗатратToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.коэффицентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.корректирующиеКоэффицентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сформироватьОтчётToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxFil
            // 
            this.comboBoxFil.FormattingEnabled = true;
            this.comboBoxFil.Location = new System.Drawing.Point(87, 37);
            this.comboBoxFil.Name = "comboBoxFil";
            this.comboBoxFil.Size = new System.Drawing.Size(230, 21);
            this.comboBoxFil.TabIndex = 2;
            // 
            // comboBoxSpec
            // 
            this.comboBoxSpec.FormattingEnabled = true;
            this.comboBoxSpec.Location = new System.Drawing.Point(559, 36);
            this.comboBoxSpec.Name = "comboBoxSpec";
            this.comboBoxSpec.Size = new System.Drawing.Size(230, 21);
            this.comboBoxSpec.TabIndex = 4;
            // 
            // checkBoxStudent_inv
            // 
            this.checkBoxStudent_inv.AutoSize = true;
            this.checkBoxStudent_inv.Location = new System.Drawing.Point(678, 63);
            this.checkBoxStudent_inv.Name = "checkBoxStudent_inv";
            this.checkBoxStudent_inv.Size = new System.Drawing.Size(111, 17);
            this.checkBoxStudent_inv.TabIndex = 5;
            this.checkBoxStudent_inv.Text = "Студент инвалид";
            this.checkBoxStudent_inv.UseVisualStyleBackColor = true;
            // 
            // buttonAddNewString
            // 
            this.buttonAddNewString.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddNewString.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddNewString.Image")));
            this.buttonAddNewString.Location = new System.Drawing.Point(87, 82);
            this.buttonAddNewString.Name = "buttonAddNewString";
            this.buttonAddNewString.Padding = new System.Windows.Forms.Padding(3);
            this.buttonAddNewString.Size = new System.Drawing.Size(40, 40);
            this.buttonAddNewString.TabIndex = 6;
            this.buttonAddNewString.UseVisualStyleBackColor = true;
            this.buttonAddNewString.Click += new System.EventHandler(this.buttonAddNewString_Click);
            this.buttonAddNewString.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // buttonAddStingPattern
            // 
            this.buttonAddStingPattern.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddStingPattern.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddStingPattern.Image")));
            this.buttonAddStingPattern.Location = new System.Drawing.Point(133, 83);
            this.buttonAddStingPattern.Name = "buttonAddStingPattern";
            this.buttonAddStingPattern.Size = new System.Drawing.Size(40, 40);
            this.buttonAddStingPattern.TabIndex = 7;
            this.buttonAddStingPattern.UseVisualStyleBackColor = true;
            this.buttonAddStingPattern.Click += new System.EventHandler(this.button2_Click);
            this.buttonAddStingPattern.MouseHover += new System.EventHandler(this.buttonAddStingPattern_MouseHover);
            // 
            // buttonEditString
            // 
            this.buttonEditString.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditString.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditString.Image")));
            this.buttonEditString.Location = new System.Drawing.Point(179, 83);
            this.buttonEditString.Name = "buttonEditString";
            this.buttonEditString.Size = new System.Drawing.Size(40, 40);
            this.buttonEditString.TabIndex = 8;
            this.buttonEditString.UseVisualStyleBackColor = true;
            this.buttonEditString.Click += new System.EventHandler(this.buttonEditString_Click);
            this.buttonEditString.MouseHover += new System.EventHandler(this.buttonEditString_MouseHover);
            // 
            // buttonDeleteSelected
            // 
            this.buttonDeleteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteSelected.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteSelected.Image")));
            this.buttonDeleteSelected.Location = new System.Drawing.Point(225, 83);
            this.buttonDeleteSelected.Name = "buttonDeleteSelected";
            this.buttonDeleteSelected.Size = new System.Drawing.Size(40, 40);
            this.buttonDeleteSelected.TabIndex = 9;
            this.buttonDeleteSelected.UseVisualStyleBackColor = true;
            this.buttonDeleteSelected.Click += new System.EventHandler(this.buttonDeleteSelected_Click);
            this.buttonDeleteSelected.MouseHover += new System.EventHandler(this.buttonDeleteSelected_MouseHover);
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            this.dataGridViewMain.AllowUserToDeleteRows = false;
            this.dataGridViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMain.ColumnHeadersHeight = 50;
            this.dataGridViewMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCheck});
            this.dataGridViewMain.Location = new System.Drawing.Point(0, 161);
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMain.Size = new System.Drawing.Size(984, 351);
            this.dataGridViewMain.TabIndex = 10;
            this.dataGridViewMain.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMain_CellClick);
            this.dataGridViewMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridViewMain.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewMain_CellValueChanged);
            // 
            // ColumnCheck
            // 
            this.ColumnCheck.HeaderText = "";
            this.ColumnCheck.Name = "ColumnCheck";
            this.ColumnCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnCheck.Width = 50;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.стоимостныеГруппыToolStripMenuItem,
            this.нормативыToolStripMenuItem,
            this.коэффицентыToolStripMenuItem,
            this.отчётToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // стоимостныеГруппыToolStripMenuItem
            // 
            this.стоимостныеГруппыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.группыToolStripMenuItem});
            this.стоимостныеГруппыToolStripMenuItem.Name = "стоимостныеГруппыToolStripMenuItem";
            this.стоимостныеГруппыToolStripMenuItem.Size = new System.Drawing.Size(139, 20);
            this.стоимостныеГруппыToolStripMenuItem.Text = "Стоимостные группы";
            // 
            // группыToolStripMenuItem
            // 
            this.группыToolStripMenuItem.Name = "группыToolStripMenuItem";
            this.группыToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.группыToolStripMenuItem.Text = "Группы...";
            this.группыToolStripMenuItem.Click += new System.EventHandler(this.группыToolStripMenuItem_Click);
            // 
            // нормативыToolStripMenuItem
            // 
            this.нормативыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.базовыеНормативыЗатратToolStripMenuItem});
            this.нормативыToolStripMenuItem.Name = "нормативыToolStripMenuItem";
            this.нормативыToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.нормативыToolStripMenuItem.Text = "Нормативы";
            // 
            // базовыеНормативыЗатратToolStripMenuItem
            // 
            this.базовыеНормативыЗатратToolStripMenuItem.Name = "базовыеНормативыЗатратToolStripMenuItem";
            this.базовыеНормативыЗатратToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.базовыеНормативыЗатратToolStripMenuItem.Text = "Базовые нормативы затрат...";
            this.базовыеНормативыЗатратToolStripMenuItem.Click += new System.EventHandler(this.базовыеНормативыЗатратToolStripMenuItem_Click);
            // 
            // коэффицентыToolStripMenuItem
            // 
            this.коэффицентыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.корректирующиеКоэффицентыToolStripMenuItem});
            this.коэффицентыToolStripMenuItem.Name = "коэффицентыToolStripMenuItem";
            this.коэффицентыToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.коэффицентыToolStripMenuItem.Text = "Коэффиценты";
            // 
            // корректирующиеКоэффицентыToolStripMenuItem
            // 
            this.корректирующиеКоэффицентыToolStripMenuItem.Name = "корректирующиеКоэффицентыToolStripMenuItem";
            this.корректирующиеКоэффицентыToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.корректирующиеКоэффицентыToolStripMenuItem.Text = "Корректирующие коэффиценты...";
            this.корректирующиеКоэффицентыToolStripMenuItem.Click += new System.EventHandler(this.корректирующиеКоэффицентыToolStripMenuItem_Click);
            // 
            // отчётToolStripMenuItem
            // 
            this.отчётToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сформироватьОтчётToolStripMenuItem});
            this.отчётToolStripMenuItem.Name = "отчётToolStripMenuItem";
            this.отчётToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.отчётToolStripMenuItem.Text = "Отчёт";
            // 
            // сформироватьОтчётToolStripMenuItem
            // 
            this.сформироватьОтчётToolStripMenuItem.Name = "сформироватьОтчётToolStripMenuItem";
            this.сформироватьОтчётToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.сформироватьОтчётToolStripMenuItem.Text = "Сформировать отчёт...";
            this.сформироватьОтчётToolStripMenuItem.Click += new System.EventHandler(this.сформироватьОтчётToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Структурное подразделение";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBoxYear);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonDeleteSelected);
            this.groupBox1.Controls.Add(this.comboBoxFil);
            this.groupBox1.Controls.Add(this.buttonEditString);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonAddStingPattern);
            this.groupBox1.Controls.Add(this.comboBoxSpec);
            this.groupBox1.Controls.Add(this.buttonAddNewString);
            this.groupBox1.Controls.Add(this.checkBoxStudent_inv);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(960, 128);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Отбор по данным";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(323, 36);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(230, 21);
            this.comboBoxYear.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(556, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Специальность";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(319, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Календарный год";
            // 
            // FormListCountStudent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 512);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridViewMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "FormListCountStudent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Численность обучающихся";
            this.Load += new System.EventHandler(this.FormListCountStudent_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxFil;
        private System.Windows.Forms.ComboBox comboBoxSpec;
        private System.Windows.Forms.CheckBox checkBoxStudent_inv;
        private System.Windows.Forms.Button buttonAddNewString;
        private System.Windows.Forms.Button buttonAddStingPattern;
        private System.Windows.Forms.Button buttonEditString;
        private System.Windows.Forms.Button buttonDeleteSelected;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.ToolStripMenuItem нормативыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem базовыеНормативыЗатратToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem коэффицентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem корректирующиеКоэффицентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчётToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сформироватьОтчётToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnCheck;
        private System.Windows.Forms.ToolStripMenuItem стоимостныеГруппыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem группыToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxYear;
    }
}

