 using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarioProgrammer
{
    public class Player : LivingGameObject
    {
        public override bool Living => true;
        public override bool Destructible { get => true; }
        public override string Name { get => "Player"; }
        public override Image Image => Image.FromFile(PlayerImages[PlayerImagesPointer]);
        public override bool Permeability => false;

        public int HealthPoint { get; private set; }
        public int Money { get; private set; }
        public override int AttackPower { get => attackPower; }
        public override bool LookRight { get => PlayerImagesPointer == 0; }

        private string[] PlayerImages = new string[2] { "Pictures/Girl/PlayerRight.png", "Pictures/Girl/PlayerLeft.png" };
        private int PlayerImagesPointer;
        private int attackPower;
        public override Point Location => location;

        private Point location;

        public override void ChangeLocation(Point newLocation)
        {
            location = newLocation;
        }

        public void LookOtherWay(Keys keys)
        {
            if (keys == Keys.Left)
                PlayerImagesPointer = 1;
            if (keys == Keys.Right)
                PlayerImagesPointer = 0;
        }

        public override void ReceiveDamage(int damage)
        {
            HealthPoint -= damage;
            if (HealthPoint < 0)
                HealthPoint = 0;
        }

        public Player(Point point)
        {
            location = point;
            HealthPoint = 10;
            attackPower = 2;
            Money = 0;
        }
    }
}
