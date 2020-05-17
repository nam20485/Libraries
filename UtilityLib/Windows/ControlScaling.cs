using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Libraries.UtilityLib.Windows
{
    public class ControlScaling
    {
        public double ScaleFactor { get; set; }

        public ControlScaling(double scaleFactor)
        {
            ScaleFactor = scaleFactor;
        }

        // scale a control and optionally its children
        public static void ScaleControl(Control c, double scaleFactor, bool children)
        {
            //if (c is Label)
            //{
            //    (c as Label).AutoSize = false;
            //}

            // control itself
            ScaleControl(c, scaleFactor);

            // children
            if (children)
            {
                ScaleControls(c.Controls, scaleFactor);
            }
        }

        // non-static version caches scaleFactor in class member
        public void ScaleControl(Control c, bool children)
        {
            ScaleControl(c, ScaleFactor, children);
        }      

        private static void ResizeControl(Control c, double scaleFactor)
        {
            c.Size = new Size(Convert.ToInt32(c.Width * scaleFactor), Convert.ToInt32(c.Height * scaleFactor));            
        }

        private static void RelocateControl(Control c, double scaleFactor)
        {
            c.Location = new Point(Convert.ToInt32(c.Left * scaleFactor), Convert.ToInt32(c.Top * scaleFactor));
        }

        private static void ScaleFont(Control c, double scaleFactor)
        {
            c.Font = new Font(c.Font.FontFamily, (float) scaleFactor*c.Font.Size, c.Font.Style);
        }

        private static void ScaleControl(Control c, double scaleFactor)
        {
            ScaleFont(c, scaleFactor);
            ResizeControl(c, scaleFactor);
            RelocateControl(c, scaleFactor);           
        }

        private static void ScaleControls(System.Windows.Forms.Control.ControlCollection controls, double scaleFactor)
        {
            foreach (Control c in controls)
            {
                ScaleControl(c, scaleFactor);
            }
        }       
    }
}
