namespace BomberLib.Items
{
    public class Door : Item
    {
        public Door() : base(GameData.GraphicsFactory.CreateDoorSprite())
        {
        }

        public override int GetHashCode()
        {
            return (int) ItemsHashCodes.Door;
        }
    }
}