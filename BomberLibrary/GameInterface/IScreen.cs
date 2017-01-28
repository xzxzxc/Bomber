using BomberLibrary.Interfaces;

namespace BomberLibrary.GameInterface
{
    public interface IScreen: IDrawable
    {
        void Load();
        void UnLoad();
    }
}