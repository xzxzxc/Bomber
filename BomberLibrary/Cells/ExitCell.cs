using System.Runtime.Serialization;
using BomberLibrary.Items;

namespace BomberLibrary.Cells
{
    [DataContract]
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

        //private ExitCell(SerializationInfo propertyBag, StreamingContext context):base(propertyBag, context) { }
    }
}