using Shell32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace DesktopDestroyer
{
    static class ImageHelper
    {
        private static readonly ImageCodecInfo TIFFCodec;

        private static readonly EncoderParameters ImgTo8bbpEncoderParams;

        private static readonly ShellClass ShellObject;

        static ImageHelper()
        {
            TIFFCodec =
                ImageCodecInfo
                    .GetImageEncoders()
                    .Where(x => x.MimeType == "image/tiff")
                    .First();
            ImgTo8bbpEncoderParams = new EncoderParameters(1);
            var encoder = Encoder.ColorDepth;
            ImgTo8bbpEncoderParams.Param[0] = new EncoderParameter(encoder, 8L);
            ShellObject = new ShellClass();
        }

        public static Bitmap CaptureScreen()
        {
            ShellObject.ToggleDesktop();
            Thread.Sleep(10);
            var screenBmp = new Bitmap(
                (int)SystemParameters.PrimaryScreenWidth,
                (int)SystemParameters.PrimaryScreenHeight,
                PixelFormat.Format32bppArgb);
            using(var bmpGraphics = Graphics.FromImage(screenBmp))
            {
                bmpGraphics.CopyFromScreen(0, 0, 0, 0, screenBmp.Size);
            }
            return screenBmp;
        }

        public static Image To8bpp(Image oldImage)
        {
            var stream = new MemoryStream();
            oldImage.Save(stream, TIFFCodec, ImgTo8bbpEncoderParams);
            stream.Seek(0, SeekOrigin.Begin);
            return Image.FromStream(stream);
        }
    }
}
