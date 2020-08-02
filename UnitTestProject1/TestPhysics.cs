using System;
using System.Drawing;
using System.Windows.Forms;
using MarioProgrammer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForGame
{
    [TestClass]
    public class TestPhysics
    {
        string[] StringMap = new string[] {
                "000000000000000000000",
                "00C00C000000000000000",
                "0C0000C00000000000000",
                "000000000000000000000",
                "000000000000000000000",
                "0000HH000000000000000",
                "000000000000000000000",
                "000000000000000000000",
                "00P0B0000VV0000000000",
                "000BBBA0000000S000000",
                "GGGGGGGGGGGGGGGGGGGGG",
                "EEEEEEEEEEEEEEEEEEEEE"
        };

        [TestMethod]
        public void Physics()
        {
            var map = new GameMap(StringMap);
            var physics = new Physics(map);
            Assert.IsNotNull(physics);
        }

        [TestMethod]
        public void TestHeroStandUp()
        {
            var physics = new Physics(new GameMap(StringMap));
            physics.HeroStandUp();
            Assert.IsFalse(physics.IsJump);
            Assert.IsFalse(physics.MovingUp);
        }


        [TestMethod]
        public void TestMovePlatform()
        {
            var physics = new Physics(new GameMap(StringMap));
            var previousPositions1 = new Point[] { new Point { X = 4, Y = 5 }, new Point { X = 5, Y = 5 } };
            var previousPositions2 = new Point[] { new Point { X = 9, Y = 8 }, new Point { X = 10, Y = 8 } };
            physics.MovePlatform();
            Assert.IsTrue(physics.GameMap[previousPositions1[0].X, previousPositions1[0].Y].Name == "EmptyCell");
            Assert.IsTrue(physics.GameMap[previousPositions1[1].X, previousPositions1[1].Y].Name == "MovingPlatform");
            Assert.IsTrue(physics.GameMap[previousPositions1[1].X + 1, previousPositions1[1].Y].Name == "MovingPlatform");
            Assert.IsTrue(physics.GameMap[previousPositions1[1].X + 2, previousPositions1[1].Y].Name == "EmptyCell");
            Assert.IsTrue(physics.GameMap[previousPositions2[0].X, previousPositions2[0].Y].Name == "EmptyCell");
            Assert.IsTrue(physics.GameMap[previousPositions2[0].X, previousPositions2[0].Y - 1].Name == "MovingPlatform");
            Assert.IsTrue(physics.GameMap[previousPositions2[1].X, previousPositions2[1].Y].Name == "EmptyCell");
            Assert.IsTrue(physics.GameMap[previousPositions2[1].X, previousPositions2[1].Y - 1].Name == "MovingPlatform");
        }

        [TestMethod]
        public void MoveDown()
        {
            var physics = new Physics(new GameMap(StringMap));
            Assert.AreEqual(physics.GameMap.HeroPosition, new Point() { X = 2, Y = 8 });
            physics.GameMap.MoveDown(physics.GameMap.HeroData);
            Assert.AreEqual(physics.GameMap.HeroPosition, new Point() { X = 2, Y = 9 });
        }

        [TestMethod]
        public void MoveHero()
        {
            var physics = new Physics(new GameMap(StringMap));
            physics.GameMap.MoveDown(physics.GameMap.HeroData);
            physics.MoveHero(Keys.Up);
            Assert.IsTrue(physics.IsJump);
            Assert.IsTrue(physics.MovingUp);
        }
    }
}