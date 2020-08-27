using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Infrastructure;
using Sweeper.Views.Converters;
using System;
using Xamarin.Forms;

namespace Sweeper.Test.Views.Converters
{

    [TestClass]
    public class TestConverters
    {
        [DataRow(100, 2, 4)]
        [DataTestMethod]
        public void Test_Piece_Sizing(double frameSize, int separatorSize, int numItems)
        {
            var cvt = new SizeConverter();
            object[] values = new object[] { frameSize, separatorSize, numItems };
            Assert.AreNotEqual(0, cvt.Convert(values, typeof(int), null, System.Globalization.CultureInfo.CurrentCulture));
            try
            {
                cvt.ConvertBack(null, null, null, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (NotImplementedException)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }

        [DataRow(100, 2)]
        [DataTestMethod]
        public void Test_Coordinate_Converter(int x, int y)
        {
            var cvt = new CoordinateConverter();
            object[] values = new object[] { x, y };
            Assert.AreNotEqual(string.Empty, cvt.Convert(values, typeof(int), null, System.Globalization.CultureInfo.CurrentCulture));
            try
            {
                cvt.ConvertBack(null, null, null, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (NotImplementedException)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }

        
        [TestMethod]
        public void TestPieceValues()
        {
            var cvt = new PieceValueToImageConverter();
            Assert.IsNull(cvt.Convert(PieceValues.NOMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNull(cvt.Convert(PieceValues.BLANK, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.BUTTON, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.EIGHTMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.FIVEMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.FLAGGED, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.FOURMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.MINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.ONEMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.PRESSED, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.SEVENMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.SIXMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.THREEMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.TWOMINE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(PieceValues.WRONGCHOICE, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));

            try 
            { 
                cvt.ConvertBack(null,typeof(PieceValues),null, System.Globalization.CultureInfo.CurrentCulture);
            }catch(NotImplementedException)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);

        }

        [TestMethod]
        public void TestGameStateValues()
        {
            var cvt = new FaceValueToImageConverter();
           
            Assert.IsNotNull(cvt.Convert(GameStates.IN_EXTENDED_PLAY, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(GameStates.IN_PLAY, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(GameStates.LOST, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(GameStates.NOT_STARTED, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
            Assert.IsNotNull(cvt.Convert(GameStates.WON, typeof(ImageSource), null, System.Globalization.CultureInfo.CurrentCulture));
          


            try
            {
                cvt.ConvertBack(null, typeof(GameStates), null, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (NotImplementedException)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);

        }


    }
}
