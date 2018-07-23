using System;
using System.Numerics;
using System.Linq;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Nethereum.Hex.HexConvertors
{
    public class HexBigIntegerBigEndianConvertor : IHexConvertor<BigInteger>
    {
        public string ConvertToHex(BigInteger newValue)
        {
            if (newValue.Sign < 0) throw new Exception("Hex Encoding of Negative BigInteger value is not supported");
            if (newValue == 0) return "0x0";
            byte[] bytes;
            if (BitConverter.IsLittleEndian != false)
                bytes = newValue.ToByteArray().Reverse().ToArray();
            else
                bytes = newValue.ToByteArray().ToArray();

            return "0x" + bytes.ToHexCompact();
            //return newValue.ToHex(false);
        }

        public BigInteger ConvertFromHex(string hex)
        {
            return hex.HexToBigInteger(false);
        }
    }
}