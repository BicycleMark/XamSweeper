using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweeper.Models;
using Sweeper.Infrastructure;
using Moq;

namespace Sweeper.Test.Models
{
    [TestClass]
    public class BoardModelTest
    {
        [DataRow(10, 10, 10)]
        [DataRow(15, 15, 15)]
        [DataRow(20,20, 20)]
        [DataRow(100, 100, 150)]
        
       

        [DataTestMethod]
       
        public void TestConstruction(int rows, int cols, int mines)
        {
            var repo =  new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            
            var settings = new Moq.Mock<ISettings>();
            settings.SetupGet(m => m.Rows).Returns(rows);
            settings.SetupGet(m => m.Columns).Returns(cols);
            settings.SetupGet(m => m.MineCount).Returns(mines); 
            BoardModel bm = new BoardModel(repo.Object, settings.Object, false);

            for (int r = 0; r < bm.Rows; r++)
                for (int c = 0; c < bm.Columns; c++)
                {
                    Assert.AreEqual(bm[r, c].ShownValue, GamePieceModel.PieceValues.BUTTON);
                    Assert.AreEqual(bm[r, c].ItemValue, GamePieceModel.PieceValues.NOMINE);
                }

            // There is a rule we never let the user fail first time out
           Assert.IsFalse(bm.Play(new GridPoint(rows / 2, cols / 2)));
        }
    }
}
