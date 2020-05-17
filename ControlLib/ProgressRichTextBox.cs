using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Libraries.ControlLib
{
    public partial class ProgressRichTextBox : FormattabeRichTextBox
    {
        public Color ProgressTextColor { get; set; }
        public bool BlinkProgressText   { get; set; }

        public ProgressRichTextBox()
        {
            InitializeComponent();
        }

        public void AppendProgressText(string text)
        {
            // revert all previous text to non-blinking
            ClearBlinking();

            foreach (Selection selection in selections)
            {
                selection.ForeColor = ForeColor;
            }

            // revert non-progress text to ForeColor            
            UpdateTextColor();          

            // append progress text respecting blink and color
            AppendText(text, ProgressTextColor, BlinkProgressText);
        }      
    }
}
