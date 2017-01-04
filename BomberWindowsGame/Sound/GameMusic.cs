using BomberLib.Sound;
using Microsoft.Xna.Framework.Audio;
using SoundEffect = Microsoft.Xna.Framework.Audio.SoundEffect;

namespace BomberWindowsGame.Sound
{
    public class GameMusic : Music
    {
        private readonly SoundEffectInstance _instance;

        public GameMusic(SoundEffect soundEffect)
        {
            _instance = soundEffect.CreateInstance();
            _instance.IsLooped = true;
        }

        public override void Play()
        {
            _instance.Play();
        }

        public override void PauseOrResume()
        {
            if (_instance.State == SoundState.Paused)
                _instance.Resume();
            else
                _instance.Pause();
        }
    }
}