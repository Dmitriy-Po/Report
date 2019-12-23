namespace Report
{
    partial class Form_КК_Добавление
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_КК_Добавление));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxFormEducation = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCoeff = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNameCoef = new System.Windows.Forms.TextBox();
            this.textBoxFullDesc = new System.Windows.Forms.TextBox();
            this.textBoxDetail = new System.Windows.Forms.TextBox();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.checkBoxStdInv = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Наименование:";
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveAndClose.Image")));
            this.buttonSaveAndClose.Location = new System.Drawing.Point(69, 12);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(40, 40);
            this.buttonSaveAndClose.TabIndex = 12;
            this.buttonSaveAndClose.Tag = "";
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.Location = new System.Drawing.Point(23, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(40, 40);
            this.buttonSave.TabIndex = 11;
            this.buttonSave.Tag = "";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxFormEducation
            // 
            this.comboBoxFormEducation.FormattingEnabled = true;
            this.comboBoxFormEducation.Location = new System.Drawing.Point(129, 55);
            this.comboBoxFormEducation.Name = "comboBoxFormEducation";
            this.comboBoxFormEducation.Size = new System.Drawing.Size(245, 21);
            this.comboBoxFormEducation.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "Форма обучения:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Календарный год:";
            // 
            // textBoxCoeff
            // 
            this.textBoxCoeff.Location = new System.Drawing.Point(129, 29);
            this.textBoxCoeff.Name = "textBoxCoeff";
            this.textBoxCoeff.Size = new System.Drawing.Size(245, 20);
            this.textBoxCoeff.TabIndex = 21;
            this.textBoxCoeff.Text = "00,00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Значение:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.comboBoxYear);
            this.groupBox1.Controls.Add(this.comboBoxFormEducation);
            this.groupBox1.Controls.Add(this.textBoxCoeff);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 300);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 131);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Коэффицент";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.Crimson;
            this.label11.Location = new System.Drawing.Point(111, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 18);
            this.label11.TabIndex = 43;
            this.label11.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.ForeColor = System.Drawing.Color.Crimson;
            this.label13.Location = new System.Drawing.Point(111, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 18);
            this.label13.TabIndex = 41;
            this.label13.Text = "*";
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.ItemHeight = 13;
            this.comboBoxYear.Location = new System.Drawing.Point(129, 85);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(245, 21);
            this.comboBoxYear.TabIndex = 31;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Полное наименование:\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Уточнение:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Комментарий:";
            // 
            // textBoxNameCoef
            // 
            this.textBoxNameCoef.Location = new System.Drawing.Point(141, 68);
            this.textBoxNameCoef.Name = "textBoxNameCoef";
            this.textBoxNameCoef.Size = new System.Drawing.Size(245, 20);
            this.textBoxNameCoef.TabIndex = 34;
            // 
            // textBoxFullDesc
            // 
            this.textBoxFullDesc.Location = new System.Drawing.Point(141, 95);
            this.textBoxFullDesc.Multiline = true;
            this.textBoxFullDesc.Name = "textBoxFullDesc";
            this.textBoxFullDesc.Size = new System.Drawing.Size(245, 100);
            this.textBoxFullDesc.TabIndex = 35;
            // 
            // textBoxDetail
            // 
            this.textBoxDetail.Location = new System.Drawing.Point(141, 204);
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.Size = new System.Drawing.Size(245, 20);
            this.textBoxDetail.TabIndex = 36;
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(141, 231);
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(245, 20);
            this.textBoxComment.TabIndex = 37;
            // 
            // checkBoxStdInv
            // 
            this.checkBoxStdInv.AutoSize = true;
            this.checkBoxStdInv.Location = new System.Drawing.Point(275, 257);
            this.checkBoxStdInv.Name = "checkBoxStdInv";
            this.checkBoxStdInv.Size = new System.Drawing.Size(111, 17);
            this.checkBoxStdInv.TabIndex = 38;
            this.checkBoxStdInv.Text = "Студент инвалид";
            this.checkBoxStdInv.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.ForeColor = System.Drawing.Color.Crimson;
            this.label7.Location = new System.Drawing.Point(190, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(196, 15);
            this.label7.TabIndex = 39;
            this.label7.Text = "* - Обязательно для заполнения";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.ForeColor = System.Drawing.Color.Crimson;
            this.label9.Location = new System.Drawing.Point(123, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 18);
            this.label9.TabIndex = 40;
            this.label9.Text = "*";
            // 
            // Form_КК_Добавление
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 443);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkBoxStdInv);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.textBoxDetail);
            this.Controls.Add(this.textBoxFullDesc);
            this.Controls.Add(this.textBoxNameCoef);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_КК_Добавление";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление/Редактирование";
            this.Load += new System.EventHandler(this.Form_КК_Добавление_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSaveAndClose;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox comboBoxFormEducation;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxCoeff;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxNameCoef;
        public System.Windows.Forms.TextBox textBoxFullDesc;
        public System.Windows.Forms.TextBox textBoxDetail;
        public System.Windows.Forms.TextBox textBoxComment;
        public System.Windows.Forms.CheckBox checkBoxStdInv;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
    }
}