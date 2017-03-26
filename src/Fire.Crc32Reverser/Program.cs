using System;
using System.Globalization;

namespace Fire.Crc32Reverser
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Fire.Crc32Reverser");

            if ((args.Length > 2) || (args.Length == 0))
            {
                PrintUsage();
                return;
            }

            var crcHex = args[0];

            uint crcToMatch;
            if (!HexHelper.TryParseCrcHex(crcHex, out crcToMatch))
            {
                Console.WriteLine("crcHex is not in correct format: " + crcHex);
                PrintUsage();
                return;
            }

            string validChars;

            if (args.Length == 2)
            {
                validChars = args[1];

                if (validChars.Length == 0)
                {
                    Console.WriteLine("validChars cannot be empty");
                    PrintUsage();
                    return;
                }
            }
            else
            {
                validChars = Constants.DefaultCharSet;
            }

            var reverser = new CrcReverser();
            reverser.Execute(validChars, crcToMatch);
        }

        private static void PrintUsage()
        {
            Console.WriteLine("usage: Fire.Crc32Reverser.exe crcHex [validChars]");
            Console.WriteLine(" crcHex     : crc value in hex (e.g. 7C1FD48A or 0x7C1FD48A)");
            Console.WriteLine(" validChars : character allowed (e.g. 1234567890ABCabc-[]+=) (default: a-zA-Z0-9)");
        }
    }
}