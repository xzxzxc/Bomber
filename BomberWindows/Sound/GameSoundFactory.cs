using BomberLibrary.Sound;
using Microsoft.Xna.Framework.Content;
using XNASoundEffect = Microsoft.Xna.Framework.Audio.SoundEffect;

namespace BomberWindows.Sound
{
    public class GameSoundFactory:SoundFactory
    {
        private readonly ContentManager _content;

        public override SoundEffect CreateBombBoomSound(int num)
        {
            return new GameSoundEffect(_content.Load<XNASoundEffect>("Sounds\\explosion" + num));
        }

        public override Music CreateMusic()
        {
            return new GameMusic(_content.Load<XNASoundEffect>("Sounds\\music"));
        }

        public GameSoundFactory(ContentManager content)
        {
            _content = content;
        }
    }
}