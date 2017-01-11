using System;

namespace BomberLib.Bombs
{
    public class Bomb1:Bomb
    {
        public Bomb1(float x, float y) : base(GameData.GraphicsFactory.CreateBombSprite(x, y, 1),
            GameData.SoundFactory.CreateBombBoomSound(1), TimeSpan.FromSeconds(2.5), 1)
        {
        }
    }
}