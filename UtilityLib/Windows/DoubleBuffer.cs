using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.Disposeable;

namespace Libraries.UtilityLib.Windows
{
    public class DoubleBuffer : ManagedResource
    {
        protected Bitmap buffer;
        protected Graphics bufferGraphics;

        public Graphics Graphics { get; protected set; }
        public Rectangle ClipRectangle { get; protected set; }

        public DoubleBuffer(Graphics g, Rectangle clipRect)
        {
            if (clipRect.IsEmpty)
            {
                throw new ArgumentException("DoubleBuffer.DoubleBuffer()- clipRect must not be empty");
            }

            buffer = new Bitmap(clipRect.Width, clipRect.Height);
            bufferGraphics = Graphics.FromImage(buffer);

            Graphics = g;
            ClipRectangle = clipRect;
        }     

        protected void Render()
        {
            if (Graphics != null)
            {
                Graphics.DrawImageUnscaled(buffer, ClipRectangle);
            }
        }   

        protected override void FreeManagedResources()
        {
            Render();

            if (bufferGraphics != null)
            {
                bufferGraphics.Dispose();
                bufferGraphics = null;
            }

            if (buffer != null)
            {
                buffer.Dispose();
                buffer = null;
            }           
        }
    }
}
