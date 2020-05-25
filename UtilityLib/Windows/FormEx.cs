using System;
using System.Windows.Forms;
using Libraries.UtilityLib.Windows;


namespace Libraries.UtilityLib.Windows
{
    public partial class FormEx : Form
    {
        public FormEx()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Test if the About item was selected from the system menu
            if ((m.Msg == (int) Win32API.WM.SYSCOMMAND) && (m.WParam.ToInt32() == 0x1/*SYSMENU_ABOUT_ID*/))
            {
                MessageBox.Show("Custom About Dialog");
            }
        }
    }
}
