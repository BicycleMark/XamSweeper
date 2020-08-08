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

        [DataRow(GamePieceModel.PieceValues.NOMINE,true)]
        [DataRow(GamePieceModel.PieceValues.ONEMINE, true)]
        [DataRow(GamePieceModel.PieceValues.TWOMINE, true)]
        [DataRow(GamePieceModel.PieceValues.THREEMINE, true)]
        [DataRow(GamePieceModel.PieceValues.FOURMINE, true)]
        [DataRow(GamePieceModel.PieceValues.FIVEMINE, true)]
        [DataRow(GamePieceModel.PieceValues.SIXMINE, true)]
        [DataRow(GamePieceModel.PieceValues.SEVENMINE, true)]
        [DataRow(GamePieceModel.PieceValues.EIGHTMINE, true)]
        [DataRow(GamePieceModel.PieceValues.WRONGCHOICE, true)]
        [DataRow(GamePieceModel.PieceValues.MINE, true)]

        [DataRow(GamePieceModel.PieceValues.BLANK, false)]
        [DataRow(GamePieceModel.PieceValues.BUTTON, false)]
        [DataRow(GamePieceModel.PieceValues.PRESSED, false)]
        [DataRow(GamePieceModel.PieceValues.FLAGGED, true)]       
        [DataTestMethod]
        public void Test_IsPlayed_Returns_Notification_And_Queries_Correct_Value(GamePieceModel.PieceValues pieceValue, bool shouldReturnIsPlayed)
        {
            GamePieceModel gpm = new GamePieceModel(1, 1);
            int played = 0;
            Assert.IsFalse(gpm.IsPlayed);
            gpm.PropertyChanged += (s, e) => { if (e.PropertyName == "IsPlayed") ++played; };
            gpm.ShownValue = pieceValue;
            Assert.AreEqual(shouldReturnIsPlayed ? 1 : 0, played);
            Assert.AreEqual(shouldReturnIsPlayed, gpm.IsPlayed);
        }

        [TestMethod]
        public void Test_ToggleFlag()
        {
            GamePieceModel gpm = new GamePieceModel(1, 1);
            int played = 0;
            Assert.IsFalse(gpm.IsPlayed);
            gpm.PropertyChanged += (s, e) => { if (e.PropertyName == "IsPlayed") ++played; };
            gpm.ToggleFlag();
            Assert.IsTrue(gpm.IsPlayed);
            Assert.IsTrue(gpm.IsFlagged);
            gpm.ToggleFlag();     
            Assert.IsFalse( gpm.IsPlayed);
            Assert.IsFalse(gpm.IsFlagged);
        }
    }
}
