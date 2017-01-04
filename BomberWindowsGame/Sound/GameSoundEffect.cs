using BomberLib.Sound;
using XNASoundEffect = Microsoft.Xna.Framework.Audio.SoundEffect;

namespace BomberWindowsGame.Sound
{
    public class GameSoundEffect:SoundEffect
    {
        private readonly XNASoundEffect _xnaSoundEffect;

        public GameSoundEffect(XNASoundEffect xnaSoundEffect)
        {
            _xnaSoundEffect = xnaSoundEffect;
        }

        public override void Play()
        {
            _xnaSoundEffect.Play();
        }
    }
}