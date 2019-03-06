using System;

namespace UtilitiesMisc
{
    public class ConsoleUtils
    {
        public static void Print(string text, MessageOriginColor messageOrigin, bool newLineAfterPrint)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            ConsoleColor color = (ConsoleColor)messageOrigin;
            Console.ForegroundColor = color;
            if (newLineAfterPrint)
            {
                Console.WriteLine(text);
            }

            else
            {
                Console.Write(text);
            }
            Console.ForegroundColor = currentColor;
        }
    }
}
