using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkylineProblemWinforms
{
    public class ColorDialogEx : ColorDialog
    {
        public ColorDialogEx()
        {
            this.AllowFullOpen = true;
            this.AnyColor = true;
        }
    }
}
