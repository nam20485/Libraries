namespace Libraries.UnitLib
{
    partial class UnitConverterControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.unitTypeComboBox = new System.Windows.Forms.ComboBox();
            this.sourceMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.sourceUnitComboBox = new System.Windows.Forms.ComboBox();
            this.resultMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.destUnitComboBox = new System.Windows.Forms.ComboBox();
            this.equalsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // unitTypeComboBox
            // 
            this.unitTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitTypeComboBox.FormattingEnabled = true;
            this.unitTypeComboBox.Location = new System.Drawing.Point(4, 4);
            this.unitTypeComboBox.Name = "unitTypeComboBox";
            this.unitTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.unitTypeComboBox.TabIndex = 0;
            this.unitTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.unitTypeComboBox_SelectedIndexChanged);
            // 
            // sourceMaskedTextBox
            // 
            this.sourceMaskedTextBox.Location = new System.Drawing.Point(4, 41);
            this.sourceMaskedTextBox.Name = "sourceMaskedTextBox";
            this.sourceMaskedTextBox.PromptChar = '#';
            this.sourceMaskedTextBox.ResetOnPrompt = false;
            this.sourceMaskedTextBox.Size = new System.Drawing.Size(73, 20);
            this.sourceMaskedTextBox.TabIndex = 1;
            this.sourceMaskedTextBox.TextChanged += new System.EventHandler(this.sourceMaskedTextBox_TextChanged);
            // 
            // sourceUnitComboBox
            // 
            this.sourceUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sourceUnitComboBox.FormattingEnabled = true;
            this.sourceUnitComboBox.Location = new System.Drawing.Point(83, 40);
            this.sourceUnitComboBox.Name = "sourceUnitComboBox";
            this.sourceUnitComboBox.Size = new System.Drawing.Size(101, 21);
            this.sourceUnitComboBox.TabIndex = 2;
            this.sourceUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.sourceUnitComboBox_SelectedIndexChanged);
            // 
            // resultMaskedTextBox
            // 
            this.resultMaskedTextBox.Location = new System.Drawing.Point(218, 41);
            this.resultMaskedTextBox.Name = "resultMaskedTextBox";
            this.resultMaskedTextBox.ReadOnly = true;
            this.resultMaskedTextBox.Size = new System.Drawing.Size(73, 20);
            this.resultMaskedTextBox.TabIndex = 3;
            // 
            // destUnitComboBox
            // 
            this.destUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destUnitComboBox.FormattingEnabled = true;
            this.destUnitComboBox.Location = new System.Drawing.Point(297, 40);
            this.destUnitComboBox.Name = "destUnitComboBox";
            this.destUnitComboBox.Size = new System.Drawing.Size(101, 21);
            this.destUnitComboBox.TabIndex = 4;
            this.destUnitComboBox.SelectedIndexChanged += new System.EventHandler(this.destUnitComboBox_SelectedIndexChanged);
            // 
            // equalsLabel
            // 
            this.equalsLabel.AutoSize = true;
            this.equalsLabel.Location = new System.Drawing.Point(189, 44);
            this.equalsLabel.Name = "equalsLabel";
            this.equalsLabel.Size = new System.Drawing.Size(25, 13);
            this.equalsLabel.TabIndex = 5;
            this.equalsLabel.Text = "==>";
            // 
            // UnitConverterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.equalsLabel);
            this.Controls.Add(this.destUnitComboBox);
            this.Controls.Add(this.resultMaskedTextBox);
            this.Controls.Add(this.sourceUnitComboBox);
            this.Controls.Add(this.sourceMaskedTextBox);
            this.Controls.Add(this.unitTypeComboBox);
            this.Name = "UnitConverterControl";
            this.Size = new System.Drawing.Size(404, 77);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox unitTypeComboBox;
        private System.Windows.Forms.MaskedTextBox sourceMaskedTextBox;
        private System.Windows.Forms.ComboBox sourceUnitComboBox;
        private System.Windows.Forms.MaskedTextBox resultMaskedTextBox;
        private System.Windows.Forms.ComboBox destUnitComboBox;
        private System.Windows.Forms.Label equalsLabel;
    }
}
