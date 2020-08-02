using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MarioProgrammer
{
    public class GameMap
    {
        private Player heroData;
        private GameObject[,] gameMap;
        private MovingPlatform[] platformPositions = new MovingPlatform[100];
        private Monster[] MonsterPositions = new Monster[100];

        public int Width { get; }
        public int Height { get; }

        public MovingPlatform[] PlatformPositions { get => platformPositions; }
        public Player HeroData { get => heroData; }
        public Point HeroPosition { get => heroData.Location; }
        public event Action MoveHero;

        public GameObject this[int row, int column]
        {
            get
            {
                if (row >= 0 && row < gameMap.GetLength(0)
                    && column >= 0 && column < gameMap.GetLength(1))
                    return gameMap[row, column];
                throw new IndexOutOfRangeException();
            }
        }

        public void MoveMonsters()
        {
            for (var i = 0; i < MonsterPositions.Length; i++)
            {
                if (MonsterPositions[i] == null)
                    break;
                var direction = (MonsterPositions[i].MoveLeft) ? Keys.Left : Keys.Right;
                if (Math.Abs(MonsterPositions[i].Location.X - HeroData.Location.X) == 1)
                    direction = Keys.Space;
                MonsterPositions[i].ChangeDirection();
                MoveObject(MonsterPositions[i], direction);
            }
        }

        public void MovePlatforms()
        {
            for (var i = 0; i < platformPositions.Length; i++)
            {
                if (platformPositions[i] == null)
                    break;
                var oldPosition = platformPositions[i].CurrentCountCells;
                platformPositions[i].Move();
                var newPosition = platformPositions[i].CurrentCountCells;
                var differnts = newPosition - oldPosition;
                var oldPoint = platformPositions[i].Location;
                var movingWithHero = (heroData.Location == new Point(oldPoint.X, oldPoint.Y - 1)) ? true : false;
                if (platformPositions[i].IsUpDownPlatform)
                {
                    platformPositions[i].ChangeLocation(new Point
                    {
                        X = platformPositions[i].Location.X,
                        Y = platformPositions[i].Location.Y + differnts
                    }); 
                    if (movingWithHero)
                        ChangeObjectLocation(heroData, new Point(oldPoint.X, oldPoint.Y - 1 + differnts));
                }
                else
                {

                    platformPositions[i].ChangeLocation( new Point
                    {
                        X = platformPositions[i].Location.X + differnts,
                        Y = platformPositions[i].Location.Y
                    });
                    if (movingWithHero)
                        ChangeObjectLocation(heroData, new Point(oldPoint.X + differnts, oldPoint.Y - 1));
                }
                var newPoint = platformPositions[i].Location;
                if (i > 0 && platformPositions[i - 1].Location == oldPoint)
                    ChangeCells(oldPoint, newPoint, platformPositions[i], null);
                else
                    ChangeCells(oldPoint, newPoint, platformPositions[i], new EmptyCell(oldPoint));
            }
        }

        public void MoveObject(LivingGameObject gameObject, Keys key)
        {
            if (gameObject == heroData &&
                (key == Keys.Right && !heroData.LookRight || key == Keys.Left && heroData.LookRight))
                heroData.LookOtherWay(key);
            if (key == Keys.Up)
                ChangeObjectLocation(gameObject, new Point(gameObject.Location.X, gameObject.Location.Y - 1));
            if (key == Keys.Left)
                ChangeObjectLocation(gameObject, new Point(gameObject.Location.X - 1, gameObject.Location.Y));
            if (key == Keys.Right)
                ChangeObjectLocation(gameObject, new Point(gameObject.Location.X + 1, gameObject.Location.Y));
            if (key == Keys.Space)
                MakeDamage(gameObject);
        }

        public void MoveDown(LivingGameObject gameObject)
        {
            ChangeObjectLocation(gameObject,new Point(gameObject.Location.X, gameObject.Location.Y + 1));
        }

        public void MakeDamage(LivingGameObject gameObject)
        {
            var x = (gameObject.LookRight) ? gameObject.Location.X + 1 : gameObject.Location.X - 1;
            var y = gameObject.Location.Y;
            if (x >= 0)
            {
                if (gameMap[x, y].Destructible && gameMap[x, y].GetType() == typeof(LivingGameObject) || gameMap[x, y].Name == "Box")
                    ((LivingGameObject)gameMap[x, y]).ReceiveDamage(gameObject.AttackPower);
                if (gameMap[x, y].GetType() == typeof(LivingGameObject) || gameMap[x, y].Name == "Box" && ((LivingGameObject)gameMap[x, y]).Durability == 0)
                    gameMap[x, y] = new EmptyCell(new Point(x, y));
            }
            MoveHero();
        }

        public void ChangeObjectLocation(LivingGameObject gameObject, Point newLocation)
        {
            if (newLocation.X >= 0 && newLocation.Y >= 0
                && newLocation.X < Width && newLocation.Y < Height
                && gameMap[newLocation.X, newLocation.Y].Permeability)
            {
                ChangeCells(gameObject.Location, newLocation, gameObject, new EmptyCell(gameObject.Location));
                gameObject.ChangeLocation(newLocation);
                MoveHero();
            }
        }

        public void ChangeCells(Point startPosition, Point endPosition, GameObject movingObject, GameObject stayObject)
        {
            if (stayObject != null)
                gameMap[startPosition.X, startPosition.Y] = stayObject;
            gameMap[endPosition.X, endPosition.Y] = movingObject;
        }

        public GameMap(string[] input)
        {
            var countMovingPlatform = 0;
            var countMonsters = 0;
            gameMap = new GameObject[input[0].Length, input.Length];
            Width = input[0].Length;
            Height = input.Length;
            for (var i = 0; i < input.Length; i++)
                for (var j = 0; j < input[0].Length; j++)
                {
                    var point = new Point(j, i);
                    switch (input[i][j])
                    {
                        case '1':
                            {
                                gameMap[j, i] = new Crystal(point, "Pink");
                                break;
                            }
                        case '2':
                            {
                                gameMap[j, i] = new Crystal(point, "Blue");
                                break;
                            }
                        case '3':
                            {
                                gameMap[j, i] = new Crystal(point, "Green");
                                break;
                            }
                        case '4':
                            {
                                gameMap[j, i] = new Eat(point, "Apple");
                                break;
                            }
                        case '5':
                            {
                                gameMap[j, i] = new Eat(point, "Banana");
                                break;
                            }
                        case '6':
                            {
                                gameMap[j, i] = new Eat(point, "Pineapple");
                                break;
                            }
                        case 'D':
                            {
                                gameMap[j, i] = new Dumbbell(point);
                                break;
                            }
                        case 'X':
                            {
                                gameMap[j, i] = new Portal(point);
                                break;
                            }
                        case 'B':
                            {
                                gameMap[j, i] = new Box(point);
                                break;
                            }
                        case 'M':
                            {
                                gameMap[j, i] = new Money(point);
                                break;
                            }
                        case 'T':
                            {
                                gameMap[j, i] = new Treasures(point);
                                break;
                            }
                        case 'P':
                            {
                                gameMap[j, i] = new Player(point);
                                heroData = new Player(point);
                                break;
                            }
                        case 'G':
                            {
                                gameMap[j, i] = new Grass(point);
                                break;
                            }
                        case 'E':
                            {
                                gameMap[j, i] = new Earth(point);
                                break;
                            }
                        case 'W':
                            {
                                gameMap[j, i] = new Water(point);
                                break;
                            }
                        case 'L':
                            {
                                gameMap[j, i] = new Lava(point);
                                break;
                            }
                        case 'C':
                            {
                                gameMap[j, i] = new Cloud(point);
                                break;
                            }
                        case 'A':
                            {
                                var monster = new Monster("Assassin", point);
                                gameMap[j, i] = monster;
                                MonsterPositions[countMonsters] = monster;
                                countMonsters++;
                                break;
                            }
                        case 'S':
                            {
                                var monster = new Monster("Samara", point);
                                gameMap[j, i] = monster;
                                MonsterPositions[countMonsters] = monster;
                                countMonsters++;
                                break;
                            }
                        case 'H':
                            {
                                var platform = new MovingPlatform(false, 3, point);
                                gameMap[j, i] = platform;
                                platformPositions[countMovingPlatform] = platform;
                                countMovingPlatform++;
                                break;
                            }
                        case 'R':
                            {
                                var platform = new MovingPlatform(false, 5, point);
                                gameMap[j, i] = platform;
                                platformPositions[countMovingPlatform] = platform;
                                countMovingPlatform++;
                                break;
                            }
                        case 'V':
                            {
                                var platform = new MovingPlatform(true, 5, point);
                                gameMap[j, i] = platform;
                                platformPositions[countMovingPlatform] = platform;
                                countMovingPlatform++;
                                break;
                            }
                        case 'U':
                            {
                                var platform = new MovingPlatform(true, 2, point);
                                gameMap[j, i] = platform;
                                platformPositions[countMovingPlatform] = platform;
                                countMovingPlatform++;
                                break;
                                }
                        default:
                            {
                                gameMap[j, i] = new EmptyCell(point);
                                break;
                            }
                    }
                }
        }
    }
}
