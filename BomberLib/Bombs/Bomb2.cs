using System;

namespace BomberLib.Bombs
{
    public class Bomb2:Bomb
    {
        public Bomb2(float x, float y) : base(GameData.GraphicsFactory.CreateBombSprite(x, y, 2),
            GameData.SoundFactory.CreateBombBoomSound(2), TimeSpan.FromSeconds(2.0), 2)
        {
        }
    }
}