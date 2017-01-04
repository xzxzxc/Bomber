using System;

namespace BomberLib.Bombs
{
    public class Bomb1:Bomb
    {
        public Bomb1() : base(GameData.GraphicsFactory.CreateBombSprite(1),
            GameData.SoundFactory.CreateBombBoomSound(1), TimeSpan.FromSeconds(2.5), 1)
        {
        }
    }
}