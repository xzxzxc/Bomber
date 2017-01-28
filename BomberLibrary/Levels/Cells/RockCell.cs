using System.Runtime.Serialization;

namespace BomberLibrary.Levels.Cells
{
    [DataContract]
    public class RockCell : Cell
    {
        public RockCell(float x, float y) : base(GameData.GraphicsFactory.CreateRockSprite(x, y)) { }

        
    }
}