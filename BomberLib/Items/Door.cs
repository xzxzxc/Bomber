namespace BomberLib.Items
{
    public class Door : Item
    {
        public Door(float x, float y) : base(GameData.GraphicsFactory.CreateDoorSprite(x, y))
        {
        }

        public override int GetHashCode()
        {
            return (int) ItemsHashCodes.Door;
        }
    }
}