using System;
using System.Globalization;

namespace Fire.Crc32Reverser
{
    public class HexHelper
    {
        public static bool TryParseCrcHex(string crcHex, out uint crcToMatch)
        {
            if (crcHex.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                crcHex = crcHex.Substring(2);
            }

            return uint.TryParse(crcHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out crcToMatch);
        }
    }
}