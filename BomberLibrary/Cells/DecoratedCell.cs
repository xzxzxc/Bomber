using System;
using System.Runtime.Serialization;
using BomberLibrary.Graphics;
using BomberLibrary.Items;
using JetBrains.Annotations;

namespace BomberLibrary.Cells
{
    [DataContract]
    public abstract class DecoratedCell : Cell
    {
        protected Sprite PrimarySprite;
        protected Item _item;
        public Item Item => _item;
        [DataMember]
        private int _itemHashCode;
        [DataMember]
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
        [DataMember]
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

        [OnSerializing]
        [UsedImplicitly]
        private void InitializeItemHashCode(StreamingContext context)
        {
            _itemHashCode = _item?.GetHashCode() ?? -1;
        }

        [OnDeserialized]
        [UsedImplicitly]
        private void OnDeserialize(StreamingContext context)
        { 
            try
            {
                _item = Item.CreateByHashCode(X + GameData.XStandartOffset, Y + GameData.XStandartOffset,
                    _itemHashCode);
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