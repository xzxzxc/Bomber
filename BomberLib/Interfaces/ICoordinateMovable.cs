namespace BomberLib.Interfaces
{
    public interface ICoordinateMovable
    {
        void MoveLeft(float speed);
        void MoveRight(float speed);
        void MoveUp(float speed);
        void MoveDown(float speed);

        // float X {get; set; }
        // float Y { get; set; }
    }
}