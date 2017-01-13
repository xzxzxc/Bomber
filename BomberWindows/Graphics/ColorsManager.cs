using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BomberWindows.Graphics
{
    public static class ColorsManager
    {
        private static readonly Dictionary<string, Color> Colors = new Dictionary<string, Color>
        { {"White", Color.White}, {"Black", Color.Black},
            { "TransparentWhite", Color.White * 0.5f } };

        public static Color FromName(string name)
        {
            return Colors[name];
        }
    }
}