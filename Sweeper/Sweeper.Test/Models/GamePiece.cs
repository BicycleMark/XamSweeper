using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Models;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class GamePieceTest
    {
        [TestMethod]
        public void TestConstruction()
        {

            GamePieceModel gpm = new GamePieceModel(1,1);
            Assert.AreEqual(GamePieceModel.PieceValues.BUTTON, gpm.ShownValue);
            Assert.AreEqual(GamePieceModel.PieceValues.NOMINE, gpm.ItemValue);
        }

    }
}
