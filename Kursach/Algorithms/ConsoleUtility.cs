using System;

namespace Kursach.Algorithms
{
    internal static class ConsoleUtility
    {
        private const char Block = '■';
        private const string Back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
        public static void WriteProgressBar(double percent, bool update = false)
        {

            if (update)
            {
                Console.Write(Back);
            }

            Console.Write("[");
            var p = (percent / 10f) + .5f;
            for (var i = 0; i < 10; ++i)
            {
                Console.Write(i >= p ? ' ' : Block);
            }
            Console.Write("] {0,2:##0}%", percent);
            Console.ResetColor();
        }
    }
}