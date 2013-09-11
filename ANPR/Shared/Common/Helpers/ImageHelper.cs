namespace ANPR.Common.Helpers
{
    public static class ImageHelper
    {
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
    }
}
