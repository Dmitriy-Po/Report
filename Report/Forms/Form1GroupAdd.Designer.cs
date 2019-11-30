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
            this.comboBoxCorrectKoef = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewБНЗ_Группы)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewБНЗ_Группы
            // 
            this.dataGridViewБНЗ_Группы.AllowUserToAddRows = false;
            this.dataGridViewБНЗ_Группы.AllowUserToDeleteRows = false;
            this.dataGridViewБНЗ_Группы.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewБНЗ_Группы.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            this.dataGridViewБНЗ_Группы.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewБНЗ_Группы.Location = new System.Drawing.Point(0, 284);
            this.dataGridViewБНЗ_Группы.Name = "dataGridViewБНЗ_Группы";
            this.dataGridViewБНЗ_Группы.Size = new System.Drawing.Size(684, 178);
            this.dataGridViewБНЗ_Группы.TabIndex = 31;
            this.dataGridViewБНЗ_Группы.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewБНЗ_Группы_CellEndEdit);
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
            this.buttonDeleteKoef.Location = new System.Drawing.Point(43, 249);
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
            this.buttonAddKoef.Location = new System.Drawing.Point(12, 249);
            this.buttonAddKoef.Name = "buttonAddKoef";
            this.buttonAddKoef.Size = new System.Drawing.Size(25, 25);
            this.buttonAddKoef.TabIndex = 25;
            this.buttonAddKoef.UseVisualStyleBackColor = true;
            // 
            // textBoxFillDesc
            // 
            this.textBoxFillDesc.Location = new System.Drawing.Point(150, 88);
            this.textBoxFillDesc.Name = "textBoxFillDesc";
            this.textBoxFillDesc.Size = new System.Drawing.Size(242, 20);
            this.textBoxFillDesc.TabIndex = 22;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(150, 114);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(242, 20);
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
            this.comboBoxYear.Size = new System.Drawing.Size(242, 21);
            this.comboBoxYear.TabIndex = 24;
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Location = new System.Drawing.Point(150, 62);
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.Size = new System.Drawing.Size(242, 20);
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
            // 
            // comboBoxCorrectKoef
            // 
            this.comboBoxCorrectKoef.FormattingEnabled = true;
            this.comboBoxCorrectKoef.Location = new System.Drawing.Point(22, 194);
            this.comboBoxCorrectKoef.Name = "comboBoxCorrectKoef";
            this.comboBoxCorrectKoef.Size = new System.Drawing.Size(370, 21);
            this.comboBoxCorrectKoef.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(22, 174);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(20, 1, 20, 1);
            this.label5.Size = new System.Drawing.Size(259, 17);
            this.label5.TabIndex = 32;
            this.label5.Text = "Добавление базовых нормативов затрат\r\n";
            // 
            // FormGroupAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.dataGridViewБНЗ_Группы);
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
            this.Controls.Add(this.comboBoxCorrectKoef);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGroupAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormGroupAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewБНЗ_Группы)).EndInit();
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
        public System.Windows.Forms.ComboBox comboBoxCorrectKoef;
        private System.Windows.Forms.Label label5;
    }
}