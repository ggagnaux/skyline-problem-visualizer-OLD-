namespace SkylineProblemWinforms
{
    partial class FormManageSkylineSettings
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
            this.checkBoxHighlightSkyline = new System.Windows.Forms.CheckBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.labelSkylineBorderColor = new System.Windows.Forms.Label();
            this.panelSkylineBorderColorSwatch = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxShowDataPointWindow = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelSkylineBorderWidth = new System.Windows.Forms.Label();
            this.textBoxCanvasGridColor = new System.Windows.Forms.TextBox();
            this.textBoxSkylineBorderWidth = new System.Windows.Forms.TextBox();
            this.textBoxSkylineBorderColor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCanvasMarginInPixels = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelCanvasGridColorSwatch = new System.Windows.Forms.Panel();
            this.labelCanvasGridColor = new System.Windows.Forms.Label();
            this.checkBoxShowGrid = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCanvasBackgroundColor = new System.Windows.Forms.TextBox();
            this.panelCanvasBackgroundColorSwatch = new System.Windows.Forms.Panel();
            this.checkBoxShowXAxis = new System.Windows.Forms.CheckBox();
            this.checkBoxShowYAxis = new System.Windows.Forms.CheckBox();
            this.checkBoxShowCoordinates = new System.Windows.Forms.CheckBox();
            this.textBoxDefaultDataFile = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxHighlightSkyline
            // 
            this.checkBoxHighlightSkyline.AutoSize = true;
            this.checkBoxHighlightSkyline.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxHighlightSkyline.Location = new System.Drawing.Point(21, 31);
            this.checkBoxHighlightSkyline.Name = "checkBoxHighlightSkyline";
            this.checkBoxHighlightSkyline.Size = new System.Drawing.Size(82, 24);
            this.checkBoxHighlightSkyline.TabIndex = 5;
            this.checkBoxHighlightSkyline.Text = "Enable";
            this.checkBoxHighlightSkyline.UseVisualStyleBackColor = true;
            this.checkBoxHighlightSkyline.CheckedChanged += new System.EventHandler(this.checkBoxHighlightSkyline_CheckedChanged);
            // 
            // labelSkylineBorderColor
            // 
            this.labelSkylineBorderColor.AutoSize = true;
            this.labelSkylineBorderColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkylineBorderColor.Location = new System.Drawing.Point(47, 88);
            this.labelSkylineBorderColor.Name = "labelSkylineBorderColor";
            this.labelSkylineBorderColor.Size = new System.Drawing.Size(168, 20);
            this.labelSkylineBorderColor.TabIndex = 5;
            this.labelSkylineBorderColor.Text = "Skyline Border Color:";
            // 
            // panelSkylineBorderColorSwatch
            // 
            this.panelSkylineBorderColorSwatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSkylineBorderColorSwatch.Location = new System.Drawing.Point(246, 88);
            this.panelSkylineBorderColorSwatch.Name = "panelSkylineBorderColorSwatch";
            this.panelSkylineBorderColorSwatch.Size = new System.Drawing.Size(47, 27);
            this.panelSkylineBorderColorSwatch.TabIndex = 6;
            this.panelSkylineBorderColorSwatch.Click += new System.EventHandler(this.panelSkylineBorderColorSwatch_Click);
            this.panelSkylineBorderColorSwatch.DoubleClick += new System.EventHandler(this.panelSkylineBorderColorSwatch_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(712, 328);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(98, 34);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Close";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Default Data File:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.Location = new System.Drawing.Point(712, 14);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(98, 30);
            this.buttonBrowse.TabIndex = 3;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // checkBoxShowDataPointWindow
            // 
            this.checkBoxShowDataPointWindow.AutoSize = true;
            this.checkBoxShowDataPointWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowDataPointWindow.Location = new System.Drawing.Point(16, 58);
            this.checkBoxShowDataPointWindow.Name = "checkBoxShowDataPointWindow";
            this.checkBoxShowDataPointWindow.Size = new System.Drawing.Size(220, 24);
            this.checkBoxShowDataPointWindow.TabIndex = 4;
            this.checkBoxShowDataPointWindow.Text = "Show Data Point Window";
            this.checkBoxShowDataPointWindow.UseVisualStyleBackColor = true;
            this.checkBoxShowDataPointWindow.CheckedChanged += new System.EventHandler(this.checkBoxShowDataPointWindow_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelSkylineBorderWidth);
            this.groupBox1.Controls.Add(this.textBoxCanvasGridColor);
            this.groupBox1.Controls.Add(this.textBoxSkylineBorderWidth);
            this.groupBox1.Controls.Add(this.textBoxSkylineBorderColor);
            this.groupBox1.Controls.Add(this.panelSkylineBorderColorSwatch);
            this.groupBox1.Controls.Add(this.checkBoxHighlightSkyline);
            this.groupBox1.Controls.Add(this.labelSkylineBorderColor);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(16, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 143);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skyline Highlighting";
            // 
            // labelSkylineBorderWidth
            // 
            this.labelSkylineBorderWidth.AutoSize = true;
            this.labelSkylineBorderWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSkylineBorderWidth.Location = new System.Drawing.Point(47, 58);
            this.labelSkylineBorderWidth.Name = "labelSkylineBorderWidth";
            this.labelSkylineBorderWidth.Size = new System.Drawing.Size(113, 20);
            this.labelSkylineBorderWidth.TabIndex = 22;
            this.labelSkylineBorderWidth.Text = "Border Width:";
            // 
            // textBoxCanvasGridColor
            // 
            this.textBoxCanvasGridColor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCanvasGridColor.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxCanvasGridColor.Location = new System.Drawing.Point(293, 175);
            this.textBoxCanvasGridColor.MaxLength = 8;
            this.textBoxCanvasGridColor.Name = "textBoxCanvasGridColor";
            this.textBoxCanvasGridColor.ReadOnly = true;
            this.textBoxCanvasGridColor.Size = new System.Drawing.Size(0, 20);
            this.textBoxCanvasGridColor.TabIndex = 20;
            // 
            // textBoxSkylineBorderWidth
            // 
            this.textBoxSkylineBorderWidth.Location = new System.Drawing.Point(246, 55);
            this.textBoxSkylineBorderWidth.Name = "textBoxSkylineBorderWidth";
            this.textBoxSkylineBorderWidth.Size = new System.Drawing.Size(47, 27);
            this.textBoxSkylineBorderWidth.TabIndex = 21;
            this.textBoxSkylineBorderWidth.TextChanged += new System.EventHandler(this.textBoxSkylineBorderWidth_TextChanged);
            // 
            // textBoxSkylineBorderColor
            // 
            this.textBoxSkylineBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSkylineBorderColor.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxSkylineBorderColor.Location = new System.Drawing.Point(294, 65);
            this.textBoxSkylineBorderColor.MaxLength = 8;
            this.textBoxSkylineBorderColor.Name = "textBoxSkylineBorderColor";
            this.textBoxSkylineBorderColor.ReadOnly = true;
            this.textBoxSkylineBorderColor.Size = new System.Drawing.Size(0, 20);
            this.textBoxSkylineBorderColor.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(388, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Canvas Margin:";
            // 
            // textBoxCanvasMarginInPixels
            // 
            this.textBoxCanvasMarginInPixels.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCanvasMarginInPixels.Location = new System.Drawing.Point(648, 163);
            this.textBoxCanvasMarginInPixels.Name = "textBoxCanvasMarginInPixels";
            this.textBoxCanvasMarginInPixels.Size = new System.Drawing.Size(47, 27);
            this.textBoxCanvasMarginInPixels.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelCanvasGridColorSwatch);
            this.groupBox2.Controls.Add(this.labelCanvasGridColor);
            this.groupBox2.Controls.Add(this.checkBoxShowGrid);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(16, 246);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(335, 116);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Canvas Grid";
            // 
            // panelCanvasGridColorSwatch
            // 
            this.panelCanvasGridColorSwatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCanvasGridColorSwatch.Location = new System.Drawing.Point(246, 52);
            this.panelCanvasGridColorSwatch.Name = "panelCanvasGridColorSwatch";
            this.panelCanvasGridColorSwatch.Size = new System.Drawing.Size(43, 30);
            this.panelCanvasGridColorSwatch.TabIndex = 2;
            this.panelCanvasGridColorSwatch.Click += new System.EventHandler(this.panelCanvasGridColorSwatch_Click);
            this.panelCanvasGridColorSwatch.DoubleClick += new System.EventHandler(this.panelCanvasGridColorSwatch_Click);
            // 
            // labelCanvasGridColor
            // 
            this.labelCanvasGridColor.AutoSize = true;
            this.labelCanvasGridColor.Location = new System.Drawing.Point(47, 58);
            this.labelCanvasGridColor.Name = "labelCanvasGridColor";
            this.labelCanvasGridColor.Size = new System.Drawing.Size(91, 20);
            this.labelCanvasGridColor.TabIndex = 1;
            this.labelCanvasGridColor.Text = "Grid Color:";
            // 
            // checkBoxShowGrid
            // 
            this.checkBoxShowGrid.AutoSize = true;
            this.checkBoxShowGrid.Location = new System.Drawing.Point(22, 27);
            this.checkBoxShowGrid.Name = "checkBoxShowGrid";
            this.checkBoxShowGrid.Size = new System.Drawing.Size(82, 24);
            this.checkBoxShowGrid.TabIndex = 0;
            this.checkBoxShowGrid.Text = "Enable";
            this.checkBoxShowGrid.UseVisualStyleBackColor = true;
            this.checkBoxShowGrid.CheckedChanged += new System.EventHandler(this.checkBoxShowGrid_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(388, 201);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Canvas Background Color:";
            // 
            // textBoxCanvasBackgroundColor
            // 
            this.textBoxCanvasBackgroundColor.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxCanvasBackgroundColor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCanvasBackgroundColor.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxCanvasBackgroundColor.Location = new System.Drawing.Point(701, 199);
            this.textBoxCanvasBackgroundColor.Name = "textBoxCanvasBackgroundColor";
            this.textBoxCanvasBackgroundColor.Size = new System.Drawing.Size(10, 15);
            this.textBoxCanvasBackgroundColor.TabIndex = 5;
            // 
            // panelCanvasBackgroundColorSwatch
            // 
            this.panelCanvasBackgroundColorSwatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCanvasBackgroundColorSwatch.Location = new System.Drawing.Point(648, 196);
            this.panelCanvasBackgroundColorSwatch.Name = "panelCanvasBackgroundColorSwatch";
            this.panelCanvasBackgroundColorSwatch.Size = new System.Drawing.Size(47, 30);
            this.panelCanvasBackgroundColorSwatch.TabIndex = 4;
            this.panelCanvasBackgroundColorSwatch.Click += new System.EventHandler(this.panelCanvasBackgroundColorSwatch_Click);
            this.panelCanvasBackgroundColorSwatch.DoubleClick += new System.EventHandler(this.panelCanvasBackgroundColorSwatch_Click);
            // 
            // checkBoxShowXAxis
            // 
            this.checkBoxShowXAxis.AutoSize = true;
            this.checkBoxShowXAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowXAxis.Location = new System.Drawing.Point(388, 78);
            this.checkBoxShowXAxis.Name = "checkBoxShowXAxis";
            this.checkBoxShowXAxis.Size = new System.Drawing.Size(126, 24);
            this.checkBoxShowXAxis.TabIndex = 20;
            this.checkBoxShowXAxis.Text = "Show X-Axis";
            this.checkBoxShowXAxis.UseVisualStyleBackColor = true;
            this.checkBoxShowXAxis.CheckedChanged += new System.EventHandler(this.checkBoxShowXAxis_CheckedChanged);
            // 
            // checkBoxShowYAxis
            // 
            this.checkBoxShowYAxis.AutoSize = true;
            this.checkBoxShowYAxis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowYAxis.Location = new System.Drawing.Point(388, 105);
            this.checkBoxShowYAxis.Name = "checkBoxShowYAxis";
            this.checkBoxShowYAxis.Size = new System.Drawing.Size(125, 24);
            this.checkBoxShowYAxis.TabIndex = 21;
            this.checkBoxShowYAxis.Text = "Show Y-Axis";
            this.checkBoxShowYAxis.UseVisualStyleBackColor = true;
            this.checkBoxShowYAxis.CheckedChanged += new System.EventHandler(this.checkBoxShowYAxis_CheckedChanged);
            // 
            // checkBoxShowCoordinates
            // 
            this.checkBoxShowCoordinates.AutoSize = true;
            this.checkBoxShowCoordinates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowCoordinates.Location = new System.Drawing.Point(388, 133);
            this.checkBoxShowCoordinates.Name = "checkBoxShowCoordinates";
            this.checkBoxShowCoordinates.Size = new System.Drawing.Size(167, 24);
            this.checkBoxShowCoordinates.TabIndex = 22;
            this.checkBoxShowCoordinates.Text = "Show Coordinates";
            this.checkBoxShowCoordinates.UseVisualStyleBackColor = true;
            this.checkBoxShowCoordinates.CheckedChanged += new System.EventHandler(this.checkBoxShowCoordinates_CheckedChanged);
            // 
            // textBoxDefaultDataFile
            // 
            this.textBoxDefaultDataFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDefaultDataFile.Location = new System.Drawing.Point(164, 15);
            this.textBoxDefaultDataFile.Name = "textBoxDefaultDataFile";
            this.textBoxDefaultDataFile.Size = new System.Drawing.Size(531, 27);
            this.textBoxDefaultDataFile.TabIndex = 2;
            // 
            // FormManageSkylineSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 374);
            this.Controls.Add(this.checkBoxShowCoordinates);
            this.Controls.Add(this.checkBoxShowYAxis);
            this.Controls.Add(this.checkBoxShowXAxis);
            this.Controls.Add(this.textBoxCanvasBackgroundColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelCanvasBackgroundColorSwatch);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.textBoxCanvasMarginInPixels);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxShowDataPointWindow);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxDefaultDataFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormManageSkylineSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxHighlightSkyline;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label labelSkylineBorderColor;
        private System.Windows.Forms.Panel panelSkylineBorderColorSwatch;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBoxShowDataPointWindow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCanvasMarginInPixels;
        private System.Windows.Forms.TextBox textBoxSkylineBorderColor;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxShowGrid;
        private System.Windows.Forms.Label labelCanvasGridColor;
        private System.Windows.Forms.Panel panelCanvasGridColorSwatch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxCanvasBackgroundColor;
        private System.Windows.Forms.Panel panelCanvasBackgroundColorSwatch;
        private System.Windows.Forms.TextBox textBoxCanvasGridColor;
        private System.Windows.Forms.CheckBox checkBoxShowXAxis;
        private System.Windows.Forms.CheckBox checkBoxShowYAxis;
        private System.Windows.Forms.CheckBox checkBoxShowCoordinates;
        private System.Windows.Forms.Label labelSkylineBorderWidth;
        private System.Windows.Forms.TextBox textBoxSkylineBorderWidth;
        private System.Windows.Forms.TextBox textBoxDefaultDataFile;
    }
}