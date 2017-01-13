using System.Runtime.Serialization;
using BomberLibrary.Items;
using JetBrains.Annotations;

namespace BomberLibrary.Cells
{
    [DataContract]
    public class TreeCell : DecoratedCell
    {
        public TreeCell(float x, float y, Item hidenItem = null) : 
            base(GameData.GraphicsFactory.CreateTreeSprite(x, y), hidenItem) { }

        public override void UnBoom()
        {
            base.UnBoom();
            PrimarySprite = GameData.GraphicsFactory.CreateTreeSprite(X, Y);
        }

        [OnDeserialized]
        [UsedImplicitly]
        public void DeleteTreeIfMovable(StreamingContext context)
        {
            if (IsMovable)
                PrimarySprite = null;
        }
    }
}