namespace SkylineProblemWinforms.UI
{
    partial class InfoPanel
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelMousePosition = new System.Windows.Forms.Label();
            this.labelZoomLevel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mouse Position:";
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.AutoSize = true;
            this.labelMousePosition.Location = new System.Drawing.Point(132, 8);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(86, 17);
            this.labelMousePosition.TabIndex = 1;
            this.labelMousePosition.Text = "1000 x 1000";
            this.labelMousePosition.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelZoomLevel
            // 
            this.labelZoomLevel.AutoSize = true;
            this.labelZoomLevel.Location = new System.Drawing.Point(132, 27);
            this.labelZoomLevel.Name = "labelZoomLevel";
            this.labelZoomLevel.Size = new System.Drawing.Size(44, 17);
            this.labelZoomLevel.TabIndex = 3;
            this.labelZoomLevel.Text = "100%";
            this.labelZoomLevel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Zoom Level:";
            // 
            // InfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(229, 76);
            this.ControlBox = false;
            this.Controls.Add(this.labelZoomLevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoPanel";
            this.Text = "Info";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMousePosition;
        private System.Windows.Forms.Label labelZoomLevel;
        private System.Windows.Forms.Label label4;
    }
}