using System.Runtime.Serialization;

namespace BomberLibrary.Cells
{
    [DataContract]
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

    }
}