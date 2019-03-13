using FileHelperLibrary.Entities;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace FileHelperLibrary
{
    public class ImageHelper: FileHelper
    {
        private static ImageHelper _instance { get; set; }

        private ImageHelper()
        {
        }

        public static ImageHelper GetInstance()
        {
            if (_instance == null) _instance = new ImageHelper();

            return _instance;
        }

        public ImageInfo Read(string path, int width, int height)
        {
            return ProcessReading(path, width, height);
        }

        private ImageInfo ProcessReading(string path, int width, int height)
        {
            if (System.IO.File.Exists(path))
            {
                var extension = Path.GetExtension(path).Substring(1);
                var image = ResizeImage(path, width, height);
                var ms = new MemoryStream();

                image.Save(ms, GetImageFormat(extension));
                ms.Position = 0;

                return new ImageInfo
                {
                    Stream = ms,
                    Extension = Path.GetExtension(path),
                    Size = ms.Length,
                    Path = path,
                    Name = Path.GetFileName(path)
                };
            }

            return null;
        }

        private static ImageFormat GetImageFormat(string extension)
        {
            var imageFormat = ImageFormat.Jpeg;

            switch (extension)
            {
                case "jpg":
                case "jpeg":
                    imageFormat = ImageFormat.Jpeg;
                    break;

                case "png":
                    imageFormat = ImageFormat.Png;
                    break;

                case "gif":
                    imageFormat = ImageFormat.Gif;
                    break;

                case "icon":
                    imageFormat = ImageFormat.Icon;
                    break;
            }

            return imageFormat;
        }

        private Image ResizeImage(string path, int width, int height)
        {
            var size = new Size(width, height);
            var imageResized = Resize(System.Drawing.Image.FromFile(path), size);
            return imageResized;
        }

        private Image Resize(Image image, Size size)
        {
            var sourceWidth = image.Width;
            var sourceHeight = image.Height;

            float nPercent;

            var nPercentW = (size.Width / (float)sourceWidth);
            var nPercentH = (size.Height / (float)sourceHeight);

            if (nPercentH > nPercentW)
            {
                nPercent = nPercentH;
            }
            else
            {
                nPercent = nPercentW;
            }

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var b = new Bitmap(destWidth, destHeight);
            using (var g = Graphics.FromImage(b))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, destWidth, destHeight);
            }

            return b;
        }
    }
}
