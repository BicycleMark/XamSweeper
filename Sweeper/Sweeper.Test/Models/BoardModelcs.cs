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
        [TestMethod]
        public void TestBoardConstruction()
        {
            var repo =  new Moq.Mock<IPropertyRepository>();
            repo.SetupAllProperties();
            
            var settings = new Moq.Mock<ISettings>();
            settings.SetupGet(m => m.Rows).Returns(120);
            settings.SetupGet(m => m.Columns).Returns(120);
            BoardModel bm = new BoardModel(repo.Object, settings.Object, false);

            for (int r = 0; r < bm.Rows; r++)
                for (int c = 0; c < bm.Columns; c++)
                {
                    Assert.AreEqual(bm[r, c].ShownValue, GamePieceModel.PieceValues.BUTTON);
                    Assert.AreEqual(bm[r, c].ItemValue, GamePieceModel.PieceValues.NOMINE);
                }
           
        }
    }
}
