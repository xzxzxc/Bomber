using System;
using System.Runtime.Serialization;
using BomberLib.Graphics;
using BomberLib.Items;

namespace BomberLib.Cells
{
    [Serializable]
    public abstract class DecoratedCell : Cell
    {
        [NonSerialized]
        protected Sprite PrimarySprite;
        protected Item _item;
        public Item Item => _item;

        protected DecoratedCell(Sprite sprite, Item hidenItem) : base(GameData.GraphicsFactory.CreateGrassSprite())
        {
            PrimarySprite = sprite;
            _item = hidenItem;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (_item != null)
                info.AddValue("ItemHashCode", _item.GetHashCode());
            else
                info.AddValue("ItemHashCode", -1);
        }

        protected DecoratedCell(SerializationInfo propertyBag, StreamingContext context) : base(propertyBag, context)
        {
            _sprite = GameData.GraphicsFactory.CreateGrassSprite();
            X = propertyBag.GetSingle("X");
            Y = propertyBag.GetSingle("Y");
            try
            {
                _item = Item.CreateByHashCode(propertyBag.GetInt32("ItemHashCode"));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void Boom()
        {
            base.Boom();
            PrimarySprite = null;
            _isMovable = true;
            ChangeSprite(GameData.GraphicsFactory.CreateGrassAdterBoomSprite());
            _sprite.DrawAnimatonOneTime(0, X, Y);
        }

        public void ClearItem()
        {
            _item = null;
        }

        public override void UnBoom()
        {
            base.UnBoom();
            _isMovable = false;
            ChangeSprite(GameData.GraphicsFactory.CreateGrassSprite());
        }

        protected override void Draw(float x, float y)
        {
            base.Draw(x, y);
            Item?.Draw(x + 0.1f * GameData.CellWidth, y + 0.1f * GameData.CellHeight);
            PrimarySprite?.Draw(x, y);
        }

        public override void MoveLeft(float speed)
        {
            base.MoveLeft(speed);
            Item?.MoveLeft(speed);
            PrimarySprite?.MoveLeft(speed);
        }

        public override void MoveRight(float speed)
        {
            base.MoveRight(speed);
            Item?.MoveRight(speed);
            PrimarySprite?.MoveRight(speed);
        }

        public override void MoveUp(float speed)
        {
            base.MoveUp(speed);
            Item?.MoveUp(speed);
            PrimarySprite?.MoveUp(speed);
        }

        public override void MoveDown(float speed)
        {
            base.MoveDown(speed);
            Item?.MoveDown(speed);
            PrimarySprite?.MoveDown(speed);
        }
    }
}