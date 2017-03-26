using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fire.Crc32Reverser.Tests
{
    [TestClass]
    public class CrcReverserTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            var sb = new StringBuilder();
            var reverser = new CrcReverser
            {
                PrintLine = s => sb.AppendLine(s),
                MaxDepth = 6
            };

            var crcHex = "0x7C1FD48A";
            uint crcToMatch;
            HexHelper.TryParseCrcHex(crcHex, out crcToMatch);
            reverser.Execute(Constants.DefaultCharSet, crcToMatch);

            Assert.AreEqual("zbbW0" + Environment.NewLine, sb.ToString());
        }
    }
}
