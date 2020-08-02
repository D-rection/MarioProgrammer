using System;
using System.Drawing;
using MarioProgrammer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForGame
{
    [TestClass]
    public class TestMonsters
    {
        [TestMethod]
        public void Monsters()
        {
            var monster1 = new Monster("Samara", new Point { X = 1, Y = 1 });

            Assert.AreEqual(monster1.Name, "Samara");
            Assert.IsTrue(monster1.Destructible);
            Assert.IsFalse(monster1.Permeability);

            var monster2 = new Monster("Assassin", new Point { X = 1, Y = 1 });

            Assert.AreEqual(monster2.Name, "Assassin");
            Assert.IsTrue(monster2.Destructible);
            Assert.IsFalse(monster2.Permeability);
        }
    }
}
