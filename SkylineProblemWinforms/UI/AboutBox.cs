using System.Reflection;
using KohdAndArt.Toolkit.Sys;
using MetroFramework.Forms;

namespace SkylineProblemWinforms.UI
{
    partial class AboutBox : MetroForm //Form
    {
        private readonly string _thirdpartyDescription =
            @"'MetroFramework - Modern UI for WinForms', " +
             "Copyright (c) 2013 - Jens Thiel.";
        private readonly string _thirdpartyLink = @"https://github.com/thielj/MetroFramework";

        public AboutBox()
        {
            InitializeComponent();

            // Need to tell the utility class that this is the assembly 
            var util = new AssemblyUtilities(Assembly.GetExecutingAssembly());

            // Set the form values
            this.Text = $"About {util.AssemblyTitle}";
            this.labelProductName.Text = $"V{util.AssemblyVersion}";
            this.labelCopyright.Text = util.AssemblyCopyright;
            this.textBoxDescription.Text = util.AssemblyDescription;
            this.textBoxThirdpartyComponents.Text = _thirdpartyDescription;

            // Initialize the link label
            this.linkLabelThirdparty.Text = _thirdpartyLink;
            this.linkLabelThirdparty.Links.Add(0, _thirdpartyLink.Length, _thirdpartyLink);
            this.linkLabelThirdparty.LinkClicked += (o, i) =>
            {
                linkLabelThirdparty.LinkVisited = true;
                System.Diagnostics.Process.Start(_thirdpartyLink);
            };
        }
    }
}
