using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Nethereum.Hex.HexConvertors.Extensions;

namespace Nethereum.ABI.Decoders
{
    public class IntTypeDecoder : TypeDecoder
    {
        private readonly bool _signed;

        public IntTypeDecoder(bool signed)
        {
            _signed = signed;
        }

        public IntTypeDecoder():this(false)
        {
            
        }

        public override object Decode(byte[] encoded, Type type)
        {
            if (type == typeof(byte))
                return DecodeByte(encoded);

            if (type == typeof(sbyte))
                return DecodeSbyte(encoded);

            if (type == typeof(short))
                return DecodeShort(encoded);

            if (type == typeof(ushort))
                return DecodeUShort(encoded);

            if (type == typeof(int))
                return DecodeInt(encoded);

            if (type.GetTypeInfo().IsEnum)
            {
                var val = DecodeInt(encoded);
                return Enum.ToObject(type, val);
            }

            if (type == typeof(uint))
                return DecodeUInt(encoded);

            if (type == typeof(long))
                return DecodeLong(encoded);

            if (type == typeof(ulong))
                return DecodeULong(encoded);

            if ((type == typeof(BigInteger)) || (type == typeof(object)))
                return DecodeBigInteger(encoded);

            throw new NotSupportedException(type + " is not a supported decoding type for IntType");
        }

        public BigInteger DecodeBigInteger(string hexString)
        {
            if (!hexString.StartsWith("0x"))
                hexString = "0x" + hexString;

            return DecodeBigInteger(hexString.HexToByteArray());
        }

        private bool IsZeroFilled(byte[] bs) {
            int byteLength = bs.Length;
            int base64Length = byteLength >> 3;
            int remainingBytes = byteLength & 0x7;
            unsafe {
                fixed (byte* pByteBuffer = bs) {
                    ulong* pBuffer = (ulong*)pByteBuffer;
                    int index = 0;
                    while (index < base64Length) {
                        if (pBuffer[index] != 0) {
                            return false;
                        }
                        index++;
                    }
                }
            }
            int startRemainingBytes = (base64Length << 3);
            for (int i = startRemainingBytes; i < byteLength; i++) {
                if (bs[i] != 0) {
                    return false;
                }
            }
            return true;
        }

        public BigInteger DecodeBigInteger(byte[] encoded)
        {
            var negative = false;
            if (_signed) negative = encoded.First() == 0xFF;

            if (!_signed)
            {
                var listEncoded = encoded.ToList();
                listEncoded.Insert(0,0x00);
                encoded = listEncoded.ToArray();
            }

            if (BitConverter.IsLittleEndian)
                encoded = encoded.Reverse().ToArray();

            if (negative)
                return new BigInteger(encoded) -
                       new BigInteger(
                           "0xffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff".HexToByteArray()) - 1;

            if (IsZeroFilled(encoded)) {
                //more than 2 bytes zero filled array causes IndexOutOfRangeException below
                encoded = new byte[] { 0 };
            }

            return new BigInteger(encoded);
        }

        public byte DecodeByte(byte[] encoded)
        {
            return (byte)DecodeBigInteger(encoded);
        }

        public sbyte DecodeSbyte(byte[] encoded)
        {
            return (sbyte)DecodeBigInteger(encoded);
        }

        public short DecodeShort(byte[] encoded)
        {
            return (short)DecodeBigInteger(encoded);
        }

        public ushort DecodeUShort(byte[] encoded)
        {
            return (ushort)DecodeBigInteger(encoded);
        }

        public int DecodeInt(byte[] encoded)
        {
            return (int) DecodeBigInteger(encoded);
        }

        public long DecodeLong(byte[] encoded)
        {
            return (long) DecodeBigInteger(encoded);
        }

        public uint DecodeUInt(byte[] encoded)
        {
            return (uint) DecodeBigInteger(encoded);
        }

        public ulong DecodeULong(byte[] encoded)
        {
            return (ulong) DecodeBigInteger(encoded);
        }

        public override Type GetDefaultDecodingType()
        {
            return typeof(BigInteger);
        }

        public override bool IsSupportedType(Type type)
        {
            return (type == typeof(int)) || (type == typeof(uint)) || 
                   (type == typeof(ulong)) || (type == typeof(long))  ||
                   (type == typeof(short)) || (type == typeof(ushort)) ||
                   (type == typeof(byte)) || (type == typeof(sbyte)) ||
                   (type == typeof(BigInteger)) || (type == typeof(object))
                   || type.GetTypeInfo().IsEnum;
        }
    }
}