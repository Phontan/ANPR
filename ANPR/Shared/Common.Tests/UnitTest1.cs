using System.Diagnostics;
using System.Drawing;
using ANPR.Common.Helpers;
using ANPR.Common.Log;
using NUnit.Framework;

namespace ANPR.Common.Tests
{
    [TestFixture]
    public class unit_test1
    {
        private static readonly ILogger Logger = new ConsoleLogger();

        [Test]
        public void test_method()
        {
            var img = (Bitmap) System.Drawing.Image.FromFile("1.jpg");
            var sw = new Stopwatch();
            sw.Start();
            var grayscaleImg = ImageHelper.GetGrayscaleImage(img);
            Logger.Debug("Time1: {0}", sw.ElapsedMilliseconds);
            grayscaleImg.Save("Grayscale.jpg");
        }
    }
}