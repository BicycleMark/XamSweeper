using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Views.Converters;

namespace Sweeper.Test.Views.Converters
{

    [TestClass]
    public class TestConverters
    {
        [DataRow(100, 2, 4)]
        [DataTestMethod]
        public void Test_Piece_Sizing(double frameSize, int separatorSize, int numItems)
        {
            var sizeConverter = new SizeConverter();
            object[] values = new object[] { frameSize, separatorSize, numItems };
            Assert.AreNotEqual(0,sizeConverter.Convert(values, typeof(int), null, System.Globalization.CultureInfo.CurrentCulture));
        }

        [DataRow(100, 2)]
        [DataTestMethod]
        public void Test_Coordinate_Converter(int x, int y)
        {
            var coordinateConverter = new CoordinateConverter();
            object[] values = new object[] { x, y };
            Assert.AreNotEqual(string.Empty, coordinateConverter.Convert(values, typeof(int), null, System.Globalization.CultureInfo.CurrentCulture));
        }
    }
}
