using System;
using System.Drawing;
using MarioProgrammer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForGame
{
    [TestClass]
    public class TestGameObject
    {
        [TestMethod]
        public void GameObject()
        {
            var gameObject = new GameObject();

            Assert.AreEqual(gameObject.Name, "Name");
            Assert.IsNull(gameObject.Image);
            Assert.IsTrue(gameObject.Destructible);
            Assert.IsTrue(gameObject.Permeability);
        }

        [TestMethod]
        public void TestGrass()
        {
            var grass = new Grass(new Point { X = 1, Y = 1 } );

            Assert.AreEqual(grass.Name, "Grass");
            Assert.IsFalse(grass.Destructible);
            Assert.IsFalse(grass.Permeability);
        }

        [TestMethod]
        public void TestEarth()
        {
            var earth = new Earth(new Point { X = 1, Y = 1 });

            Assert.AreEqual(earth.Name, "Earth");
            Assert.IsFalse(earth.Destructible);
            Assert.IsFalse(earth.Permeability);
        }

        [TestMethod]
        public void TestCloud()
        {
            var cloud = new Cloud(new Point { X = 1, Y = 1 });

            Assert.AreEqual(cloud.Name, "Cloud");
            Assert.IsFalse(cloud.Destructible);
            Assert.IsFalse(cloud.Permeability);
        }

        [TestMethod]
        public void TestBox()
        {
            var box = new Box(new Point { X = 1, Y = 1 });

            Assert.AreEqual(box.Name, "Box");
            Assert.IsTrue(box.Destructible);
            Assert.IsFalse(box.Permeability);
        }

        [TestMethod]
        public void TestEmptyCell()
        {
            var emptyCell = new EmptyCell(new Point { X = 1, Y = 1 });

            Assert.AreEqual(emptyCell.Name, "EmptyCell");
            Assert.IsFalse(emptyCell.Destructible);
            Assert.IsTrue(emptyCell.Permeability);
        }
    }
}
