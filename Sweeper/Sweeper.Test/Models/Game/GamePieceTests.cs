using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Models;
using Sweeper.Models.Game;

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

        [DataRow(GamePieceModel.PieceValues.NOMINE, true)]
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

        [DataRow(GamePieceModel.PieceValues.BLANK, true)]
        [DataRow(GamePieceModel.PieceValues.BUTTON, false)]
        [DataRow(GamePieceModel.PieceValues.PRESSED, false)]
        [DataRow(GamePieceModel.PieceValues.FLAGGED, false)]     
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

        
        [DataRow(GamePieceModel.PieceValues.FLAGGED, true)]
        [DataRow(GamePieceModel.PieceValues.BUTTON, true)]
        [DataRow(GamePieceModel.PieceValues.BLANK, false)]
        [DataRow(GamePieceModel.PieceValues.EIGHTMINE, false)]
        [DataRow(GamePieceModel.PieceValues.FIVEMINE, false)]     
        [DataRow(GamePieceModel.PieceValues.FOURMINE, false)]
        [DataRow(GamePieceModel.PieceValues.MINE, false)]
        [DataRow(GamePieceModel.PieceValues.NOMINE, false)]
        [DataRow(GamePieceModel.PieceValues.PRESSED, false)]
        [DataRow(GamePieceModel.PieceValues.SEVENMINE, false)]
        [DataRow(GamePieceModel.PieceValues.SIXMINE, false)]
        [DataRow(GamePieceModel.PieceValues.THREEMINE, false)]
        [DataRow(GamePieceModel.PieceValues.TWOMINE, false)]
        [DataRow(GamePieceModel.PieceValues.WRONGCHOICE, false)]

        [DataTestMethod]
        public void Test_ToggleFlag(GamePieceModel.PieceValues shownValue, bool shouldAllowToggle )
        {
            GamePieceModel gpm = new GamePieceModel(1, 1);

            gpm.ShownValue = shownValue;
           
            gpm.ToggleFlag();
           
            if (shouldAllowToggle)
            {
                Assert.AreNotEqual(gpm.ShownValue, shownValue);
            }else
            {
                Assert.AreEqual(gpm.ShownValue, shownValue);
            }
           
        }

        [TestMethod]
        public void Test_ToggleFlag_With_Out_Of_Range_Values()
        {
            GamePieceModel gpm = new GamePieceModel(1, 1);
            int played = 0;
            Assert.IsFalse(gpm.IsPlayed);
            gpm.PropertyChanged += (s, e) => { if (e.PropertyName == "IsPlayed") ++played; };

            gpm.ShownValue = GamePieceModel.PieceValues.EIGHTMINE;
            gpm.ToggleFlag();
           
            Assert.IsFalse(gpm.IsFlagged);
           
        }
    }
}
