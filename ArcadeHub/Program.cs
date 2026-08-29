using System;
using ArcadeHub.Managers;

namespace ArcadeHub
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ArcadeManager())
                game.Run();
        }
    }
}
