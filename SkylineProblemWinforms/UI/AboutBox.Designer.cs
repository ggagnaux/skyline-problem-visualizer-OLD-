namespace SkylineProblemWinforms.UI
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.buttonOK = new MetroFramework.Controls.MetroButton();
            this.textBoxThirdpartyComponents = new System.Windows.Forms.TextBox();
            this.linkLabelThirdparty = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logoPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logoPictureBox.ErrorImage = null;
            this.logoPictureBox.Image = global::SkylineProblemWinforms.Properties.Resources.SkylineProblemVisualizerLogo;
            this.logoPictureBox.InitialImage = global::SkylineProblemWinforms.Properties.Resources.SkylineProblemVisualizerLogo;
            this.logoPictureBox.Location = new System.Drawing.Point(16, 78);
            this.logoPictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(578, 399);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescription.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxDescription.Location = new System.Drawing.Point(608, 168);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.Size = new System.Drawing.Size(395, 69);
            this.textBoxDescription.TabIndex = 23;
            this.textBoxDescription.TabStop = false;
            this.textBoxDescription.Text = "Description";
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelProductName.Location = new System.Drawing.Point(604, 78);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(129, 20);
            this.labelProductName.TabIndex = 25;
            this.labelProductName.Text = "Product Version";
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyright.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelCopyright.Location = new System.Drawing.Point(604, 123);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(143, 20);
            this.labelCopyright.TabIndex = 27;
            this.labelCopyright.Text = "Product Copyright";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.buttonOK.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.buttonOK.Location = new System.Drawing.Point(861, 434);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(142, 43);
            this.buttonOK.Style = MetroFramework.MetroColorStyle.Teal;
            this.buttonOK.TabIndex = 28;
            this.buttonOK.Text = "&OK";
            this.buttonOK.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.buttonOK.UseSelectable = true;
            // 
            // textBoxThirdpartyComponents
            // 
            this.textBoxThirdpartyComponents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.textBoxThirdpartyComponents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxThirdpartyComponents.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxThirdpartyComponents.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxThirdpartyComponents.Location = new System.Drawing.Point(608, 285);
            this.textBoxThirdpartyComponents.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxThirdpartyComponents.Multiline = true;
            this.textBoxThirdpartyComponents.Name = "textBoxThirdpartyComponents";
            this.textBoxThirdpartyComponents.ReadOnly = true;
            this.textBoxThirdpartyComponents.Size = new System.Drawing.Size(395, 60);
            this.textBoxThirdpartyComponents.TabIndex = 29;
            this.textBoxThirdpartyComponents.TabStop = false;
            this.textBoxThirdpartyComponents.Text = "Third Party Components";
            // 
            // linkLabelThirdparty
            // 
            this.linkLabelThirdparty.AutoSize = true;
            this.linkLabelThirdparty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelThirdparty.LinkColor = System.Drawing.Color.Aqua;
            this.linkLabelThirdparty.Location = new System.Drawing.Point(604, 373);
            this.linkLabelThirdparty.Name = "linkLabelThirdparty";
            this.linkLabelThirdparty.Size = new System.Drawing.Size(84, 20);
            this.linkLabelThirdparty.TabIndex = 32;
            this.linkLabelThirdparty.TabStop = true;
            this.linkLabelThirdparty.Text = "linkLabel1";
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1019, 491);
            this.Controls.Add(this.linkLabelThirdparty);
            this.Controls.Add(this.textBoxThirdpartyComponents);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.logoPictureBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(12, 60, 12, 11);
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About...";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelCopyright;
        private MetroFramework.Controls.MetroButton buttonOK;
        private System.Windows.Forms.TextBox textBoxThirdpartyComponents;
        private System.Windows.Forms.LinkLabel linkLabelThirdparty;
    }
}
