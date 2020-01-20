namespace Report.Forms
{
    partial class FormСозданиеОтчёта
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonShowReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.buttonPrintReport = new System.Windows.Forms.Button();
            this.buttonExportTo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCoef = new System.Windows.Forms.TextBox();
            this.GridReport = new System.Windows.Forms.DataGridView();
            this.Filial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GridReport)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonShowReport
            // 
            this.buttonShowReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonShowReport.Location = new System.Drawing.Point(95, 120);
            this.buttonShowReport.Name = "buttonShowReport";
            this.buttonShowReport.Size = new System.Drawing.Size(150, 30);
            this.buttonShowReport.TabIndex = 0;
            this.buttonShowReport.Text = "Просмотр";
            this.buttonShowReport.UseVisualStyleBackColor = true;
            this.buttonShowReport.Click += new System.EventHandler(this.buttonShowReport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(94, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Календарный год:";
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(296, 29);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(263, 21);
            this.comboBoxYear.TabIndex = 2;
            // 
            // buttonPrintReport
            // 
            this.buttonPrintReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrintReport.Location = new System.Drawing.Point(253, 120);
            this.buttonPrintReport.Name = "buttonPrintReport";
            this.buttonPrintReport.Size = new System.Drawing.Size(150, 30);
            this.buttonPrintReport.TabIndex = 3;
            this.buttonPrintReport.Text = "Печать";
            this.buttonPrintReport.UseVisualStyleBackColor = true;
            this.buttonPrintReport.Click += new System.EventHandler(this.buttonPrintReport_Click);
            // 
            // buttonExportTo
            // 
            this.buttonExportTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExportTo.Location = new System.Drawing.Point(409, 120);
            this.buttonExportTo.Name = "buttonExportTo";
            this.buttonExportTo.Size = new System.Drawing.Size(150, 30);
            this.buttonExportTo.TabIndex = 4;
            this.buttonExportTo.Text = "Сохранить как...";
            this.buttonExportTo.UseVisualStyleBackColor = true;
            this.buttonExportTo.Click += new System.EventHandler(this.buttonExportTo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(94, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Коэффицент выравнивания:";
            // 
            // textBoxCoef
            // 
            this.textBoxCoef.Location = new System.Drawing.Point(296, 57);
            this.textBoxCoef.Name = "textBoxCoef";
            this.textBoxCoef.Size = new System.Drawing.Size(263, 20);
            this.textBoxCoef.TabIndex = 6;
            this.textBoxCoef.Text = "1";
            // 
            // GridReport
            // 
            this.GridReport.AllowUserToAddRows = false;
            this.GridReport.AllowUserToDeleteRows = false;
            this.GridReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Filial,
            this.Value});
            this.GridReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GridReport.Location = new System.Drawing.Point(0, 189);
            this.GridReport.Name = "GridReport";
            this.GridReport.ReadOnly = true;
            this.GridReport.Size = new System.Drawing.Size(584, 310);
            this.GridReport.TabIndex = 7;
            // 
            // Filial
            // 
            this.Filial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Filial.DefaultCellStyle = dataGridViewCellStyle1;
            this.Filial.HeaderText = "Филиал";
            this.Filial.Name = "Filial";
            this.Filial.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Value.DefaultCellStyle = dataGridViewCellStyle2;
            this.Value.HeaderText = "Сумма, тыс. руб.";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // FormСозданиеОтчёта
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 499);
            this.Controls.Add(this.GridReport);
            this.Controls.Add(this.textBoxCoef);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonExportTo);
            this.Controls.Add(this.buttonPrintReport);
            this.Controls.Add(this.comboBoxYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonShowReport);
            this.Name = "FormСозданиеОтчёта";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormСозданиеОтчёта";
            this.Load += new System.EventHandler(this.FormСозданиеОтчёта_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GridReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonShowReport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Button buttonPrintReport;
        private System.Windows.Forms.Button buttonExportTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCoef;
        public System.Windows.Forms.DataGridView GridReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Filial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
    }
}