using System;
using System.Runtime.Serialization;

namespace BomberLib.Cells
{
    [Serializable]
    public class GrassCell:Cell
    {
        public GrassCell(float x, float y) : base(GameData.GraphicsFactory.CreateGrassSprite(x, y))
        {
            IsMovable = true;
        }

        public override void Boom()
        {

            base.Boom();
            ChangeSprite(GameData.GraphicsFactory.CreateGrassAdterBoomSprite(X, Y));
            Sprite.StartDrawingAnimationToEnd(0);
        }

        public override void UnBoom()
        {
            base.UnBoom();
            ChangeSprite(GameData.GraphicsFactory.CreateGrassSprite(X, Y));
        }

        protected GrassCell(SerializationInfo propertyBag, StreamingContext context) : base(propertyBag, context)
        {
            Sprite = GameData.GraphicsFactory.CreateGrassSprite(propertyBag.GetSingle("X"), propertyBag.GetSingle("Y"));
        }
    }
}