using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace MarioProgrammer
{
    static class CellSize
    {
        public static int Size { get => 60; }
        public static int ScreenWidth { get => 21; }
        public static int ScreenHeight { get => 12; }
    }

    static class Program
    {
        public static class RenderingAssistance
        {
            public static Point GetPoint(Point point)
            {
                return new Point(point.X * CellSize.Size, point.Y * CellSize.Size);
            }
        }

        public class GameForm : Form
        {           
            public GameForm()
            {
                Icon = new Icon("Label.ico");
                Text = "Mario Programmer";
                BackColor = Color.Black;
                Size = new Size(1275, 760);
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false;
                StartPosition = FormStartPosition.CenterScreen;

                var globalMap = new GameMap(GetMap());
                var screenPicture = new PictureBox
                {
                    BackgroundImage = Image.FromFile("BackGround.jpg"),
                    Dock = DockStyle.Fill,
                    Size = new Size(1260, 720),
                    Location = new Point(0, 0)
                };
                var labels = GetLabel(globalMap, screenPicture);
                var physics = new Physics(globalMap);


                var monsterTimer = new Timer();
                monsterTimer.Interval = 500;
                monsterTimer.Tick += new EventHandler((sender, args) =>
                {
                    physics.GameMap.MoveMonsters();
                });
                monsterTimer.Start();

                var platformTimer = new Timer();
                platformTimer.Interval = 1000;
                platformTimer.Tick += new EventHandler((sender, args) =>
                    ManageMovingPlatform(physics, labels, screenPicture));
                platformTimer.Start();

                var timer = new Timer();
                timer.Interval = 100;
                timer.Tick += new EventHandler((sender, args) => ManageJump(physics));
                timer.Start();


                KeyDown += (sender, args) => physics.MoveHero(args.KeyCode);
                Controls.Add(screenPicture);
                physics.GameMap.MoveHero += () =>
                {
                    Invalidate();
                };

                Paint += (sender, args) =>
                {
                    SetLabels(labels, globalMap, screenPicture);
                };
            }
          

            private void ManageMovingPlatform(Physics physics, Label[] labels, PictureBox pictureBox)
            {
                //var frameRate = 10;

                //var oldPositions = new Point[physics.GameMap.PlatformPositions.Length];
                //for (var i = 0; i < oldPositions.Length; i++)
                //{
                //    if (physics.GameMap.PlatformPositions[i] == null)
                //        break;
                //    oldPositions[i] = physics.GameMap.PlatformPositions[i].CurrentLocation();
                //}

                physics.MovePlatform();

                //var newPositions = new Point[physics.GameMap.PlatformPositions.Length];
                //for (var i = 0; i < oldPositions.Length; i++)
                //{
                //    if (physics.GameMap.PlatformPositions[i] == null)
                //        break;
                //    newPositions[i] = physics.GameMap.PlatformPositions[i].CurrentLocation();
                //}

                //var animationLabel = new Label[labels.Length];
                //for (var i = 0; i < labels.Length; i++)
                //    animationLabel[i] = labels[i];

                //var shiftPoints = new Point[animationLabel.Length];
                //for (var j = 0; j < physics.GameMap.PlatformPositions.Length; j++)
                //{
                //    var oldPlatform = oldPositions[j];
                //    if (oldPlatform == null)
                //        break;
                //    var shiftPointX = newPositions[j].X - oldPositions[j].X;
                //    var shiftPointY = newPositions[j].Y - oldPositions[j].Y;
                //    shiftPoints[oldPlatform.X + oldPlatform.Y * CellSize.ScreenWidth] =
                //        RenderingAssistance.GetPoint(new Point(shiftPointX, shiftPointY));
                //}

                //var countFrameRate = 0;
                //var animationTimer = new Timer();
                //animationTimer.Interval = 100;
                //animationTimer.Tick += new EventHandler((sender, args) =>
                //{
                //    if (countFrameRate == frameRate)
                //    {
                //        animationTimer.Stop();
                //        animationTimer.Dispose();
                //    }
                //    for (var j = 0; j < physics.GameMap.PlatformPositions.Length; j++)
                //    {
                //        var oldPlatform = oldPositions[j];
                //        if (oldPlatform == null)
                //            break;
                //        var location = RenderingAssistance.GetPoint(new Point(oldPositions[j].X, oldPositions[j].Y));
                //        location.X += countFrameRate * shiftPoints[j].X / frameRate;
                //        location.Y += countFrameRate * shiftPoints[j].X / frameRate;
                //        animationLabel[oldPlatform.X + oldPlatform.Y * CellSize.ScreenWidth] = new Label
                //        {
                //            Location = location,
                //            Image = physics.GameMap.PlatformPositions[0].Image
                //        };
                //    }
                //    countFrameRate++;
                //    SetGameScreen(animationLabel, pictureBox);
                //});
                //animationTimer.Start();                                 

                Invalidate();
            }

            private void ManageJump(Physics physics)
            {
                var x = physics.GameMap.HeroPosition.X;
                var y = physics.GameMap.HeroPosition.Y + 1;
                if (physics.IsJump)
                {
                    if (physics.MovingUp)
                        physics.OneTickOfJump();
                    else
                    {
                        if (physics.GameMap[x, y].Permeability)
                            physics.GameMap.MoveDown(physics.GameMap.HeroData);

                        else
                            physics.HeroStandUp();
                    }
                }
                else
                {
                    if (physics.GameMap[x, y].Permeability)
                        physics.MoveDown();
                }
            }

            private string[] GetMap()
            {
                var result = File.ReadAllLines("Architecture/Map.txt");
                return result;
            }

            //   Функции отрисовки экрана      
            #region
            private void SetLabels(Label[] label, GameMap map, PictureBox pictureBox)
            {
                var originScreenCoordinates = SetRelativeOfCoordinate(map.HeroPosition, map.Width, map.Height);
                for (var i = 0; i < CellSize.ScreenHeight; i++)
                    for (var j = 0; j < CellSize.ScreenWidth; j++)
                    {
                        var x = j + originScreenCoordinates.X;
                        var y = i + originScreenCoordinates.Y;
                        if (map.Width > x && map.Height > y)
                        {
                            if (label[j + CellSize.ScreenWidth * i] != null)
                            {
                                if (label[j + CellSize.ScreenWidth * i].Name != map[x, y].Name)
                                {
                                    label[j + CellSize.ScreenWidth * i].Image = map[x, y].Image;
                                    label[j + CellSize.ScreenWidth * i].Name = map[x, y].Name;
                                }
                            }
                            else
                            {
                                label[j + CellSize.ScreenWidth * i] = new Label()
                                {
                                    BackColor = Color.Transparent,
                                    Location = RenderingAssistance.GetPoint(new Point(j, i)),
                                    Image = map[x, y].Image,
                                    Size = new Size(CellSize.Size, CellSize.Size),
                                    Name = map[x, y].Name
                                };
                            }
                        }
                        else
                        {
                            label[j + CellSize.ScreenWidth * i].Image = null;
                        }
                    }
                SetGameScreen(label, pictureBox);
            }

            private Point SetRelativeOfCoordinate(Point absolutHeroPosition, int width, int height)
            {
                var relativeHeroPosition = new Point();
                var isHorizontalCenter = StayInCenter(absolutHeroPosition.X, false);
                var isVericalCenter = StayInCenter(absolutHeroPosition.Y, true);
                if (isHorizontalCenter)
                {
                    if (isVericalCenter)
                        relativeHeroPosition = new Point(CellSize.ScreenWidth / 2 - 1,
                            CellSize.ScreenHeight / 2 - 1);
                    else
                        relativeHeroPosition = new Point(CellSize.ScreenWidth / 2 - 1,
                            absolutHeroPosition.Y % CellSize.ScreenWidth);
                }
                else
                {
                    if (isVericalCenter)
                        relativeHeroPosition = new Point(absolutHeroPosition.X % CellSize.ScreenWidth,
                            CellSize.ScreenWidth / 2 - 1);
                    else
                        relativeHeroPosition = new Point(absolutHeroPosition.X % CellSize.ScreenWidth,
                            absolutHeroPosition.Y % CellSize.ScreenWidth);
                }
                var x = absolutHeroPosition.X - relativeHeroPosition.X;
                if (width - x < CellSize.ScreenWidth && width >= CellSize.ScreenWidth)
                    x = width - CellSize.ScreenWidth;
                var y = absolutHeroPosition.Y - relativeHeroPosition.Y;
                if (height - y < CellSize.ScreenHeight && height >= CellSize.ScreenHeight)
                    y = height - CellSize.ScreenHeight;
                return new Point(x, y);
            }

            private bool StayInCenter(int position, bool isVert)
            {
                var result = (isVert) ? position > CellSize.ScreenWidth / 2 - 1
                    && position < Height - (CellSize.ScreenWidth / 2 - 1)
                   : position > CellSize.ScreenWidth / 2 - 1
                   && position < Width - (CellSize.ScreenWidth / 2 - 1);
                return result;
            }

            private Label[] GetLabel(GameMap map, PictureBox pictureBox)
            {
                var result = new Label[CellSize.ScreenHeight * CellSize.ScreenWidth];
                Invalidate();
                return result;
            }

            private void PrintOnBitMap(PictureBox pictureBox, Label[] labels)
            {
                var graphic = Graphics.FromImage(pictureBox.Image);
                for (var j = 0; j < CellSize.ScreenHeight; j++)
                    for (var i = 0; i < CellSize.ScreenWidth; i++)
                    {
                        var currentLabel = labels[i + j * CellSize.ScreenWidth];
                        graphic.DrawImage(currentLabel.Image, currentLabel.Location);
                    }
            }

            private void SetGameScreen(Label[] labels, PictureBox pictureBox)
            {
                for (var j = 0; j < CellSize.ScreenHeight; j++)
                    for (var i = 0; i < CellSize.ScreenWidth; i++)
                    {
                        pictureBox.Controls.Add(labels[i + j * CellSize.ScreenWidth]);
                    }
            }
            #endregion
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new GameForm());
        }
    }
}
