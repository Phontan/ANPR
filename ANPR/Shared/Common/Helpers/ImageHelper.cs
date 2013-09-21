using System;
using System.Drawing;
using System.Drawing.Imaging;
using ANPR.Common.Log;

namespace ANPR.Common.Helpers
{
    public static class ImageHelper
    {
        private static readonly ILogger Logger = new NLogger("ImageHelper");

        public static int[,] IntegralImage(Bitmap sourceImage)
        {
            Ensure.NotNull(sourceImage, "sourceImage");
            var brightnessMatrix = new int[sourceImage.Width, sourceImage.Height];

            var data = sourceImage.LockBits(new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                                            ImageLockMode.ReadWrite,
                                            sourceImage.PixelFormat);
            try
            {
                unsafe
                {
                    var ptr = (byte*) data.Scan0;
                    for (var i = 0; i < data.Height; i++)
                    {
                        for (var j = 0; j < data.Width; j++)
                        {
                            brightnessMatrix[i, j] = (byte) ((0.299*ptr[2]) + (0.587*ptr[1]) + (0.114*ptr[0]));
                            ptr += 3;
                        }
                        ptr += data.Stride - data.Width*3;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occur while reading image.");
            }
            finally
            {
                sourceImage.UnlockBits(data);
            }
            return IntegralImage(brightnessMatrix);
        }

        public static int[,] IntegralImage(int[,] sourceImage)
        {
            var width = sourceImage.GetLength(0);
            var height = sourceImage.GetLength(1);
            var result = new int[width, height];
            result[0, 0] = sourceImage[0, 0];
            for (var x = 1; x < width; x++)
            {
                result[x, 0] = sourceImage[x, 0] + result[x - 1, 0];
            }
            for (var y = 1; y < height; y++)
            {
                result[0, y] = sourceImage[0, y] + result[0, y - 1];
            }
            for (var y = 1; y < height; y++)
            {
                for (var x = 1; x < width; x++)
                {
                    result[x, y] = sourceImage[x, y] + result[x - 1, y] + result[x, y - 1] - result[x - 1, y - 1];
                }
            }

            return result;
        }

        public static int SumOfRectangle(int[,] integralImage, int top, int right, int bottom, int left)
        {
            int a = 0, b = 0, c = integralImage[right, bottom], d = 0;
            if (top > 0)
                b = integralImage[right, top - 1];
            if (left > 0)
                d = integralImage[left - 1, bottom];
            if (b != 0 && d != 0)
                a = integralImage[left - 1, top - 1];

            return a + c - b - d;
        }

        public static Bitmap GetGrayscaleImage(Bitmap sourceImage)
        {
            Ensure.NotNull(sourceImage, "sourceImage");

            var image = (Bitmap)sourceImage.Clone();
            var data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                                            ImageLockMode.ReadWrite,
                                            image.PixelFormat);
            try
            {
                unsafe
                {
                    var ptr = (byte*)data.Scan0;
                    for (var i = 0; i < data.Height; i++)
                    {
                        for (var j = 0; j < data.Width; j++)
                        {
                            var y = (byte)((0.299 * ptr[2]) + (0.587 * ptr[1]) + (0.114 * ptr[0]));
                            ptr[0] = ptr[1] = ptr[2] = y;
                            ptr += 3;
                        }
                        ptr += data.Stride - data.Width * 3;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occur while reading image.");
            }
            finally
            {
                image.UnlockBits(data);
            }

            return image;
        }
    }
}
