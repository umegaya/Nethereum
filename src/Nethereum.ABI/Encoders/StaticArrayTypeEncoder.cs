using System;
using System.Collections;
using Nethereum.ABI.Util;
using Nethereum.Util;

namespace Nethereum.ABI.Encoders
{
    public class StaticArrayTypeEncoder : ArrayTypeEncoder
    {
        private readonly int arraySize;

        public StaticArrayTypeEncoder(ABIType elementType, int arraySize) : base(elementType)
        {
            this.arraySize = arraySize;
        }

        public override byte[] EncodeList(IList l)
        {
            if (l.Count != arraySize)
                throw new Exception("List size (" + l.Count + ") != " + arraySize);

            return EncodeListCommon(l, null);
        }
    }
}