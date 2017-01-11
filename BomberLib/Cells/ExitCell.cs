using System;
using System.Runtime.Serialization;
using BomberLib.Items;

namespace BomberLib.Cells
{
    [Serializable]
    public class ExitCell : TreeCell
    {
        public ExitCell(float x, float y) : base(x, y, new Door(x, y))
        {
        }

        public override void UnBoom()
        {
            base.UnBoom();
            _item = new Door(X, Y);
        }

        private ExitCell(SerializationInfo propertyBag, StreamingContext context):base(propertyBag, context) { }
    }
}