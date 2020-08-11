using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Models;
using Sweeper.Infrastructure;
using Sweeper.Models.Game;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class SweeperGameModelTests
    {
        private BoardModel PrepareBoardWithMocks(int rows, int cols, int mines, bool playFirstRandomPiece = true)
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settings = new Moq.Mock<ISettingsModel>();
            settings.SetupGet(m => m.Rows).Returns(rows);
            settings.SetupGet(m => m.Columns).Returns(cols);
            settings.SetupGet(m => m.MineCount).Returns(mines);
            var bm = new BoardModel(repo.Object, settings.Object, false);

            if (playFirstRandomPiece)
            {
                Random random = new Random();
                Assert.IsTrue(bm.Play(new GridPoint(random.Next(bm.Rows), random.Next(bm.Columns))));
            }
            return bm;
        }
        [DataRow(10, 10, 10, true)]
        [DataRow(15, 15, 15, true)]
        [DataRow(20, 20, 20, true)]
        [DataTestMethod]

        public void Test_Board_Play_First_Mine_Verify_Played_Then_Play_Second_Mine_Verify_Exception_on_second_play
           (int rows,
            int cols,
            int mines,
            bool verifyByQuery)
        {
            //Arrange get a fully initialized Board with one or more piecePlayed
            BoardModel bm = PrepareBoardWithMocks(rows, cols, mines, true);
            // Find two Mines
            var testItems = bm.Model.Where(m => m.ItemValue == GamePieceModel.PieceValues.MINE).Take(2).ToArray();
            // Play Returns False when you hit a mine
            Assert.IsFalse(bm.Play(testItems[0].GridPoint));
            // Play returns exception on second play 
            try
            {
                Assert.IsFalse(bm.Play(testItems[1].GridPoint));
                Assert.IsTrue(bm.Model.Count(m => m.IsPlayed) == 2);
            }
            catch (InvalidOperationException)
            {
                Assert.IsTrue(true);
                return;
            }
            // We should never get here
            Assert.IsFalse(false);
        }

        [DataRow(10, 10, 10, false)]
        [DataRow(15, 15, 15, false)]
        [DataTestMethod]
        public void Test_Board_Play_First_Mine_Then_Eliminate_Contiguous_Pieces(int rows,
                                                                                int cols,
                                                                                int mines,
                                                                                bool verifyByQuery)
        {
            //Arrange
            BoardModel bm = PrepareBoardWithMocks(rows, cols, mines, true);

            var contiguousPieces = from cp in bm.Model
                                   where cp.ItemValue >= GamePieceModel.PieceValues.ONEMINE &&
                                         cp.ItemValue <= GamePieceModel.PieceValues.EIGHTMINE
                                   select new { cp.GridPoint };



            int tilesToEliminate = contiguousPieces.Count();
            foreach (var gp in contiguousPieces)
            {
                Assert.IsTrue(bm.Play(gp.GridPoint));
            }
            var postTestContiguousPieces = bm.Model.Count(m => m.ShownValue >= GamePieceModel.PieceValues.ONEMINE &&
                                                               m.ShownValue <= GamePieceModel.PieceValues.EIGHTMINE);
            Assert.AreEqual(tilesToEliminate, postTestContiguousPieces);
        }

        [DataRow(10, 10, 10, true)]
        [DataRow(15, 15, 15, true)]
        [DataRow(20, 20, 20, true)]
        [DataTestMethod]
        public void Test_Board_Play_First_Mine_Then_All_Non_Contiguous_Pieces(int rows,
                                                                                int cols,
                                                                                int mines,
                                                                                bool verifyByQuery)
        {
            //Arrange
            BoardModel bm = PrepareBoardWithMocks(rows, cols, mines, playFirstRandomPiece: false);


            foreach (var gp in bm.Model)
            {
                if (!gp.IsPlayed && gp.ItemValue == GamePieceModel.PieceValues.NOMINE)
                    Assert.IsTrue(bm.Play(gp.GridPoint));
            }
        }

        [DataRow(10, 10, 10, true)]
        [DataRow(15, 15, 15, true)]
        [DataRow(20, 20, 20, true)]
        [DataTestMethod]
        public void Test_Play_First_Mine_Then_Toggle_All_Mines(int rows,
                                                               int cols,
                                                               int mines,
                                                               bool verifyByQuery)
        {
            //Arrange
            BoardModel bm = PrepareBoardWithMocks(rows, cols, mines, playFirstRandomPiece: true);


            foreach (var gp in bm.Model)
            {
                if (!gp.IsPlayed && gp.ItemValue == GamePieceModel.PieceValues.MINE)
                    Assert.AreEqual(GamePieceModel.PieceValues.FLAGGED, bm.ToggleFlag(gp.GridPoint));
            }
        }

        [DataRow(10, 10, 10)]
        [DataRow(15, 15, 15)]
        [DataRow(20, 20, 20)]
        [DataTestMethod]
        public void TestInitializations(int rows,
                                        int cols,
                                        int mines)
        {
            BoardModel bm = PrepareBoardWithMocks(rows, cols, mines, true);
            Assert.IsTrue(rows * cols > bm.Model.Count(m => m.IsPlayed == false));
        }

        [DataRow(10, 10, 10, 20, 20, 20)]
        [DataTestMethod]
        public void Test_Resize(int r1, int c1, int m1, int r2, int c2, int m2)
        {
            //Arrange
            BoardModel bm = PrepareBoardWithMocks(r1, c1, m1, playFirstRandomPiece: false);
            var settings = new Moq.Mock<ISettingsModel>();
            settings.SetupGet(m => m.Rows).Returns(r2);
            settings.SetupGet(m => m.Columns).Returns(c2);
            settings.SetupGet(m => m.MineCount).Returns(m2);
            bm.Resize(settings.Object);
            Assert.AreEqual(r2, bm.Rows);
            Assert.AreEqual(c2, bm.Columns);
        }

        [TestMethod]
        public void Test_IndexSetter()
        {
            BoardModel bm = PrepareBoardWithMocks(10, 10, 10, true);
            for (int i = 0; i < bm.Rows; i++)
            {
                for (int j = 0; j < bm.Columns; j++)
                {
                    bm[i, j] = new GamePieceModel(i, j);
                }
            }

        }
        [DataRow(10, 10, 10, 10)]
        [DataRow(15, 15, 15, 15)]
        [DataRow(20, 20, 20, 20)]
        [DataTestMethod]
        public void PlayOutOfBounds(int r, int c, int obr, int obc)
        {
            BoardModel bm = PrepareBoardWithMocks(10, 10, 10, true);

            try
            {
                bm.Play(new GridPoint(obr, obc));

            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }

        [DataRow(true)]
        [DataRow(true, true)]
        [DataTestMethod]
        public void Test_CorrectlyFlagged(bool setFlag, bool setIncorrectly = false)
        {
            BoardModel bm = PrepareBoardWithMocks(10, 10, 10, true);

            if (setFlag)
            {
                var flaggedItems = bm.Model.Where(m => m.ItemValue == GamePieceModel.PieceValues.MINE);
                if (!setIncorrectly)
                {
                    foreach (var p in flaggedItems)
                    {
                        bm[p.GridPoint.R, p.GridPoint.C].ToggleFlag();
                    }
                }
                else
                {


                }
            }
            if (!setIncorrectly)
            {
                Assert.IsTrue(bm.AllCorrectlyFlagged);
            }
            else
            {
                Assert.IsFalse(bm.AllCorrectlyFlagged);
            }
        }

        [TestMethod]
        public void LoadFromRepo()
        {
            Assert.Inconclusive();
        }
    }
}

