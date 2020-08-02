using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioProgrammer
{
    public class Monster : LivingGameObject
    {
        public override bool Living => true;
        public override bool Destructible { get => true; }
        public override string Name { get => name; }
        public override Image Image { get => Image.FromFile(image); }
    public override bool Permeability => false;

        public override int Durability  { get => durability;}       
        public override int AttackPower { get => attackPower; }
        public override Point Location => location;

        private string name;
        private string image;
        private int attackPower;
        private int durability;
        public bool MoveLeft { get => moveLeft; }
        private bool moveLeft = true;

        private Point location;

        public override void ChangeLocation(Point newLocation)
        {
            location = newLocation;
        }

        public override void ReceiveDamage(int damage)
        {
            durability -= damage;
            if (durability < 0)
                durability = 0;
        }

        public void ChangeDirection()
        {
            moveLeft = !moveLeft;
        }

        public Monster(string name, Point point)
        {
            if (name.CompareTo("Assassin") == 0)
            {
                this.name = name;
                image = "Pictures/Assassin.png";
                durability = 5;
                attackPower = 1;
                location = point;
            }
            else if (name.CompareTo("Samara") == 0)
            {
                this.name = name;
                image = "Pictures/Samara.png";
                durability = 20;
                attackPower = 2;
                location = point;
            }
        }
    }
}
