using System;
using System.Drawing;
using System.Windows.Forms;
using MarioProgrammer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForGame
{
    [TestClass]
    public class TestMovingPlatform
    {
        [TestMethod]
        public void MovingPlatform()
        {
            var horizontalPlatforms = new MovingPlatform(false, 2, new Point { X = 1, Y = 1 });
            var verticalPlatforms = new MovingPlatform(true, 2, new Point { X = 1, Y = 1 });
            Assert.IsNotNull(horizontalPlatforms);
            Assert.IsNotNull(verticalPlatforms);
        }
    }
}
