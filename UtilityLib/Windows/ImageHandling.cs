using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.Logger;
using System.Drawing.Imaging;

namespace Libraries.UtilityLib.Windows
{
    public static class ImageHandling
    {
        public static Image ScaleImage(Image image, Size size)
        {
            return ScaleImage(image, size.Width, size.Height);
        }

        public static Image ScaleImage(Image image, int width, int height)
        {
            double aspectRatio = image.Width / image.Height;
            double boxRatio = width / height;

            double scaleFactor = 0.0;
            //Use height, since that is the most restrictive dimension of box. 
            if (boxRatio > aspectRatio)
            {
                scaleFactor = (double) height / (double) image.Height;
            }
            else
            {
                scaleFactor = (double) width / (double) image.Width;
            }

            int newWidth = (int) (image.Width * scaleFactor);
            int newHeight = (int) (image.Height * scaleFactor);

            Image newImage = null;
            using (Bitmap buffer = new Bitmap((int) newWidth, newHeight))            
            {
                using (Graphics g = Graphics.FromImage(buffer))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode  = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;  
                    g.DrawImage(image, 0, 0, newWidth, newHeight);
                }
                  
                try
                {
                   newImage = buffer.Clone() as Image;
                }
                catch(Exception e)
                {         
                    LocalStaticLogger.WriteLine(e.ToString());
                }            
            }

            return newImage;
        }

        public static Image Scale(this Image image, int width, int height)
        {
            return ScaleImage(image, width, height);
        }

        public static Bitmap ConvertImageToGrayScale(Image original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            using (Graphics g = Graphics.FromImage(newBitmap))
            {

                //create the grayscale ColorMatrix
                ColorMatrix colorMatrix = new ColorMatrix(
                  new float[][] 
                  {
                     new float[] {.3f, .3f, .3f, 0, 0},
                     new float[] {.59f, .59f, .59f, 0, 0},
                     new float[] {.11f, .11f, .11f, 0, 0},
                     new float[] {0, 0, 0, 1, 0},
                     new float[] {0, 0, 0, 0, 1}
                  }
                );

                //create some image attributes
                ImageAttributes attributes = new ImageAttributes();

                //set the color matrix attribute
                attributes.SetColorMatrix(colorMatrix);

                //draw the original image on the new image
                //using the grayscale color matrix
                g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                  0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);                            
            }
            return newBitmap;
        }

        public static Image ConvertToGrayScale(this Image image)
        {
            return ConvertImageToGrayScale(image);
        }
    }
}
