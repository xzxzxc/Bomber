using System;
using System.Runtime.Serialization;

namespace BomberLib.Cells
{
    [Serializable]
    public class RockCell : Cell
    {
        public RockCell() : base(GameData.GraphicsFactory.CreateRockSprite()) { }

        protected RockCell(SerializationInfo propertyBag, StreamingContext context) : base(propertyBag, context)
        {
            _sprite = GameData.GraphicsFactory.CreateRockSprite();
            X = propertyBag.GetSingle("X");
            Y = propertyBag.GetSingle("Y");
        }
    }
}