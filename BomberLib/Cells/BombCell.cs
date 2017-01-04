using System;
using System.Runtime.Serialization;
using BomberLib.Items;

namespace BomberLib.Cells
{
    [Serializable]
    public class BombCell:TreeCell
    {
        private readonly int _bombNum;

        public BombCell(int bombNum) : base(new BombItem(bombNum))
        {
            _bombNum = bombNum;
        }

        public override void UnBoom()
        {
            base.UnBoom();
            _item = new BombItem(_bombNum);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("BombNum", _bombNum);
        }

        private BombCell(SerializationInfo propertyBag, StreamingContext context):base(propertyBag, context)
        {
            _bombNum = propertyBag.GetInt32("BombNum");
        }
    }
}