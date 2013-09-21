using System;
using System.Drawing;
using ANPR.Common.Helpers;

namespace ANPR.Common
{
    public class Image
    {
        private readonly Bitmap _sourceImage;
        private readonly Lazy<int[,]> _integralImageLazy;
        private readonly Lazy<Bitmap> _grayscaleImageLazy;

        public int[,] IntegralImage
        {
            get { return _integralImageLazy.Value; }
        }

        public Bitmap GrayscaleImage
        {
            get { return _grayscaleImageLazy.Value; }
        }

        public Image(Bitmap sourceImage)
        {
            _sourceImage = sourceImage;
            _integralImageLazy = new Lazy<int[,]>(() => ImageHelper.IntegralImage(_sourceImage));
            _grayscaleImageLazy = new Lazy<Bitmap>(() => ImageHelper.GetGrayscaleImage(_sourceImage));
        }
    }
}