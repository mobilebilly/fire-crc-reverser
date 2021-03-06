﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fire.Crc32Reverser
{
    public class CrcReverser
    {
        public Encoding Encoding = Encoding.UTF8;
        public int MaxDepth = int.MaxValue;
        public int MinDepthToParallel = 5;
        public Action<string> PrintLine = Console.WriteLine;

        public void Execute(string validChars, uint crcToMatch)
        {
            var charMap = Encoding.GetBytes(validChars);

            var depth = 1;
            while (depth < MaxDepth)
            {
                if (depth < MinDepthToParallel)
                {
                    ProcessDepth(crcToMatch, charMap, depth);
                }
                else
                {
                    var currentDepth = depth;
                    Parallel.For(0, charMap.Length, index => ProcessDepth(crcToMatch, charMap, currentDepth, index));
                }

                depth++;
            }
        }

        private void ProcessDepth(uint crcToMatch, byte[] charMap, int depth, int? mostSigBitVal=null)
        {
            var baseNum = charMap.Length;
            var leaseSigBit = depth - 1;
            var parentCrc = new uint[depth - 1];
            var buffer = new int[depth];
            buffer[0] = mostSigBitVal ?? 0;

            uint crc = 0;
            for (var i = 0; i < depth - 1; i++)
            {
                crc = parentCrc[i] = Crc32.Append(crc, charMap[buffer[i]]);
            }
            
            var currentPos = leaseSigBit;

            while (true)
            {
                var value = Crc32.Append(leaseSigBit == 0 ? 0 : parentCrc[leaseSigBit - 1], charMap[buffer[leaseSigBit]]);

                if (value == crcToMatch)
                {
                    OnMatch(charMap, buffer);
                }

                buffer[leaseSigBit]++;

                while (buffer[currentPos] == baseNum)
                {
                    buffer[currentPos] = 0;

                    if (--currentPos < 0)
                    {
                        return;
                    }

                    if (currentPos == 0 &&  mostSigBitVal.HasValue)
                    {
                        return;
                    }

                    buffer[currentPos]++;
                }

                while (currentPos < leaseSigBit)
                {
                    var initial = currentPos == 0 ? 0 : parentCrc[currentPos - 1];
                    parentCrc[currentPos] = Crc32.Append(initial, charMap[buffer[currentPos]]);
                    currentPos++;
                }
            }
        }

        private void OnMatch(byte[] charMap, int[] buffer)
        {
            var chars = new byte[buffer.Length];

            for (var i = 0; i < buffer.Length; i++)
                chars[i] = charMap[buffer[i]];

            var msg = Encoding.GetString(chars);
            PrintLine(msg);
        }
    }
}