using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioProgrammer
{
    public class MovingPlatform : GameObject
    {
        public override bool Living => false;
        public override bool Destructible => false;
        public override string Name => "MovingPlatform";
        public override bool Permeability => false;
        public override Image Image => Image.FromFile("Pictures/MovingPlatform.png");

        public bool IsUpDownPlatform { get; private set; }
        public int CountCells { get; private set; }
        public bool StraightDirection { get; private set; }
        public int CurrentCountCells { get; private set; }
        public override Point Location => location;

        private Point location;

        public void ChangeLocation(Point newLocation)
        {
            location = newLocation;
        }

        public Point CurrentLocation()
        {
            if (IsUpDownPlatform)
                return new Point(location.X, location.Y + CurrentCountCells);           
            else
                return new Point(location.X + CurrentCountCells, location.Y);
        }

        public void Move()
        {
            if (StraightDirection)
            {
                if (CurrentCountCells == CountCells || CurrentCountCells == -CountCells)
                {
                    CurrentCountCells = (IsUpDownPlatform) ? CurrentCountCells + 1 : CurrentCountCells - 1;
                    StraightDirection = !StraightDirection;
                }
                else
                    CurrentCountCells = (IsUpDownPlatform) ? CurrentCountCells - 1 : CurrentCountCells + 1;
            }
            else
            {
                if (CurrentCountCells == 0)
                {
                    CurrentCountCells = (IsUpDownPlatform) ? CurrentCountCells - 1 : CurrentCountCells + 1;
                    StraightDirection = !StraightDirection;
                }
                else
                    CurrentCountCells = (IsUpDownPlatform) ? CurrentCountCells + 1 : CurrentCountCells - 1;
            }
        }

        public MovingPlatform(bool isUp, int cells, Point point)
        {
            location = point;
            IsUpDownPlatform = isUp;
            CountCells = cells;
            CurrentCountCells = 0;
            StraightDirection = true;
        }
    }
}
