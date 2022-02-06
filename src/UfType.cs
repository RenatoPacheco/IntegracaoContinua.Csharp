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
            if (!IsValid())
                _value = input?.Trim() ?? string.Empty;
        }

        private string _value;
        private bool _isValid;

        public static explicit operator string(UfType input) => input.ToString();
        public static explicit operator UfType(string input) => new UfType(input);

        /// <summary>
        /// Return value 000.000.000-00
        /// </summary>
        public static readonly UfType Empty = new UfType { _value = "000.000.000-00" };

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
                    output = new UfType
                    {
                        _value = input.ToUpper(),
                        _isValid = true
                    };
                    return true;
                }
            }
            output = Empty;
            return false;
        }
        
        public bool IsValid() => _isValid;

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
            return _value;
        }

        public override int GetHashCode()
        {
            return $"{_value}:{GetType()}".GetHashCode();
        }

        public bool Equals(UfType other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is UfType value && Equals(value);
        }

        public int CompareTo(UfType other)
        {
            return _value.CompareTo(other._value);
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

        #region IConvertible implementation

        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

        /// <internalonly/>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return _value;
        }

        /// <internalonly/>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_value);
        }

        /// <internalonly/>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_value);
        }

        /// <internalonly/>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_value);
        }

        /// <internalonly/>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_value);
        }

        /// <internalonly/>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_value);
        }

        /// <internalonly/>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_value);
        }

        /// <internalonly/>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_value);
        }

        /// <internalonly/>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_value);
        }

        /// <internalonly/>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_value);
        }

        /// <internalonly/>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_value);
        }

        /// <internalonly/>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_value);
        }

        /// <internalonly/>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_value);
        }

        /// <internalonly/>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_value);
        }

        /// <internalonly/>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_value);
        }

        /// <internalonly/>
        object IConvertible.ToType(System.Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(this, type, provider);
        }

        #endregion
    }
}