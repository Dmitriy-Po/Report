namespace Report
{
    partial class FormКоэффиценты
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormКоэффиценты));
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.dataGridViewCoeff = new System.Windows.Forms.DataGridView();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDeleteSelected = new System.Windows.Forms.Button();
            this.buttonEditString = new System.Windows.Forms.Button();
            this.buttonAddStingPattern = new System.Windows.Forms.Button();
            this.buttonAddNewString = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCoeff)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(15, 25);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(321, 21);
            this.comboBoxYear.TabIndex = 23;
            this.comboBoxYear.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // dataGridViewCoeff
            // 
            this.dataGridViewCoeff.AllowUserToAddRows = false;
            this.dataGridViewCoeff.AllowUserToDeleteRows = false;
            this.dataGridViewCoeff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCoeff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCoeff.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.check});
            this.dataGridViewCoeff.Location = new System.Drawing.Point(0, 113);
            this.dataGridViewCoeff.Name = "dataGridViewCoeff";
            this.dataGridViewCoeff.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCoeff.Size = new System.Drawing.Size(758, 287);
            this.dataGridViewCoeff.TabIndex = 22;
            // 
            // check
            // 
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.Width = 50;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Календарный год";
            // 
            // buttonDeleteSelected
            // 
            this.buttonDeleteSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteSelected.Image = ((System.Drawing.Image)(resources.GetObject("buttonDeleteSelected.Image")));
            this.buttonDeleteSelected.Location = new System.Drawing.Point(153, 66);
            this.buttonDeleteSelected.Name = "buttonDeleteSelected";
            this.buttonDeleteSelected.Size = new System.Drawing.Size(40, 40);
            this.buttonDeleteSelected.TabIndex = 20;
            this.buttonDeleteSelected.UseVisualStyleBackColor = true;
            this.buttonDeleteSelected.Click += new System.EventHandler(this.buttonDeleteSelected_Click);
            // 
            // buttonEditString
            // 
            this.buttonEditString.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditString.Image = ((System.Drawing.Image)(resources.GetObject("buttonEditString.Image")));
            this.buttonEditString.Location = new System.Drawing.Point(107, 66);
            this.buttonEditString.Name = "buttonEditString";
            this.buttonEditString.Size = new System.Drawing.Size(40, 40);
            this.buttonEditString.TabIndex = 19;
            this.buttonEditString.UseVisualStyleBackColor = true;
            this.buttonEditString.Click += new System.EventHandler(this.buttonEditString_Click);
            // 
            // buttonAddStingPattern
            // 
            this.buttonAddStingPattern.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddStingPattern.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddStingPattern.Image")));
            this.buttonAddStingPattern.Location = new System.Drawing.Point(61, 66);
            this.buttonAddStingPattern.Name = "buttonAddStingPattern";
            this.buttonAddStingPattern.Size = new System.Drawing.Size(40, 40);
            this.buttonAddStingPattern.TabIndex = 18;
            this.buttonAddStingPattern.UseVisualStyleBackColor = true;
            this.buttonAddStingPattern.Click += new System.EventHandler(this.buttonAddStingPattern_Click);
            // 
            // buttonAddNewString
            // 
            this.buttonAddNewString.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddNewString.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddNewString.Image")));
            this.buttonAddNewString.Location = new System.Drawing.Point(15, 65);
            this.buttonAddNewString.Name = "buttonAddNewString";
            this.buttonAddNewString.Padding = new System.Windows.Forms.Padding(3);
            this.buttonAddNewString.Size = new System.Drawing.Size(40, 40);
            this.buttonAddNewString.TabIndex = 17;
            this.buttonAddNewString.UseVisualStyleBackColor = true;
            this.buttonAddNewString.Click += new System.EventHandler(this.buttonAddNewString_Click);
            // 
            // FormКоэффиценты
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 400);
            this.Controls.Add(this.comboBoxYear);
            this.Controls.Add(this.dataGridViewCoeff);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDeleteSelected);
            this.Controls.Add(this.buttonEditString);
            this.Controls.Add(this.buttonAddStingPattern);
            this.Controls.Add(this.buttonAddNewString);
            this.Name = "FormКоэффиценты";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Корректирующие коэффиценты";
            this.Load += new System.EventHandler(this.FormКоэффиценты_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCoeff)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDeleteSelected;
        private System.Windows.Forms.Button buttonEditString;
        private System.Windows.Forms.Button buttonAddStingPattern;
        private System.Windows.Forms.Button buttonAddNewString;
        public System.Windows.Forms.DataGridView dataGridViewCoeff;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
    }
}