namespace Report
{
    partial class FormAddReport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddReport));
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonSaveAndClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxStdInv = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxОчно_заочное = new System.Windows.Forms.TextBox();
            this.textBoxОчное = new System.Windows.Forms.TextBox();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxЗаочное = new System.Windows.Forms.TextBox();
            this.comboBoxFilial = new System.Windows.Forms.ComboBox();
            this.comboBoxSpecial = new System.Windows.Forms.ComboBox();
            this.comboBoxSkill = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
            this.buttonSave.Location = new System.Drawing.Point(28, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(40, 40);
            this.buttonSave.TabIndex = 0;
            this.toolTip1.SetToolTip(this.buttonSave, "Сохранить");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonSaveAndClose
            // 
            this.buttonSaveAndClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveAndClose.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveAndClose.Image")));
            this.buttonSaveAndClose.Location = new System.Drawing.Point(74, 12);
            this.buttonSaveAndClose.Name = "buttonSaveAndClose";
            this.buttonSaveAndClose.Size = new System.Drawing.Size(40, 40);
            this.buttonSaveAndClose.TabIndex = 0;
            this.toolTip1.SetToolTip(this.buttonSaveAndClose, "Сохранить и закрыть");
            this.buttonSaveAndClose.UseVisualStyleBackColor = true;
            this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Календарный год";
            // 
            // checkBoxStdInv
            // 
            this.checkBoxStdInv.AutoSize = true;
            this.checkBoxStdInv.Location = new System.Drawing.Point(282, 71);
            this.checkBoxStdInv.Name = "checkBoxStdInv";
            this.checkBoxStdInv.Size = new System.Drawing.Size(111, 17);
            this.checkBoxStdInv.TabIndex = 2;
            this.checkBoxStdInv.Text = "Студент инвалид";
            this.checkBoxStdInv.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Количество студентов очной формы обучения";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Квалификация";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Специальность";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Структурное подразделение";
            // 
            // textBoxОчно_заочное
            // 
            this.textBoxОчно_заочное.Location = new System.Drawing.Point(312, 300);
            this.textBoxОчно_заочное.Name = "textBoxОчно_заочное";
            this.textBoxОчно_заочное.Size = new System.Drawing.Size(81, 20);
            this.textBoxОчно_заочное.TabIndex = 7;
            this.textBoxОчно_заочное.Text = "0";
            // 
            // textBoxОчное
            // 
            this.textBoxОчное.Location = new System.Drawing.Point(312, 274);
            this.textBoxОчное.Name = "textBoxОчное";
            this.textBoxОчное.Size = new System.Drawing.Size(81, 20);
            this.textBoxОчное.TabIndex = 6;
            this.textBoxОчное.Text = "0";
            // 
            // textBoxYear
            // 
            this.textBoxYear.Location = new System.Drawing.Point(127, 67);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(149, 20);
            this.textBoxYear.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(28, 130);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(20, 20);
            this.button2.TabIndex = 18;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.Location = new System.Drawing.Point(28, 178);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(20, 20);
            this.button3.TabIndex = 21;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.Location = new System.Drawing.Point(28, 229);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(20, 20);
            this.button5.TabIndex = 24;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 303);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(284, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Количество студентов очной-заочной формы обучения";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 329);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(252, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Количество студентов заочной формы обучения";
            // 
            // textBoxЗаочное
            // 
            this.textBoxЗаочное.Location = new System.Drawing.Point(312, 326);
            this.textBoxЗаочное.Name = "textBoxЗаочное";
            this.textBoxЗаочное.Size = new System.Drawing.Size(81, 20);
            this.textBoxЗаочное.TabIndex = 8;
            this.textBoxЗаочное.Text = "0";
            // 
            // comboBoxFilial
            // 
            this.comboBoxFilial.FormattingEnabled = true;
            this.comboBoxFilial.Location = new System.Drawing.Point(77, 131);
            this.comboBoxFilial.Name = "comboBoxFilial";
            this.comboBoxFilial.Size = new System.Drawing.Size(316, 21);
            this.comboBoxFilial.TabIndex = 27;
            // 
            // comboBoxSpecial
            // 
            this.comboBoxSpecial.FormattingEnabled = true;
            this.comboBoxSpecial.Location = new System.Drawing.Point(77, 179);
            this.comboBoxSpecial.Name = "comboBoxSpecial";
            this.comboBoxSpecial.Size = new System.Drawing.Size(316, 21);
            this.comboBoxSpecial.TabIndex = 28;
            // 
            // comboBoxSkill
            // 
            this.comboBoxSkill.FormattingEnabled = true;
            this.comboBoxSkill.Location = new System.Drawing.Point(77, 228);
            this.comboBoxSkill.Name = "comboBoxSkill";
            this.comboBoxSkill.Size = new System.Drawing.Size(316, 21);
            this.comboBoxSkill.TabIndex = 29;
            // 
            // FormAddReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 421);
            this.Controls.Add(this.comboBoxSkill);
            this.Controls.Add(this.comboBoxSpecial);
            this.Controls.Add(this.comboBoxFilial);
            this.Controls.Add(this.textBoxЗаочное);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxОчно_заочное);
            this.Controls.Add(this.textBoxОчное);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxStdInv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSaveAndClose);
            this.Controls.Add(this.buttonSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAddReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Форма редактирования/добавления";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSaveAndClose;
        public System.Windows.Forms.ComboBox comboBoxFilial;
        public System.Windows.Forms.ComboBox comboBoxSpecial;
        public System.Windows.Forms.ComboBox comboBoxSkill;
        private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.CheckBox checkBoxStdInv;
        public System.Windows.Forms.TextBox textBoxОчно_заочное;
        public System.Windows.Forms.TextBox textBoxОчное;
        public System.Windows.Forms.TextBox textBoxYear;
        public System.Windows.Forms.TextBox textBoxЗаочное;
    }
}