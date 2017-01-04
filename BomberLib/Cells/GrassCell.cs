using System;
using System.Runtime.Serialization;

namespace BomberLib.Cells
{
    [Serializable]
    public class GrassCell:Cell
    {
        public GrassCell() : base(GameData.GraphicsFactory.CreateGrassSprite())
        {
            _isMovable = true;
        }

        public override void Boom()
        {

            base.Boom();
            ChangeSprite(GameData.GraphicsFactory.CreateGrassAdterBoomSprite());
            _sprite.DrawAnimatonOneTime(0, X, Y);
        }

        public override void UnBoom()
        {
            base.UnBoom();
            ChangeSprite(GameData.GraphicsFactory.CreateGrassSprite());
        }

        protected GrassCell(SerializationInfo propertyBag, StreamingContext context) : base(propertyBag, context)
        {
            _sprite = GameData.GraphicsFactory.CreateGrassSprite();
            X = propertyBag.GetSingle("X");
            Y = propertyBag.GetSingle("Y");
        }
    }
}