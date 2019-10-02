namespace Report
{
    partial class FormБазовыеНормативыЗатрат
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
            this.textBoxValues = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxValues
            // 
            this.textBoxValues.Location = new System.Drawing.Point(12, 48);
            this.textBoxValues.Name = "textBoxValues";
            this.textBoxValues.Size = new System.Drawing.Size(368, 20);
            this.textBoxValues.TabIndex = 0;
            // 
            // FormБазовыеНормативыЗатрат
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 217);
            this.Controls.Add(this.textBoxValues);
            this.Name = "FormБазовыеНормативыЗатрат";
            this.Text = "Базовые Нормативы Затрат";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxValues;
    }
}