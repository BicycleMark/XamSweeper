using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Models;
using System.Diagnostics;
using Sweeper.Infrastructure;
using System.Threading;
using System.Diagnostics;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class BoardModelTest 
    {
        private BoardModel PrepareBoardWithMocks(int rows, int cols, int mines, bool playFirstRandomPiece = true)
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settings = new Moq.Mock<ISettings>();
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
            catch(InvalidOperationException)
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
        public void Test_Board_Play_First_Mine_Then_All_Non_Contiguous_Pieces(  int rows,
                                                                                int cols,
                                                                                int mines,
                                                                                bool verifyByQuery)
        {
            //Arrange
            BoardModel bm = PrepareBoardWithMocks(rows, cols, mines, playFirstRandomPiece: false);
           
           
            foreach (var gp in bm.Model )
            {
                if (!gp.IsPlayed && gp.ItemValue == GamePieceModel.PieceValues.NOMINE)
                   Assert.IsTrue( bm.Play(gp.GridPoint));
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
            Assert.IsTrue(rows * cols> bm.Model.Count(m => m.IsPlayed == false));
        }      
    }
}
