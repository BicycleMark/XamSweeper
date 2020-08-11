using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sweeper.Infrastructure;
using Sweeper.Models;
using Sweeper.Models.Game;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class GameModelTest
    {
        GameModel _model;
        [TestInitialize]
        public void Setup()
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settingsModel = new Moq.Mock<ISettingsModel>();

            var boardModel = new Moq.Mock<IBoardModel>();
            _model = new GameModel(repo.Object, settingsModel.Object, boardModel.Object);
           
        }
    
        [TestMethod]
        public void  TestConstruction()
        {
            Assert.AreEqual(0, _model.GameTime);     
            Assert.AreEqual(0,_model.GameTime);
        }

        [DataRow(false)]
        [DataRow(true)]
        [DataTestMethod]
        public void TestPlay_WithMocked_Board(bool playReturnValue)
        {
             var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settingsModel = new Moq.Mock<ISettingsModel>();
            var boardModel = new Moq.Mock<IBoardModel>();
           
            boardModel.Setup(m => m.Play(It.IsAny<GridPoint>())).Returns(playReturnValue);
            _model = new GameModel(repo.Object, settingsModel.Object, boardModel.Object);
           
            Assert.AreEqual(GameStates.NOT_STARTED, _model.GameState);
            Assert.AreEqual(0, _model.GameTime);
            _model.Play(1, 1);
            if (playReturnValue)
            {
                Assert.IsTrue(_model.GameTime > 0);
                Assert.AreEqual(GameStates.IN_PLAY, _model.GameState);
            }else
            {
                Assert.IsTrue(_model.GameTime == 0);
                Assert.AreEqual(GameStates.LOST, _model.GameState);

            }
        }

        [TestMethod]
        public void TestDispose()
        {
            Assert.IsFalse(_model.Disposed);
            _model.Dispose();
            Assert.IsTrue(_model.Disposed);
            _model.Dispose();
            Assert.IsTrue(_model.Disposed);
        }

        [TestMethod]
        public void Test_Game_From_Initialized_To_INPLAY_And_Check_Timer_Started()
        {
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

        [DataRow(GameStates.LOST,true)]
        [DataRow(GameStates.WON,true)]
        [DataRow(GameStates.IN_PLAY, false)]
        [DataRow(GameStates.NOT_STARTED, false)]
        [DataTestMethod]
        public void Test_Play_In_InvalidState_That_Exception_Is_Thrown_When_Intended(GameStates gameState, bool shouldThrow)
        {
            _model.GameState = gameState;
            bool exceptionThrew = false;
            try
            {
                _model.Play(1, 1);
            }catch(InvalidOperationException )
            {
                
                exceptionThrew = true;
            }

            if (shouldThrow)
            {
                Assert.IsTrue(exceptionThrew);
            }else
            {
                Assert.IsFalse(exceptionThrew);
            }
        }
       
        [DataRow(999,true)]
        [DataRow(100, false)]
        [DataTestMethod]
        public void Test_Time_Out(int initialGameTime, bool shouldFail)
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settingsModel = new Moq.Mock<ISettingsModel>();
            settingsModel.SetupGet(m => m.DisableTimerUpdatesForTesting).Returns(true);

            var boardModel = new Moq.Mock<IBoardModel>();

            boardModel.Setup(m => m.Play(It.IsAny<GridPoint>())).Returns(true);
            _model = new GameModel(repo.Object, settingsModel.Object, boardModel.Object);
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

        [DataRow(GameStates.IN_PLAY,GameStates.WON, true,false)]
        [DataRow(GameStates.WON, GameStates.WON, false,true)]
        [DataTestMethod]
        public void Test_Game_State_Transitions(GameStates initialState, GameStates transitionsState, bool playReturnValue, bool expectException)
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settingsModel = new Moq.Mock<ISettingsModel>();
            var boardModel = new Moq.Mock<IBoardModel>();

            boardModel.Setup(m => m.Play(It.IsAny<GridPoint>())).Returns(playReturnValue);
            boardModel.SetupGet(m => m.AllCorrectlyFlagged).Returns(true);
            _model = new GameModel(repo.Object, settingsModel.Object, boardModel.Object);
            _model.GameState = initialState;
            GameStates playVal;
            try
            {
                playVal = _model.Play(1, 1);
                Assert.AreEqual(transitionsState, playVal);
            }
            catch(InvalidOperationException)
            {
                if (expectException)
                {
                    Assert.IsTrue(expectException);
                    Assert.AreEqual(transitionsState, _model.GameState);
                }

            }  
        }

        [TestMethod]
        public void Test_ToggleFlag()
        {
            var repo = new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            var settingsModel = new Moq.Mock<ISettingsModel>();
            settingsModel.SetupGet(m => m.DisableTimerUpdatesForTesting).Returns(true);

            var boardModel = new Moq.Mock<IBoardModel>();

            boardModel.Setup(m => m.Play(It.IsAny<GridPoint>())).Returns(true);
            _model = new GameModel(repo.Object, settingsModel.Object, boardModel.Object);
            _model.Play(3, 3);

           
            _model.ToggleFlag(2, 1);
            
        }
    }



}
