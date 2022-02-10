using IntegracaoContinua.Csharp.Resources;
using System;
using System.Text.RegularExpressions;

namespace IntegracaoContinua.Csharp
{
    public struct UfType
        : IFormattable, IComparable,
        IComparable<UfType>, IEquatable<UfType>, IConvertible
    {
        public UfType(string input)
        {
            TryParse(input, out UfType output);
            this = output;
        }        

        private UfType(string value, bool isValid)
        {
            _ufValue = value?.Trim() ?? string.Empty;
            _ufIsValid = isValid;
        }

        private readonly string _ufValue;
        private readonly bool _ufIsValid;

        public static explicit operator string(UfType input) => input.ToString();
        public static explicit operator UfType(string input) => new UfType(input);

        /// <summary>
        /// Return value 000.000.000-00
        /// </summary>
        public static readonly UfType Empty = new UfType("", false);

        public static UfType Parse(string input)
        {
            if (TryParse(input, out UfType result))
            {
                return result;
            }
            else
            {
                if (input == null)
                    throw new ArgumentException(
                        nameof(input), Resource.ItCannotBeNull);
                else
                    throw new ArgumentException(
                        nameof(input), Resource.ItIsNotInAValidFormat);
            }
        }

        public static bool TryParse(string input, out UfType output)
        {
            input = input?.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                string pattern = @"^[a-zA-Z]{2}$";
                if (Regex.IsMatch(input, pattern))
                {
                    output = new UfType(input.ToUpper(), true);
                    return true;
                }
            }
            output = new UfType(input, false);
            return false;
        }
        
        public bool IsValid() => _ufIsValid;

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == string.Empty || format == null)
                return _ufValue;
            else
                throw new ArgumentException(
                    nameof(format), Resource.TheValueIsNotValid);
        }

        public override int GetHashCode()
        {
            return $"{_ufValue}:{GetType()}".GetHashCode();
        }

        public bool Equals(UfType other)
        {
            return _ufValue == other._ufValue;
        }

        public override bool Equals(object obj)
        {
            return obj is UfType value && Equals(value);
        }

        public int CompareTo(UfType other)
        {
            return _ufValue.CompareTo(other._ufValue);
        }

        public int CompareTo(object obj)
        {
            return obj is UfType other ? CompareTo(other) : -1;
        }

        public static bool operator ==(UfType left, UfType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UfType left, UfType right)
        {
            return !(left == right);
        }

        public static bool operator >(UfType left, UfType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(UfType left, UfType right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >=(UfType left, UfType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(UfType left, UfType right)
        {
            return left < right || left == right;
        }

        #region IConvertible implementation

        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        /// <internalonly/>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return _ufValue;
        }

        /// <internalonly/>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_ufValue);
        }

        /// <internalonly/>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_ufValue);
        }

        /// <internalonly/>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_ufValue);
        }

        /// <internalonly/>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_ufValue);
        }

        /// <internalonly/>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_ufValue);
        }

        /// <internalonly/>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_ufValue);
        }

        /// <internalonly/>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_ufValue);
        }

        /// <internalonly/>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_ufValue);
        }

        /// <internalonly/>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_ufValue);
        }

        /// <internalonly/>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_ufValue);
        }

        /// <internalonly/>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_ufValue);
        }

        /// <internalonly/>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_ufValue);
        }

        /// <internalonly/>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_ufValue);
        }

        /// <internalonly/>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_ufValue);
        }

        /// <internalonly/>
        object IConvertible.ToType(System.Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this, conversionType, provider);
        }

        #endregion
    }
}