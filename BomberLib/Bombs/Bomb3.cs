using System;

namespace BomberLib.Bombs
{
    public class Bomb3:Bomb
    {
        public Bomb3() : base(GameData.GraphicsFactory.CreateBombSprite(3),
            GameData.SoundFactory.CreateBombBoomSound(3), TimeSpan.FromSeconds(1.5), 3)
        {
        }
    }
}