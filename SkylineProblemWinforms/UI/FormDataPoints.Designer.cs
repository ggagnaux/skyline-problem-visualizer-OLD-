namespace SkylineProblemWinforms
{
    partial class FormDataPoints
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
            this.listBoxDataPoints = new System.Windows.Forms.ListBox();
            this.buttonHide = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxDataPoints
            // 
            this.listBoxDataPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxDataPoints.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxDataPoints.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDataPoints.FormattingEnabled = true;
            this.listBoxDataPoints.ItemHeight = 19;
            this.listBoxDataPoints.Location = new System.Drawing.Point(3, 4);
            this.listBoxDataPoints.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxDataPoints.Name = "listBoxDataPoints";
            this.listBoxDataPoints.Size = new System.Drawing.Size(266, 686);
            this.listBoxDataPoints.TabIndex = 0;
            // 
            // buttonHide
            // 
            this.buttonHide.Location = new System.Drawing.Point(3, 693);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(266, 51);
            this.buttonHide.TabIndex = 1;
            this.buttonHide.Text = "Hide";
            this.buttonHide.UseVisualStyleBackColor = true;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // FormDataPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 749);
            this.ControlBox = false;
            this.Controls.Add(this.buttonHide);
            this.Controls.Add(this.listBoxDataPoints);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDataPoints";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Raw Data Points";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxDataPoints;
        private System.Windows.Forms.Button buttonHide;
    }
}