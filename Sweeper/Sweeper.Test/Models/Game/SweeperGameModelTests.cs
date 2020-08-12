using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Models;
using Sweeper.Infrastructure;
using Sweeper.Models.Game;
using System.Threading;
using Moq;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class SweeperGameModelTests
    {
        SweeperGameModel _model;
        private SweeperGameModel PrepareBoardWithMocks(int rows, int cols, int mines, bool playFirstRandomPiece = true, bool eliminateTimer=false)
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settings = new Moq.Mock<ISettingsModel>();
            settings.SetupGet(m => m.Rows).Returns(rows);
            settings.SetupGet(m => m.Columns).Returns(cols);
            settings.SetupGet(m => m.MineCount).Returns(mines);
            settings.SetupGet(m => m.DisableTimerUpdatesForTesting).Returns(eliminateTimer);
            var bm = new SweeperGameModel(repo.Object, settings.Object, loadFromRepo: false);

            if (playFirstRandomPiece)
            {
                Random random = new Random();
                Assert.IsTrue(bm.Play(new GridPoint(random.Next(bm.Rows), random.Next(bm.Columns))));
            }
            return bm;
        }

        //[TestInitialize]
        //public void Setup()
        //{
        //    var repo = new Moq.Mock<IPropertyRepository>();
        //    repo.SetupAllProperties();
        //    var settings = new Moq.Mock<ISettingsModel>();
        //    settings.SetupGet(m => m.Rows).Returns(rows);
        //    settings.SetupGet(m => m.Columns).Returns(cols);
        //    settings.SetupGet(m => m.MineCount).Returns(mines);
        //    _model = new SweeperGameModel(repo.Object, settingsModel.Object,false);
        //}

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
            var bm = PrepareBoardWithMocks(rows, cols, mines, true);
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
            var bm = PrepareBoardWithMocks(rows, cols, mines, true);

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
            var bm = PrepareBoardWithMocks(rows, cols, mines, playFirstRandomPiece: false);


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
            var bm = PrepareBoardWithMocks(rows, cols, mines, playFirstRandomPiece: true);


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
            var bm = PrepareBoardWithMocks(rows, cols, mines, true);
            Assert.IsTrue(rows * cols > bm.Model.Count(m => m.IsPlayed == false));
        }

        [DataRow(10, 10, 10, 20, 20, 20)]
        [DataTestMethod]
        public void Test_Resize(int r1, int c1, int m1, int r2, int c2, int m2)
        {
            //Arrange
            var bm = PrepareBoardWithMocks(r1, c1, m1, playFirstRandomPiece: false);
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
            var bm = PrepareBoardWithMocks(10, 10, 10, true);
            for (int i = 0; i < bm.Rows; i++)
            {
                for (int j = 0; j < bm.Columns; j++)
                {
                    bm[i, j] = new GamePieceModel(i, j);
                }
            }

        }
        [DataRow(10, 10, 10, 10, 10)]
        [DataRow(15, 15, 15, 15, 15)]
        [DataRow(20, 20, 20, 20, 20)]
        [DataTestMethod]
        public void PlayOutOfBounds(int r, int c, int m, int obr, int obc)
        {
            var bm = PrepareBoardWithMocks(r, c, m);
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
            var bm = PrepareBoardWithMocks(10, 10, 10, true);

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

        // GameModel Tests here
        [TestMethod]
        public void TestConstruction()
        {
            var _model = PrepareBoardWithMocks(10, 10, 10, false);
            Assert.AreEqual(0, _model.GameTime);
            Assert.AreEqual(GameStates.NOT_STARTED,_model.GameState);
            var p = _model.Model.First(m => m.ShownValue == GamePieceModel.PieceValues.BUTTON).GridPoint;
            _model.Play(p.R, p.C);
            Assert.IsTrue(_model.GameTime > 0);
            Assert.AreEqual(GameStates.IN_PLAY, _model.GameState);
            Assert.AreEqual(10, _model.RemainingMines);
            Assert.IsNotNull(_model.Repo);
        }

        [TestMethod]
        public void Test_Lose()
        {
            var _model = PrepareBoardWithMocks(10, 10, 10, true);
           
            Assert.AreEqual(GameStates.IN_PLAY, _model.GameState);
            var p = _model.Model.First(m => m.ItemValue == GamePieceModel.PieceValues.MINE).GridPoint;
            _model.Play(p.R, p.C);
            Assert.IsTrue(_model.GameTime > 0);
            Assert.AreEqual(GameStates.LOST, _model.GameState);
            //Assert.AreEqual(10, _model.RemainingMines);
            Assert.IsNotNull(_model.Repo);
        }

        [TestMethod]
        public void Test_GetBoardProp()
        {
            var _model = PrepareBoardWithMocks(10, 10, 10, true);

            Assert.AreEqual(GameStates.IN_PLAY, _model.GameState);
            Assert.IsNotNull(_model.Board);
            int i = _model.MineCount;
            Assert.IsTrue(i > 0);
         
        }

        [TestMethod]
        public void TestDispose()
        {
            var _model = PrepareBoardWithMocks(10, 10, 10, true);
            Assert.IsFalse(_model.Disposed);
            _model.Dispose();
            Assert.IsTrue(_model.Disposed);
            _model.Dispose();
            Assert.IsTrue(_model.Disposed);
        }



        [DataRow(true)]
        [DataRow(false)]
        [DataTestMethod()]
        public void Test_Game_Lost(bool testLose)
        {
            var _model = PrepareBoardWithMocks(10, 10, 10,playFirstRandomPiece: true);
            Assert.AreEqual(GameStates.IN_PLAY, _model.GameState);

            if (testLose)
            {
                var p = _model.Model.FirstOrDefault(m => m.ItemValue == GamePieceModel.PieceValues.MINE).GridPoint;
                _model.Play(p);
                Assert.AreEqual(GameStates.LOST, _model.GameState);
            }
            else
            {   
                var flagList = _model.Model.Where(m => m.ItemValue == GamePieceModel.PieceValues.MINE);
                foreach (var p in flagList)
                {
                    _model.ToggleFlag(p.GridPoint.R, p.GridPoint.C);
                }
                Assert.AreEqual(GameStates.WON, _model.GameState);
            }
        }

        [TestMethod]
        public void Test_Game_From_Initialized_To_INPLAY_And_Check_Timer_Started()
        {
            var _model = PrepareBoardWithMocks(10, 10, 10, playFirstRandomPiece: false);
            int secondsInPlay = 0;
            Assert.AreEqual(GameStates.NOT_STARTED, _model.GameState);
            Assert.AreEqual(0, _model.GameTime);
            _model.GameState = GameStates.IN_PLAY;
            secondsInPlay = _model.GameTime;
            Assert.IsTrue(secondsInPlay >= 1);
            _model.GameState = GameStates.LOST;
            Thread.Sleep(2000);
            Assert.AreEqual(secondsInPlay, _model.GameTime);
        }

        [DataRow(GameStates.LOST, true)]
        [DataRow(GameStates.WON, true)]
        [DataRow(GameStates.IN_PLAY, false)]
        [DataRow(GameStates.NOT_STARTED, false)]
        [DataTestMethod]
        public void Test_Play_In_InvalidState_That_Exception_Is_Thrown_When_Intended(GameStates gameState, bool shouldThrow)
        {
            var _model = PrepareBoardWithMocks(10, 10, 10, playFirstRandomPiece: false);
            _model.GameState = gameState;
            bool exceptionThrew = false;
            try
            {
                _model.Play(1, 1);
            }
            catch (InvalidOperationException)
            {

                exceptionThrew = true;
            }

            if (shouldThrow)
            {
                Assert.IsTrue(exceptionThrew);
            }
            else
            {
                Assert.IsFalse(exceptionThrew);
            }
        }

        [DataRow(999, true)]
        [DataRow(100, false)]
        [DataTestMethod]
        public void Test_Time_Out(int initialGameTime, bool shouldFail)
        {
            var _model = PrepareBoardWithMocks(10, 10, 10, playFirstRandomPiece: false,  true);


            _model.GameTime = initialGameTime;
            Assert.AreEqual(GameStates.NOT_STARTED, _model.GameState);
            if (initialGameTime > 0)
            {
                _model.GameState = GameStates.IN_PLAY;
            }

            var playVal = _model.Play(1, 1);
            if (shouldFail)
                Assert.AreEqual(GameStates.LOST, playVal);
            else
                Assert.AreEqual(GameStates.IN_PLAY, playVal);
        }



    }
}

