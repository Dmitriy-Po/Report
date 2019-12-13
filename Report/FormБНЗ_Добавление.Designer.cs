namespace Report
{
    partial class FormБНЗ_Добавление
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormБНЗ_Добавление));
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxDesc = new System.Windows.Forms.TextBox();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.textBoxFillDesc = new System.Windows.Forms.TextBox();
            this.buttonDeleteKoef = new System.Windows.Forms.Button();
            this.buttonAddKoef = new System.Windows.Forms.Button();
            this.dataGridViewKoef = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKoef)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveAndClose.Image")));
            this.buttonSaveAndClose.Location = new System.Drawing.Point(68, 12);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(40, 40);
            this.buttonSaveAndClose.TabIndex = 2;
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
            this.buttonSave.TabIndex = 1;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxDesc
            // 
            this.textBoxDesc.Location = new System.Drawing.Point(150, 70);
            this.textBoxDesc.Name = "textBoxDesc";
            this.textBoxDesc.Size = new System.Drawing.Size(242, 20);
            this.textBoxDesc.TabIndex = 3;
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.ItemHeight = 13;
            this.comboBoxYear.Location = new System.Drawing.Point(150, 148);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(242, 21);
            this.comboBoxYear.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Наименование:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Полное наименование:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Комментарий:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Календарный год:";
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(150, 122);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(242, 20);
            this.textBoxComment.TabIndex = 5;
            // 
            // textBoxFillDesc
            // 
            this.textBoxFillDesc.Location = new System.Drawing.Point(150, 96);
            this.textBoxFillDesc.Name = "textBoxFillDesc";
            this.textBoxFillDesc.Size = new System.Drawing.Size(242, 20);
            this.textBoxFillDesc.TabIndex = 4;
            // 
            // buttonDeleteKoef
            // 
            this.buttonDeleteKoef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteKoef.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteKoef.Image")));
            this.buttonDeleteKoef.Location = new System.Drawing.Point(43, 257);
            this.buttonDeleteKoef.Name = "buttonDeleteKoef";
            this.buttonDeleteKoef.Size = new System.Drawing.Size(25, 25);
            this.buttonDeleteKoef.TabIndex = 8;
            this.buttonDeleteKoef.UseVisualStyleBackColor = true;
            this.buttonDeleteKoef.Click += new System.EventHandler(this.buttonDeleteKoef_Click);
            // 
            // buttonAddKoef
            // 
            this.buttonAddKoef.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddKoef.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddKoef.Image")));
            this.buttonAddKoef.Location = new System.Drawing.Point(12, 257);
            this.buttonAddKoef.Name = "buttonAddKoef";
            this.buttonAddKoef.Size = new System.Drawing.Size(25, 25);
            this.buttonAddKoef.TabIndex = 7;
            this.buttonAddKoef.UseVisualStyleBackColor = true;
            this.buttonAddKoef.Click += new System.EventHandler(this.buttonAddKoef_Click);
            // 
            // dataGridViewKoef
            // 
            this.dataGridViewKoef.AllowUserToAddRows = false;
            this.dataGridViewKoef.AllowUserToDeleteRows = false;
            this.dataGridViewKoef.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewKoef.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKoef.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            this.dataGridViewKoef.Location = new System.Drawing.Point(0, 288);
            this.dataGridViewKoef.Name = "dataGridViewKoef";
            this.dataGridViewKoef.Size = new System.Drawing.Size(404, 178);
            this.dataGridViewKoef.TabIndex = 16;
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Width = 50;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(12, 223);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(20, 1, 20, 1);
            this.label5.Size = new System.Drawing.Size(249, 17);
            this.label5.TabIndex = 17;
            this.label5.Text = "Выбрать корректирующий коэффицент";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Crimson;
            this.label7.Location = new System.Drawing.Point(196, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(196, 15);
            this.label7.TabIndex = 40;
            this.label7.Text = "* - Обязательно для заполнения";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Crimson;
            this.label6.Location = new System.Drawing.Point(130, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 18);
            this.label6.TabIndex = 41;
            this.label6.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.Color.Crimson;
            this.label8.Location = new System.Drawing.Point(130, 146);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 18);
            this.label8.TabIndex = 42;
            this.label8.Text = "*";
            // 
            // FormБНЗ_Добавление
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 466);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridViewKoef);
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
            this.MaximumSize = new System.Drawing.Size(420, 500);
            this.MinimumSize = new System.Drawing.Size(420, 500);
            this.Name = "FormБНЗ_Добавление";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление/Редактирование";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormБНЗ_Добавление_FormClosed);
            this.Load += new System.EventHandler(this.FormБНЗ_Добавление_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKoef)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonDeleteKoef;
        private System.Windows.Forms.Button buttonAddKoef;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBoxDesc;
        public System.Windows.Forms.ComboBox comboBoxYear;
        public System.Windows.Forms.TextBox textBoxComment;
        public System.Windows.Forms.TextBox textBoxFillDesc;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        public System.Windows.Forms.DataGridView dataGridViewKoef;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
    }
}