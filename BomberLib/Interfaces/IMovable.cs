namespace BomberLib.Interfaces
{
    public interface IMovable
    {
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();

        float X { get; set; }
        float Y { get; set; }
    }
}