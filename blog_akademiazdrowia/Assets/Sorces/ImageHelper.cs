using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace blog_akademiazdrowia.Assets.Sorces
{
    public static class ImageHelper
    {
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            return bmpImage.Clone(cropArea, bmpImage.PixelFormat);
        }

        public static void PrepareImage(string path, string layoutType)
        {
            int imgWidth;
            int imgHeight;
            double proportion;

            using (Image img = Image.FromFile(path))
            {
                imgWidth = img.Width;
                imgHeight = img.Height;
                proportion = 1;

                switch (layoutType)
                {
                    case "s-picture":
                        proportion = 0.5;
                        break;
                    case "l-picture":
                        proportion = 1;
                        break;
                    case "xxl-picture":
                        proportion = 1.5;
                        break;
                }

                Image targetImg = ImageHelper.resizeImage(img, new Size(400, (int)((400 / (float)imgWidth) * imgHeight)));
                img.Dispose();
                targetImg.Save(path);
                imgWidth = targetImg.Width;
                imgHeight = targetImg.Height;
                targetImg.Dispose();
            }

            using (Image img = Image.FromFile(path))
            {
                Point point = SetPointOfCut(imgWidth, imgHeight, proportion);
                Size size = SetCropSize(imgWidth, imgHeight, proportion);
                Image targetImg = ImageHelper.cropImage(img,new Rectangle(point,size));
                img.Dispose();
                targetImg.Save(path);
            }
        }

        public static Size SetCropSize(int imgWidth, int imgHeight, double proportion)
        {
            int cropH = 0;
            int cropW = 0;

            if (imgHeight > imgWidth * proportion)
            {
                cropW = imgWidth;
                cropH = (int)(imgWidth * proportion);
            }
            else if (imgHeight < imgWidth * proportion)
            {
                cropH = imgHeight;
                cropW = (int)(imgHeight / proportion);
            }
            else
            {
                cropW = imgWidth;
                cropH = imgHeight;
            }
            return new Size(cropW, cropH);
        }

        public static Point SetPointOfCut(int imgWidth, int imgHeight, double proportion)
        {
            Point pointOfCut = new Point();
            pointOfCut.Y = 0;
            pointOfCut.X = 0;

            if (imgHeight > imgWidth * proportion)
            {
                pointOfCut.Y = (imgHeight / 2) - ((int)(imgWidth * proportion) / 2);
            }
            else if (imgHeight < imgWidth * proportion)
            {
                pointOfCut.X = (imgWidth / 2) - ((int)(imgHeight / proportion) / 2);
            }

            return pointOfCut;
        }
    }
}