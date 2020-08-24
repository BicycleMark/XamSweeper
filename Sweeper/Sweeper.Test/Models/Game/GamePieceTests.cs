using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Infrastructure;
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
            Assert.AreEqual(PieceValues.BUTTON, gpm.ShownValue);
            Assert.AreEqual(PieceValues.NOMINE, gpm.ItemValue);
        }

        [DataRow(PieceValues.NOMINE, true)]
        [DataRow(PieceValues.ONEMINE, true)]
        [DataRow(PieceValues.TWOMINE, true)]
        [DataRow(PieceValues.THREEMINE, true)]
        [DataRow(PieceValues.FOURMINE, true)]
        [DataRow(PieceValues.FIVEMINE, true)]
        [DataRow(PieceValues.SIXMINE, true)]
        [DataRow(PieceValues.SEVENMINE, true)]
        [DataRow(PieceValues.EIGHTMINE, true)]
        [DataRow(PieceValues.WRONGCHOICE, true)]
        [DataRow(PieceValues.MINE, true)]

        [DataRow(PieceValues.BLANK, true)]
        [DataRow(PieceValues.BUTTON, false)]
        [DataRow(PieceValues.PRESSED, false)]
        [DataRow(PieceValues.FLAGGED, false)]     
        [DataTestMethod]
        public void Test_IsPlayed_Returns_Notification_And_Queries_Correct_Value(PieceValues pieceValue, bool shouldReturnIsPlayed)
        {
            GamePieceModel gpm = new GamePieceModel(1, 1);
            int played = 0;
            Assert.IsFalse(gpm.IsPlayed);
            gpm.PropertyChanged += (s, e) => { if (e.PropertyName == "IsPlayed") ++played; };
            gpm.ShownValue = pieceValue;
            Assert.AreEqual(shouldReturnIsPlayed ? 1 : 0, played);
            Assert.AreEqual(shouldReturnIsPlayed, gpm.IsPlayed);
        }

        
        [DataRow(PieceValues.FLAGGED, true)]
        [DataRow(PieceValues.BUTTON, true)]
        [DataRow(PieceValues.BLANK, false)]
        [DataRow(PieceValues.EIGHTMINE, false)]
        [DataRow(PieceValues.FIVEMINE, false)]     
        [DataRow(PieceValues.FOURMINE, false)]
        [DataRow(PieceValues.MINE, false)]
        [DataRow(PieceValues.NOMINE, false)]
        [DataRow(PieceValues.PRESSED, false)]
        [DataRow(PieceValues.SEVENMINE, false)]
        [DataRow(PieceValues.SIXMINE, false)]
        [DataRow(PieceValues.THREEMINE, false)]
        [DataRow(PieceValues.TWOMINE, false)]
        [DataRow(PieceValues.WRONGCHOICE, false)]

        [DataTestMethod]
        public void Test_ToggleFlag(PieceValues shownValue, bool shouldAllowToggle )
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

            gpm.ShownValue = PieceValues.EIGHTMINE;
            gpm.ToggleFlag();
           
            Assert.IsFalse(gpm.IsFlagged);
           
        }


        [DataRow(10,10,10)]
       

        [DataTestMethod]
        public void Test_GridPoints(int r, int c, int m)
        {

           for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    var gpm = new GamePieceModel(i, j);
                    Assert.AreEqual(i, gpm.GridPoint.R);
                    Assert.AreEqual(j, gpm.GridPoint.C);
                }

            }

        }
    }
}
