using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BomberWindowsGame.Graphics
{
    public static class ColorsManager
    {
        private static Dictionary<string, Color> _colors = new Dictionary<string, Color>
        { {"White", Color.White}, {"Black", Color.Black},
            { "TransparentWhite", Color.White * 0.5f } };

        public static Color FromName(string name)
        {
            return _colors[name];
        }
    }
}