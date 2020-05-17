using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Libraries.UtilityLib.Windows
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]   
    public class NativeWindowEx : NativeWindow
    {
        protected readonly Form form;
        protected IntPtr hWnd;

        public NativeWindowEx(Form form)
        {
            this.form = form;
            form.HandleCreated += OnFormHandleCreated;
            form.HandleDestroyed += OnFormHandleDestroyed;  

//            CreateParams cp = new CreateParams();
//
//            // Fill in the CreateParams details.
//            cp.Caption = "Click here";
//            cp.ClassName = "Button";
//
//            // Set the position on the form
//            cp.X = 100;
//            cp.Y = 100;
//            cp.Height = 100;
//            cp.Width = 100;
//
//            // Specify the form as the parent.
//            cp.Parent = parent.Handle;
//
//            // Create as a child of the specified parent
//            cp.Style = WS_CHILD | WS_VISIBLE;
//
//            // Create the actual window 
//            this.CreateHandle(cp);
        }

        protected void OnFormHandleDestroyed (object sender, EventArgs e)
        {
            AssignHandle((sender as Form).Handle);
        }

        protected void OnFormHandleCreated (object sender, EventArgs e)
        {
            ReleaseHandle();
        }

//        public override void Finalize()
//        {
//        }

        public override void CreateHandle(CreateParams cp)
        {
            base.CreateHandle(cp);
        }

        public override void DestroyHandle()
        {
            base.DestroyHandle();
        }

        public override void ReleaseHandle()
        {
            base.ReleaseHandle();
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void OnHandleChange()
        {
            // handle changed
            hWnd = Handle;
        }

        protected override void OnThreadException(Exception e)
        {
            base.OnThreadException(e);
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);
        }
    }
}
