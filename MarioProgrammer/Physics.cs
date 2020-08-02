using System.Windows.Forms;

namespace MarioProgrammer
{
    public class Physics
    {
        private GameMap gameMap;

        private bool isJump;
        private bool movingUp;
        private int CountCellsInJump = 0;

        public bool IsJump { get => isJump; }
        public bool MovingUp { get => movingUp; }
        public GameMap GameMap { get => gameMap; }

        public Physics(GameMap map)
        {
            gameMap = map;
        }

        public void HeroStandUp()
        {
            isJump = false;
            movingUp = false;
            CountCellsInJump = 0;
        }

        public void MovePlatform()
        {
            gameMap.MovePlatforms();
        }

        public void MoveDown()
        {
            gameMap.MoveDown(GameMap.HeroData);
        }

        public void OneTickOfJump()
        {
            if (CountCellsInJump != 0)
                CountCellsInJump--;
            if (CountCellsInJump == 0)
                movingUp = false;
        }

        public void MoveHero(Keys key)
        {
            if (key == Keys.Up)
            {
                if (!isJump)
                {
                    isJump = true;
                    movingUp = true;
                    CountCellsInJump = 2;
                    gameMap.MoveObject(GameMap.HeroData, key);
                }
            }
            else
            {
                gameMap.MoveObject(GameMap.HeroData, key);
            }
        }
    }
}
