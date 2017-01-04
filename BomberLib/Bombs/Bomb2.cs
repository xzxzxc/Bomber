using System;

namespace BomberLib.Bombs
{
    public class Bomb2:Bomb
    {
        public Bomb2() : base(GameData.GraphicsFactory.CreateBombSprite(2),
            GameData.SoundFactory.CreateBombBoomSound(2), TimeSpan.FromSeconds(2.0), 2)
        {
        }
    }
}