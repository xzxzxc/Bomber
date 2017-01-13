namespace BomberLibrary.Sound
{
    public abstract class SoundFactory
    {
        // Bombs
        public abstract SoundEffect CreateBombBoomSound(int num);

        public abstract Music CreateMusic();
    }
}