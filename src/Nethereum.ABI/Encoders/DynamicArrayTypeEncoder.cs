using System.Collections;
using Nethereum.ABI.Util;
using Nethereum.Util;

namespace Nethereum.ABI.Encoders
{
    public class DynamicArrayTypeEncoder : ArrayTypeEncoder
    {
        private readonly IntTypeEncoder _intTypeEncoder;

        public DynamicArrayTypeEncoder(ABIType elementType) : base(elementType)
        {
            _intTypeEncoder = new IntTypeEncoder();
        }

        public override byte[] EncodeList(IList l)
        {
            return EncodeListCommon(l, _intTypeEncoder);
        }
    }
}