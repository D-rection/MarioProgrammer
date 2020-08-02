using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;


namespace MarioProgrammer
{
    public enum HeroNames
    {
        Nerd,
        Chewbacca,
        Princess
    }

    public enum SkinNames
    {
        StandLeft,
        StandRight,
        MovingLeftFirst,
        MovingLeftSecond,
        MovingRightFirst,
        MovingRightSecond
    }

    public enum HeroPhrases
    {
        Hello,
        Bye,
        SaveMe,
        Ok,
        Aagh
    }

    public enum Music
    {
        MainTheme,
        CalmMusic,
        BattleMusic
    }

    public class SkinData
    {
        public string Name { get; private set; }
        public Image[] Skins { get; private set; }
        public SoundPlayer[] Phrases { get; private set; }
        public SoundPlayer[] Music { get; private set; }
        public bool Unlocked { get; private set; }

        public void Unlock()
        {
            Unlocked = true;
        }

        public SkinData(string name, Image[] skins, SoundPlayer[] sounds, SoundPlayer[] musics, bool unlocked)
        {

            Name = name;
            for (var i = 0; i < sounds.Length; i++)
                Phrases[i] = sounds[i];
            for (var i = 0; i < musics.Length; i++)
                Music[i] = musics[i];
            for (var i = 0; i < skins.Length; i++)
                Skins[i] = skins[i];
            Unlocked = unlocked;
        }
    }

    public class Achievement
    {
        public string Name { get; private set; }
        public bool Unlocked { get; private set; }

        public void Unlock()
        {
            Unlocked = true;
        }
    }

    class GameData
    {
        public DateTime TimeInGame { get; private set; }
        public int Money { get; private set; }
        public int[] BestPoints { get; private set; }
        public SkinData[] Skins { get; private set; }
        public Dictionary<string, Achievement> Achievements { get; private set; }
        public string[] Paths { get; private set; }

        public GameData()
        {
            var gameFile = File.ReadAllLines("/Architecture/GameMap.txt");
            TimeInGame = DateTime.Parse(gameFile[0]);
            Money = int.Parse(gameFile[1]);
            var countBestPoints = int.Parse(gameFile[2]);
            BestPoints = new int[countBestPoints];
            var count = 3;
            for (var i = 0; i < countBestPoints; i++)
            {
                BestPoints[count] = int.Parse(gameFile[count]);
                count++;
            }
            var skinPaths = int.Parse(gameFile[count]);
            for (var i = 0; i < skinPaths; i++)
            {
                SetSkin(gameFile[count]);
                count++;
            }
            var countAchievement = gameFile[count];
            count++;

        }

        private void SetSkin(string path)
        {

        }
    }
}
