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
            this.labelMousePositionLabel = new MetroFramework.Controls.MetroLabel();
            this.labelMousePosition = new MetroFramework.Controls.MetroLabel();
            this.labelZoomLevel = new MetroFramework.Controls.MetroLabel();
            this.labelZoomLevelLabel = new MetroFramework.Controls.MetroLabel();
            this.listViewData = new System.Windows.Forms.ListView();
            this.tabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPageSourceData = new MetroFramework.Controls.MetroTabPage();
            this.tabPageLog = new MetroFramework.Controls.MetroTabPage();
            this.textBoxProgramLog = new System.Windows.Forms.TextBox();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.tabControl.SuspendLayout();
            this.tabPageSourceData.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMousePositionLabel
            // 
            this.labelMousePositionLabel.AutoSize = true;
            this.labelMousePositionLabel.Location = new System.Drawing.Point(12, 73);
            this.labelMousePositionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMousePositionLabel.Name = "labelMousePositionLabel";
            this.labelMousePositionLabel.Size = new System.Drawing.Size(105, 20);
            this.labelMousePositionLabel.TabIndex = 0;
            this.labelMousePositionLabel.Text = "Mouse Position:";
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.AutoSize = true;
            this.labelMousePosition.Location = new System.Drawing.Point(173, 73);
            this.labelMousePosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(81, 20);
            this.labelMousePosition.TabIndex = 1;
            this.labelMousePosition.Text = "1000 x 1000";
            this.labelMousePosition.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelZoomLevel
            // 
            this.labelZoomLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelZoomLevel.AutoSize = true;
            this.labelZoomLevel.Location = new System.Drawing.Point(221, 97);
            this.labelZoomLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelZoomLevel.Name = "labelZoomLevel";
            this.labelZoomLevel.Size = new System.Drawing.Size(42, 20);
            this.labelZoomLevel.TabIndex = 3;
            this.labelZoomLevel.Text = "100%";
            this.labelZoomLevel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelZoomLevelLabel
            // 
            this.labelZoomLevelLabel.AutoSize = true;
            this.labelZoomLevelLabel.Location = new System.Drawing.Point(12, 97);
            this.labelZoomLevelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelZoomLevelLabel.Name = "labelZoomLevelLabel";
            this.labelZoomLevelLabel.Size = new System.Drawing.Size(86, 20);
            this.labelZoomLevelLabel.TabIndex = 2;
            this.labelZoomLevelLabel.Text = "Zoom Level:";
            // 
            // listViewData
            // 
            this.listViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewData.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.listViewData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewData.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.listViewData.Location = new System.Drawing.Point(4, 17);
            this.listViewData.Margin = new System.Windows.Forms.Padding(4);
            this.listViewData.MultiSelect = false;
            this.listViewData.Name = "listViewData";
            this.listViewData.Size = new System.Drawing.Size(552, 320);
            this.listViewData.TabIndex = 4;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.View = System.Windows.Forms.View.Details;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageSourceData);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Location = new System.Drawing.Point(8, 120);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 1;
            this.tabControl.Size = new System.Drawing.Size(287, 420);
            this.tabControl.TabIndex = 6;
            this.tabControl.UseSelectable = true;
            // 
            // tabPageSourceData
            // 
            this.tabPageSourceData.Controls.Add(this.listViewData);
            this.tabPageSourceData.HorizontalScrollbarBarColor = true;
            this.tabPageSourceData.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageSourceData.HorizontalScrollbarSize = 2;
            this.tabPageSourceData.Location = new System.Drawing.Point(4, 38);
            this.tabPageSourceData.Name = "tabPageSourceData";
            this.tabPageSourceData.Size = new System.Drawing.Size(279, 378);
            this.tabPageSourceData.TabIndex = 0;
            this.tabPageSourceData.Text = "Source Data";
            this.tabPageSourceData.VerticalScrollbarBarColor = true;
            this.tabPageSourceData.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageSourceData.VerticalScrollbarSize = 2;
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.textBoxProgramLog);
            this.tabPageLog.HorizontalScrollbarBarColor = true;
            this.tabPageLog.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPageLog.HorizontalScrollbarSize = 2;
            this.tabPageLog.Location = new System.Drawing.Point(4, 38);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(279, 378);
            this.tabPageLog.TabIndex = 1;
            this.tabPageLog.Text = "Program Log";
            this.tabPageLog.VerticalScrollbarBarColor = true;
            this.tabPageLog.VerticalScrollbarHighlightOnWheel = false;
            this.tabPageLog.VerticalScrollbarSize = 2;
            // 
            // textBoxProgramLog
            // 
            this.textBoxProgramLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxProgramLog.BackColor = System.Drawing.SystemColors.InfoText;
            this.textBoxProgramLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxProgramLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProgramLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.textBoxProgramLog.Location = new System.Drawing.Point(3, 3);
            this.textBoxProgramLog.Margin = new System.Windows.Forms.Padding(10);
            this.textBoxProgramLog.MaxLength = 1000000;
            this.textBoxProgramLog.Multiline = true;
            this.textBoxProgramLog.Name = "textBoxProgramLog";
            this.textBoxProgramLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxProgramLog.Size = new System.Drawing.Size(272, 394);
            this.textBoxProgramLog.TabIndex = 2;
            this.textBoxProgramLog.TextChanged += new System.EventHandler(this.textBoxProgramLog_TextChanged);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // InfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(301, 551);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.labelZoomLevel);
            this.Controls.Add(this.labelZoomLevelLabel);
            this.Controls.Add(this.labelMousePosition);
            this.Controls.Add(this.labelMousePositionLabel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoPanel";
            this.Opacity = 0.9D;
            this.Padding = new System.Windows.Forms.Padding(25, 75, 25, 25);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Text = "Info";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InfoPanel_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPageSourceData.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel labelMousePositionLabel;
        private MetroFramework.Controls.MetroLabel labelMousePosition;
        private MetroFramework.Controls.MetroLabel labelZoomLevel;
        private MetroFramework.Controls.MetroLabel labelZoomLevelLabel;
        private System.Windows.Forms.ListView listViewData;
        private MetroFramework.Controls.MetroTabControl tabControl;
        private MetroFramework.Controls.MetroTabPage tabPageSourceData;
        private MetroFramework.Controls.MetroTabPage tabPageLog;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.TextBox textBoxProgramLog;
    }
}