namespace Report.Forms
{
    partial class FormGroupAdd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
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
        private void InitializeComponent ()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGroupAdd));
            this.dataGridViewБНЗ_Группы = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.buttonDeleteKoef = new System.Windows.Forms.Button();
            this.buttonAddKoef = new System.Windows.Forms.Button();
            this.textBoxFillDesc = new System.Windows.Forms.TextBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridViewGroups = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewБНЗ_Группы)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewБНЗ_Группы
            // 
            this.dataGridViewБНЗ_Группы.AllowUserToAddRows = false;
            this.dataGridViewБНЗ_Группы.AllowUserToDeleteRows = false;
            this.dataGridViewБНЗ_Группы.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewБНЗ_Группы.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            this.dataGridViewБНЗ_Группы.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewБНЗ_Группы.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewБНЗ_Группы.Name = "dataGridViewБНЗ_Группы";
            this.dataGridViewБНЗ_Группы.Size = new System.Drawing.Size(621, 223);
            this.dataGridViewБНЗ_Группы.TabIndex = 31;
            this.dataGridViewБНЗ_Группы.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridViewБНЗ_Группы_CellBeginEdit);
            this.dataGridViewБНЗ_Группы.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewБНЗ_Группы_CellEndEdit);
            this.dataGridViewБНЗ_Группы.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.dataGridViewБНЗ_Группы_CellStateChanged);
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Width = 50;
            // 
            // buttonDeleteKoef
            // 
            this.buttonDeleteKoef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteKoef.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteKoef.Image")));
            this.buttonDeleteKoef.Location = new System.Drawing.Point(37, 176);
            this.buttonDeleteKoef.Name = "buttonDeleteKoef";
            this.buttonDeleteKoef.Size = new System.Drawing.Size(25, 25);
            this.buttonDeleteKoef.TabIndex = 26;
            this.buttonDeleteKoef.UseVisualStyleBackColor = true;
            this.buttonDeleteKoef.Click += new System.EventHandler(this.buttonDeleteKoef_Click);
            // 
            // buttonAddKoef
            // 
            this.buttonAddKoef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddKoef.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddKoef.Image")));
            this.buttonAddKoef.Location = new System.Drawing.Point(6, 176);
            this.buttonAddKoef.Name = "buttonAddKoef";
            this.buttonAddKoef.Size = new System.Drawing.Size(25, 25);
            this.buttonAddKoef.TabIndex = 25;
            this.buttonAddKoef.UseVisualStyleBackColor = true;
            this.buttonAddKoef.Click += new System.EventHandler(this.buttonAddKoef_Click);
            // 
            // textBoxFillDesc
            // 
            this.textBoxFillDesc.Location = new System.Drawing.Point(150, 88);
            this.textBoxFillDesc.Multiline = true;
            this.textBoxFillDesc.Name = "textBoxFillDesc";
            this.textBoxFillDesc.Size = new System.Drawing.Size(460, 20);
            this.textBoxFillDesc.TabIndex = 22;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(150, 114);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(460, 20);
            this.textBoxComment.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Календарный год:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "Комментарий:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Полное наименование:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Наименование:";
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.ItemHeight = 13;
            this.comboBoxYear.Location = new System.Drawing.Point(150, 140);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(460, 21);
            this.comboBoxYear.TabIndex = 24;
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Location = new System.Drawing.Point(150, 62);
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.Size = new System.Drawing.Size(460, 20);
            this.textBoxDesc.TabIndex = 21;
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveAndClose.Image")));
            this.buttonSaveAndClose.Location = new System.Drawing.Point(68, 12);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(40, 40);
            this.buttonSaveAndClose.TabIndex = 20;
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.Location = new System.Drawing.Point(22, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(40, 40);
            this.buttonSave.TabIndex = 19;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Crimson;
            this.label6.Location = new System.Drawing.Point(130, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 18);
            this.label6.TabIndex = 43;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Crimson;
            this.label7.Location = new System.Drawing.Point(414, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(196, 15);
            this.label7.TabIndex = 42;
            this.label7.Text = "* - Обязательно для заполнения";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Crimson;
            this.label8.Location = new System.Drawing.Point(130, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 18);
            this.label8.TabIndex = 44;
            this.label8.Text = "*";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(0, 207);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(635, 255);
            this.tabControl1.TabIndex = 45;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridViewБНЗ_Группы);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(627, 229);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Базовые нормативы";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridViewGroups);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(627, 229);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Специальности";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridViewGroups
            // 
            this.dataGridViewGroups.AllowUserToAddRows = false;
            this.dataGridViewGroups.AllowUserToDeleteRows = false;
            this.dataGridViewGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridViewGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewGroups.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewGroups.Name = "dataGridViewGroups";
            this.dataGridViewGroups.Size = new System.Drawing.Size(621, 223);
            this.dataGridViewGroups.TabIndex = 33;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            // 
            // FormGroupAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 462);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonDeleteKoef);
            this.Controls.Add(this.buttonAddKoef);
            this.Controls.Add(this.textBoxFillDesc);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxYear);
            this.Controls.Add(this.textBoxDesc);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGroupAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGroupAdd_FormClosing);
            this.Load += new System.EventHandler(this.FormGroupAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewБНЗ_Группы)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewБНЗ_Группы;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.Button buttonDeleteKoef;
        private System.Windows.Forms.Button buttonAddKoef;
        public System.Windows.Forms.TextBox textBoxFillDesc;
        public System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox comboBoxYear;
        public System.Windows.Forms.TextBox textBoxDesc;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        public System.Windows.Forms.DataGridView dataGridViewGroups;
    }
}