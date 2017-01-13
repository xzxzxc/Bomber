using System.Runtime.Serialization;
using BomberLibrary.Items;

namespace BomberLibrary.Cells
{
    [DataContract]
    public class BombCell:TreeCell
    {
        [DataMember]
        private readonly int _bombNum;

        public BombCell(float x, float y, int bombNum) : base(x, y, bombNum == default(int)?
            null:new BombItem(x, y, bombNum))
        {
            _bombNum = bombNum;
        }

        public override void UnBoom()
        {
            base.UnBoom();
            _item = new BombItem(X, Y, _bombNum);
        }


        /*
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("BombNum", _bombNum);
        }

        private BombCell(SerializationInfo propertyBag, StreamingContext context):base(propertyBag, context)
        {
            _bombNum = propertyBag.GetInt32("BombNum");
        }*/
    }
}