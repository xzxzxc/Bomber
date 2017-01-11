using System;
using BomberLib;

namespace BomberWindowsGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (BomerWindowsGame game = new BomerWindowsGame())
            {
                game.Run();
            }
        }
    }
#endif
}

