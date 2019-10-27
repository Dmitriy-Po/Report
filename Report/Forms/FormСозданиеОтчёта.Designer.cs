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
            this.buttonShowReport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.buttonPrintReport = new System.Windows.Forms.Button();
            this.buttonExportTo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCoef = new System.Windows.Forms.TextBox();
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
            // 
            // FormСозданиеОтчёта
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 162);
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
    }
}