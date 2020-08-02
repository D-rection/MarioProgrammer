using System;
using System.Drawing;
using System.Windows.Forms;
using MarioProgrammer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForGame
{
    [TestClass]
    public class TestGameMap
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
                "0000B0000VV0000000000",
                "00PBBBA0000000S000000",
                "GGGGGGGGGGGGGGGGGGGGG",
                "EEEEEEEEEEEEEEEEEEEEE"
        };

        string[] StringMap1 = new string[] {
                "000000000000000000000",
                "00C00C000000000000000",
                "0C0000C00000000000000",
                "000000000000000000000",
                "000000000000000000000",
                "0000HH000000000000000",
                "000000000000000000000",
                "000000000000000000000",
                "00P0B0000VVV000000000",
                "000BBBA0000000S000000",
                "GGGGGGGGGGGGGGGGGGGGG",
                "EEEEEEEEEEEEEEEEEEEEE"
        };

        [TestMethod]
        public void GameMap()
        {
            var map = new GameMap(StringMap);
            Assert.IsNotNull(map);
            Assert.IsTrue(map[0, 0].Name == "EmptyCell");
            Assert.IsTrue(map[0, 11].Name == "Earth");
            Assert.IsTrue(map[0, 10].Name == "Grass");
            Assert.IsTrue(map[2, 1].Name == "Cloud");
            Assert.IsTrue(map[2, 9].Name == "Player");
            Assert.IsTrue(map[6, 9].Name == "Assassin");
            Assert.IsTrue(map[14, 9].Name == "Samara");
            Assert.IsTrue(map[3, 9].Name == "Box");
            Assert.IsTrue(map[4, 5].Name == "MovingPlatform");
            Assert.IsTrue(map[9, 8].Name == "MovingPlatform");
        }

        [TestMethod]
        public void TestMoveObjectAndMoveDown()
        {
            var map = new GameMap(StringMap1);
            Assert.AreEqual(map.HeroPosition, new Point() { X = 2, Y = 8 });
            map.MoveObject(map.HeroData, Keys.Right);
            Assert.AreEqual(map.HeroPosition, new Point() { X = 3, Y = 8 });
            map.MoveObject(map.HeroData, Keys.Left);
            Assert.AreEqual(map.HeroPosition, new Point() { X = 2, Y = 8 });
            map.MoveObject(map.HeroData, Keys.Up);
            Assert.AreEqual(map.HeroPosition, new Point() { X = 2, Y = 7 });
            map.MoveDown(map.HeroData);
            Assert.AreEqual(map.HeroPosition, new Point() { X = 2, Y = 8 });
        }

        [TestMethod]
        public void TestMakeDamage()
        {
            var map = new GameMap(StringMap);
            var previousDurability = map[map.HeroPosition.X + 1, map.HeroPosition.Y].Durability;
            var c = map[map.HeroPosition.X + 1, map.HeroPosition.Y].Durability / map.HeroData.AttackPower;
            map.MakeDamage(map.HeroData);
            Assert.IsTrue(map[map.HeroPosition.X + 1, map.HeroPosition.Y].Durability == previousDurability - 2);
            for (var i = 0; i < c; i++)
                map.MakeDamage(map.HeroData);
            Assert.IsTrue(map[map.HeroPosition.X + 1, map.HeroPosition.Y].Name == "EmptyCell");
        }

        [TestMethod]
        public void TestChangeObjectLocation()
        {
            var map = new GameMap(StringMap);
            var previousPosition = map.HeroPosition;
            map.ChangeObjectLocation(map.HeroData, new Point(previousPosition.X + 1, previousPosition.Y));
            Assert.IsTrue(map.HeroPosition == previousPosition);
            map.ChangeObjectLocation(map.HeroData, new Point(previousPosition.X - 1, previousPosition.Y));
            Assert.IsTrue(map.HeroPosition == new Point(previousPosition.X - 1, previousPosition.Y));
        }

        [TestMethod]
        public void TestMovePlatforms()
        {
            var map = new GameMap(StringMap);
            var previousPositions1 = new Point[] { new Point { X = 4, Y = 5 }, new Point { X = 5, Y = 5 } };
            var previousPositions2 = new Point[] { new Point { X = 9, Y = 8 }, new Point { X = 10, Y = 8 } };
            map.MovePlatforms();
            Assert.IsTrue(map[previousPositions1[0].X, previousPositions1[0].Y].Name == "EmptyCell");
            Assert.IsTrue(map[previousPositions1[1].X, previousPositions1[1].Y].Name == "MovingPlatform");
            Assert.IsTrue(map[previousPositions1[1].X + 1, previousPositions1[1].Y].Name == "MovingPlatform");
            Assert.IsTrue(map[previousPositions1[1].X + 2, previousPositions1[1].Y].Name == "EmptyCell");
            Assert.IsTrue(map[previousPositions2[0].X, previousPositions2[0].Y].Name == "EmptyCell");
            Assert.IsTrue(map[previousPositions2[0].X, previousPositions2[0].Y - 1].Name == "MovingPlatform");
            Assert.IsTrue(map[previousPositions2[1].X, previousPositions2[1].Y].Name == "EmptyCell");
            Assert.IsTrue(map[previousPositions2[1].X, previousPositions2[1].Y - 1].Name == "MovingPlatform");
        }

        [TestMethod]
        public void TestChangeCells()
        {
            var map = new GameMap(StringMap);
            var previousAssassinPosition = new Point { X = 6, Y = 9 };
            var newAssasinPosition = new Point { X = 7, Y = 9 };
            map.ChangeCells(previousAssassinPosition, newAssasinPosition, map[6, 9], new EmptyCell(new Point { X = 6, Y = 9 }));
            Assert.IsTrue(map[6, 9].Name == "EmptyCell");
            Assert.IsTrue(map[7, 9].Name == "Assassin");
        }
    }
}