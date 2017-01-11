using System;
using System.Runtime.Serialization;

namespace BomberLib.Cells
{
    [Serializable]
    public class RockCell : Cell
    {
        public RockCell(float x, float y) : base(GameData.GraphicsFactory.CreateRockSprite(x, y)) { }

        protected RockCell(SerializationInfo propertyBag, StreamingContext context) : base(propertyBag, context)
        {
            Sprite = GameData.GraphicsFactory.CreateRockSprite(propertyBag.GetSingle("X"), propertyBag.GetSingle("Y"));
        }
    }
}