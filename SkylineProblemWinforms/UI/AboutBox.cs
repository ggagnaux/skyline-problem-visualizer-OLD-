using System.Reflection;
using System.Windows.Forms;
using KohdAndArt.Toolkit.Sys;

namespace SkylineProblemWinforms.UI
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            // Need to tell the utility class that this is the assembly 
            var util = new AssemblyUtilities(Assembly.GetExecutingAssembly());

            // Set the form values
            this.Text = $"About {util.AssemblyTitle}";
            this.labelProductName.Text = util.AssemblyProduct;
            this.labelVersion.Text = $"Version {util.AssemblyVersion}";
            this.labelCopyright.Text = util.AssemblyCopyright;
            this.labelCompanyName.Text = util.AssemblyCompany;
            this.textBoxDescription.Text = util.AssemblyDescription;
        }
    }
}
