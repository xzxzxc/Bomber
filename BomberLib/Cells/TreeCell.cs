using System;
using System.Runtime.Serialization;
using BomberLib.Items;

namespace BomberLib.Cells
{
    [Serializable]
    public class TreeCell : DecoratedCell
    {
        public TreeCell(Item hidenItem = null) : base(GameData.GraphicsFactory.CreateTreeSprite(), hidenItem) { }

        public override void UnBoom()
        {
            base.UnBoom();
            PrimarySprite = GameData.GraphicsFactory.CreateTreeSprite();
        }

        protected TreeCell(SerializationInfo propertyBag, StreamingContext context) : base(propertyBag, context)
        {
            if (!_isMovable)
            {
                PrimarySprite = GameData.GraphicsFactory.CreateTreeSprite();
                PrimarySprite.X = propertyBag.GetSingle("X");
                PrimarySprite.Y = propertyBag.GetSingle("Y");
            }
        }
    }
}