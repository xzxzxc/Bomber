using System;
using System.Runtime.Serialization;
using BomberLib.Items;

namespace BomberLib.Cells
{
    [Serializable]
    public class TreeCell : DecoratedCell
    {
        public TreeCell(float x, float y, Item hidenItem = null) : base(GameData.GraphicsFactory.CreateTreeSprite(x, y), hidenItem) { }

        public override void UnBoom()
        {
            base.UnBoom();
            PrimarySprite = GameData.GraphicsFactory.CreateTreeSprite(X, Y);
        }

        protected TreeCell(SerializationInfo propertyBag, StreamingContext context) : base(propertyBag, context)
        {
            if (!IsMovable)
            {
                PrimarySprite = GameData.GraphicsFactory.CreateTreeSprite(X, Y);
                PrimarySprite.X = propertyBag.GetSingle("X");
                PrimarySprite.Y = propertyBag.GetSingle("Y");
            }
        }
    }
}