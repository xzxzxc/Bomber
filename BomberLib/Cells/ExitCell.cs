using System;
using System.Runtime.Serialization;
using BomberLib.Items;

namespace BomberLib.Cells
{
    [Serializable]
    public class ExitCell : TreeCell
    {
        public ExitCell() : base(new Door())
        {
        }

        public override void UnBoom()
        {
            base.UnBoom();
            _item = new Door();
        }

        private ExitCell(SerializationInfo propertyBag, StreamingContext context):base(propertyBag, context) { }
    }
}