using System;
using System.Collections;
using System.Linq;
using Nethereum.Util;
using Nethereum.ABI.Util;

namespace Nethereum.ABI.Encoders
{
    public abstract class ArrayTypeEncoder : ITypeEncoder
    {
        private readonly ABIType _elementType;

        public ArrayTypeEncoder(ABIType elementType) {
            this._elementType = elementType;
        }
        public byte[] Encode(object value)
        {
            var array = value as IEnumerable;
            if ((array != null) && !(value is string))
                return EncodeList(array.Cast<object>().ToList());
            throw new Exception("Array value expected for type");
        }

        public abstract byte[] EncodeList(IList l);

        protected byte[] EncodeListCommon(IList l, IntTypeEncoder length_enc)
        {
            int start_idx = 0;
            byte[][] elems;
            if (_elementType.IsDynamic()) {
                if (length_enc != null) {
                    elems = new byte[2 * l.Count + 1][];
                    elems[0] = length_enc.EncodeInt(l.Count);
                    start_idx = 1;
                } else {
                    elems = new byte[2 * l.Count][];
                    length_enc = new IntTypeEncoder();
                }
                int prev_offset = l.Count * 32;
                elems[start_idx] = length_enc.EncodeInt(prev_offset);
                elems[l.Count + start_idx] = _elementType.Encode(l[0]);
                for (var i = 1; i < l.Count; i++) {
                    var enc = _elementType.Encode(l[i]);
                    elems[i + start_idx] = length_enc.EncodeInt(prev_offset += enc.Length);
                    elems[l.Count + i + start_idx] = enc;
                }
            } else {
                if (length_enc != null) {
                    elems = new byte[l.Count + 1][];
                    elems[0] = length_enc.EncodeInt(l.Count);;
                    start_idx = 1;
                } else {
                    elems = new byte[l.Count][];
                }
                for (var i = 0; i < l.Count; i++)
                    elems[i + start_idx] = _elementType.Encode(l[i]);
            }
            return ByteUtil.Merge(elems);
        }
    }
}