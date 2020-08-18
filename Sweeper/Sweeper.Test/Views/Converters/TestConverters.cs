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
    }
}
