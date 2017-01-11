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

        public override float X
        {
            get
            {
                return base.X;
            }
            set
            {
                base.X = value;
                if (_item != null)_item.X = value + GameData.XStandartOffset;
                if (PrimarySprite != null) PrimarySprite.X = value;
            }
        }

        public override float Y {
            get
            {
                return base.Y;
            }
            set
            {
                base.Y = value;
                if (_item != null) _item.Y = value + GameData.YStandartOffset;
                if (PrimarySprite != null) PrimarySprite.Y = value;
            }
        }

        protected DecoratedCell(Sprite sprite, Item hidenItem) : base(GameData.GraphicsFactory.CreateGrassSprite(sprite.X, sprite.Y))
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
            Sprite = GameData.GraphicsFactory.CreateGrassSprite(propertyBag.GetSingle("X"), propertyBag.GetSingle("Y"));
            try
            {
                _item = Item.CreateByHashCode(propertyBag.GetSingle("X") + GameData.XStandartOffset,
                    propertyBag.GetSingle("Y") + GameData.YStandartOffset, propertyBag.GetInt32("ItemHashCode"));
            }
            catch (ArgumentOutOfRangeException)
            {
                // ignored
            }
        }

        public override void Boom()
        {
            base.Boom();
            IsMovable = true;
            ChangeSprite(GameData.GraphicsFactory.CreateGrassAdterBoomSprite(X, Y));
            PrimarySprite = null;
            Sprite.StartDrawingAnimationToEnd(0);
        }

        public void ClearItem()
        {
            _item = null;
        }

        public override void UnBoom()
        {
            base.UnBoom();
            IsMovable = false;
            ChangeSprite(GameData.GraphicsFactory.CreateGrassSprite(X, Y));
        }

        public override void Draw()
        {
            base.Draw();
            Item?.Draw();
            PrimarySprite?.Draw();
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