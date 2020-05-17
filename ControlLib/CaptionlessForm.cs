using System.Windows.Forms;
using Libraries.UtilityLib.Windows;
using System.Runtime.InteropServices;

namespace Libraries.ControlLib
{    
    public partial class CaptionlessForm : Form
    {
        public CaptionlessForm()
        {           
            InitializeComponent();            
        }
      
        public bool ShowCaption
        {
            set
            {
                WinFormUtils.ShowCaption(Handle, value);
            }
            get
            {
                return WinFormUtils.IsCaptionShowing(Handle);
            }
        }        

        public bool AllowUserClosing { get; set; }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {            
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    if (!AllowUserClosing)
                    {
                        e.Cancel = true;
                    }                    
                    break;
            }

            base.OnFormClosing(e);
        }
    }
}
