using System;

namespace BomberLibrary.Bombs
{
    public class Bomb3:Bomb
    {
        public Bomb3(float x, float y) : base(GameData.GraphicsFactory.CreateBombSprite(x, y, 3),
            GameData.SoundFactory.CreateBombBoomSound(3), TimeSpan.FromSeconds(1.5), 3)
        {
        }
    }
}