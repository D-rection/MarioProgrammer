using System;
using System.Drawing;
using MarioProgrammer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForGame
{
    [TestClass]
    public class TestPlayer
    {
        [TestMethod]
        public void Player()
        {
            var player = new Player(new Point { X = 1, Y = 1 });

            Assert.AreEqual(player.Name, "Player");
            Assert.IsTrue(player.Destructible);
            Assert.IsFalse(player.Permeability);
        }
    }
}
