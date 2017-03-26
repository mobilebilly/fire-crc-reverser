using System.Runtime.CompilerServices;

namespace Fire.Crc32Reverser
{
    public static class Crc32
    {
        private const uint Polynomial = 0xedb88320u;

        private static readonly uint[] Table = CreateTable();

        private static uint[] CreateTable()
        {
            var table = new uint[16*256];

            for (uint i = 0; i < 256; i++)
            {
                var res = i;
                for (var t = 0; t < 16; t++)
                {
                    for (var k = 0; k < 8; k++) res = (res & 1) == 1 ? Polynomial ^ (res >> 1) : res >> 1;
                    table[t*256 + i] = res;
                }
            }

            return table;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint Append(uint crc, byte input)
        {
            return Table[(uint.MaxValue ^ crc ^ input) & 0xff] ^ ((uint.MaxValue ^ crc) >> 8) ^ uint.MaxValue;
        }
    }
}