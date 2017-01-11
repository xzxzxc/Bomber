namespace BomberLib.Interfaces
{
    public interface IMovable
    {
        void MoveLeft(float speed);
        void MoveRight(float speed);
        void MoveUp(float speed);
        void MoveDown(float speed);
    }
}