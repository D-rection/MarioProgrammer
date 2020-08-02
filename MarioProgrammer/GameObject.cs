using System.Drawing;

namespace MarioProgrammer
{
    public class GameObject
    {
        public virtual bool Living { get => false; }
        public virtual bool Destructible { get => true; }
        public virtual string Name { get => "Name"; }
        public virtual Image Image => null;
        public virtual bool Permeability { get => true; }
        public virtual Point Location { get => new Point(); }
    }

    public class LivingGameObject : GameObject
    {
        public virtual int AttackPower { get => 2; }
        public virtual Image DeathImage { get => null; }
        public virtual bool LookRight { get => true; }
        public virtual int Durability { get => 10; }

        public virtual void ChangeLocation(Point newLocation)
        {

        }

        public virtual void ReceiveDamage(int damage)
        {

        }
    }

    public class EmptyCell : GameObject
    {
        public override bool Living => false;
        public override bool Destructible => false;
        public override Image Image => null;
        public override string Name { get => "EmptyCell"; }
        public override bool Permeability => true;
        public override Point Location { get => location; }

        private Point location;

        public EmptyCell(Point point)
        {
            location = point;
        }
    }

    public class Box : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => true; }

        private int durability = 10;
        public int Durability { get => durability; }
        public override string Name { get => "Box"; }
        public override Image Image => Image.FromFile("Pictures/Box.png");
        public override bool Permeability => false;
        public override Point Location { get => location; }

        private Point location;

        public void ChangeLocation(Point newLocation)
        {
            location = newLocation;
        }

        public void ReceiveDamage(int damage)
        {
            durability -= damage;
            if (durability < 0)
                durability = 0;
        }

        public Box(Point point)
        {
            location = point;
        }
    }

    public class Grass : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => false; }
        public override string Name { get => "Grass"; }
        public override Image Image => Image.FromFile("Pictures/Grass.png");
        public override bool Permeability => false;
        public override Point Location => location;

        private Point location;

        public Grass(Point point)
        {
            location = point;
        }
    }

    public class Earth : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => false; }
        public override string Name { get => "Earth"; }
        public override Image Image => Image.FromFile("Pictures/Earth.png");
        public override bool Permeability => false;
        public override Point Location => location;

        private Point location;

        public Earth(Point point)
        {
            location = point;
        }
    }

    public class Cloud : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => false; }
        public override string Name { get => "Cloud"; }
        public override Image Image => Image.FromFile("Pictures/Cloud.png");
        public override bool Permeability => false;
        public override Point Location => location;

        private Point location;

        public Cloud(Point point)
        {
            location = point;
        }
    }

    public class Water : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => false; }
        public override string Name { get => "Water"; }
        public override Image Image => Image.FromFile("Pictures/Water.png");
        public override bool Permeability => false;
        public override Point Location => location;

        private Point location;

        public Water(Point point)
        {
            location = point;
        }
    }

    public class Lava : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => false; }
        public override string Name { get => "Lava"; }
        public override Image Image => Image.FromFile("Pictures/Lava.png");
        public override bool Permeability => false;
        public override Point Location => location;

        private Point location;

        public Lava(Point point)
        {
            location = point;
        }
    }

    public class Money : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => true; }
        public override string Name { get => "Money"; }
        public override Image Image => Image.FromFile("Pictures/Coin.png");
        public override bool Permeability => true;
        public override Point Location { get => location; }

        private Point location;

        public Money(Point point)
        {
            location = point;
        }
    }

    public class Treasures : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => true; }
        public override string Name { get => "Treasures"; }
        public override Image Image => Image.FromFile("Pictures/Treasures.png");
        public override bool Permeability => true;
        public override Point Location { get => location; }

        private Point location;

        public Treasures(Point point)
        {
            location = point;
        }
    }

    public class Crystal : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => true; }
        public override string Name { get => "Crystal"; }
        public override Image Image { get => Image.FromFile(image); }
        public override bool Permeability => true;
        public override Point Location { get => location; }

        private Point location;
        private string image;

        public Crystal(Point point, string color)
        {
            if (color == "Pink")
                image = "Pictures/CrystalPink.png";
            else if (color == "Blue")
                image = "Pictures/CrystalBlue.png";
            else if (color == "Green")
                image = "Pictures/CrystalGreen.png";
            location = point;
        }
    }

    public class Eat : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => true; }
        public override string Name { get => "Eat"; }
        public override Image Image { get => Image.FromFile(image); }
        public override bool Permeability => true;
        public override Point Location { get => location; }

        private Point location;
        private string image;

        public Eat(Point point, string fruit)
        {
            if (fruit == "Apple")
                image = "Pictures/Apple.png";
            else if (fruit == "Banana")
                image = "Pictures/Banana.png";
            else if (fruit == "Pineapple")
                image = "Pictures/Pineapple.png";
            location = point;
        }
    }

    public class Dumbbell : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => true; }
        public override string Name { get => "Dumbbell"; }
        public override Image Image { get => Image.FromFile("Pictures/Dumbbell.png"); }
        public override bool Permeability => true;
        public override Point Location { get => location; }

        private Point location;

        public Dumbbell(Point point)
        {
            location = point;
        }
    }

    public class Portal : GameObject
    {
        public override bool Living => false;
        public override bool Destructible { get => true; }
        public override string Name { get => "Portal"; }
        public override Image Image { get => Image.FromFile("Pictures/Portal.png"); }
        public override bool Permeability => true;
        public override Point Location { get => location; }

        private Point location;

        public Portal(Point point)
        {
            location = point;
        }
    }
}
