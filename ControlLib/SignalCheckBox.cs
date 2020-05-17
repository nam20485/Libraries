using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Libraries.UtilityLib.Windows;

namespace Libraries.ControlLib
{
    public partial class SignalCheckBox : CheckBox
    {
        public Image CheckedImage { get; set; }
        public Image UncheckedImage { get; set; }

        public VerticalAlignment VerticalTextAlignment { get; set;  }

        public SignalCheckBox()
        {
            InitializeComponent();

            SetStyle(   ControlStyles.ResizeRedraw | 
                        ControlStyles.UserPaint | 
                        ControlStyles.AllPaintingInWmPaint|
                        ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            //base.OnPaint(pe);

            using (DoubleBuffer buffer = new DoubleBuffer(pe.Graphics, pe.ClipRectangle))
            {
                // paint background
                buffer.Graphics.Clear(BackColor);

                Rectangle imageRect = new Rectangle(buffer.ClipRectangle.Left, buffer.ClipRectangle.Top, buffer.ClipRectangle.Height, buffer.ClipRectangle.Height);
                Image imageToUse = Checked ? CheckedImage : UncheckedImage;
                if (imageToUse != null)
                {
                    Image scaledImage = ImageHandling.ScaleImage(imageToUse, imageRect.Width, imageRect.Height);

                    // paint check image
                    if (Enabled)
                    {
                        buffer.Graphics.DrawImage(scaledImage, imageRect.Location);
                    }
                    else
                    {
                        ControlPaint.DrawImageDisabled(buffer.Graphics, scaledImage, imageRect.Left, imageRect.Top, BackColor);
                    }
                }

                Rectangle textRect = new Rectangle(imageRect.Right, buffer.ClipRectangle.Top, buffer.ClipRectangle.Width - imageRect.Width, buffer.ClipRectangle.Height);

                // paint text            
                // TODO: fill out stringformat alignment flags\fields according to CheckBox.TextAlign property
                StringFormat format = new StringFormat();
                if (VerticalTextAlignment == VerticalAlignment.Center)
                {
                    format.LineAlignment = StringAlignment.Center;
                }
                else if (VerticalTextAlignment == VerticalAlignment.Top)
                {
                    format.LineAlignment = StringAlignment.Near;
                }
                else if (VerticalTextAlignment == VerticalAlignment.Bottom)
                {
                    format.LineAlignment = StringAlignment.Far;
                }

                if (Enabled)
                {
                    using (SolidBrush textBrush = new SolidBrush(ForeColor))
                    {
                        buffer.Graphics.DrawString(Text, Font, textBrush, textRect, format);
                    }
                }
                else
                {
                    ControlPaint.DrawStringDisabled(buffer.Graphics, Text, Font, BackColor, textRect, format);
                }    
            }                          
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }

        public enum VerticalAlignment
        {
            Top,
            Center,
            Bottom
        }
    }
}
