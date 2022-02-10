using System;
using System.Text;
using System.Text.RegularExpressions;
using IntegracaoContinua.Csharp.Resources;
namespace IntegracaoContinua.Csharp
{
    public struct CepType
        : IFormattable, IComparable,
        IComparable<CepType>, IEquatable<CepType>, IConvertible
    {
        public CepType(string input)
        {
            TryParse(input, out CepType output);
            this = output;
        }

        private CepType(string value, bool isValid)
        {
            _value = value?.Trim() ?? string.Empty;
            _isValid = isValid;
        }

        private readonly string _value;
        private readonly bool _isValid;

        public static explicit operator string(CepType input) => input.ToString();
        public static explicit operator CepType(string input) => new CepType(input);

        /// <summary>
        /// Return value 000.000.000-00
        /// </summary>
        public static readonly CepType Empty = new CepType ("00000-000", false);

        public static CepType Parse(string input)
        {
            if (TryParse(input, out CepType result))
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

        public static bool TryParse(string input, out CepType output)
        {
            input = input?.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                string pattern = @"^\d{5}[\- ]?\d{3}$";
                if (Regex.IsMatch(input, pattern))
                {
                    input = Regex.Replace(input, @"[^\d]", string.Empty);
                    pattern = @"^(\d{5})(\d{3})$";
                    
                    output = new CepType(Regex.Replace(input, pattern, "$1-$2"), true);
                    return true;
                }
            }
            output = new CepType(input, false);
            return false;
        }

        /// <summary>
        /// Generate a valid CPF
        /// </summary>
        /// <returns>A object CepType with a CPF valid</returns>
        public static CepType Generate()
        {
            StringBuilder result = new StringBuilder();
            Random rdm = new Random();
            
            for (int i = 0; i < 8; i++) {
                if (i == 5) {
                    result.Append("-");
                }
                result.Append(rdm.Next(0, 9).ToString());
            }

            return new CepType(result.ToString());
        }
        public bool IsValid() => _isValid;

        public override string ToString()
        {
            return ToString("D", null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            format = format?.Trim()?.ToUpper();

            if (format == null || format == string.Empty)
                format = "D";

            if (format.Length != 1)
                throw new ArgumentException(
                    nameof(format), Resource.TheValueIsNotValid);

            char check = format[0];

            switch (check)
            {
                case 'D':
                    return _value;

                case 'N':
                    return Regex.Replace(_value, @"[^\d]", string.Empty);

                default:
                    throw new ArgumentException(
                        nameof(format), Resource.TheValueIsNotValid);
            }
        }

        public override int GetHashCode()
        {
            return $"{_value}:{GetType()}".GetHashCode();
        }

        public bool Equals(CepType other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is CepType value && Equals(value);
        }

        public int CompareTo(CepType other)
        {
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            return obj is CepType other ? CompareTo(other) : -1;
        }

        public static bool operator ==(CepType left, CepType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CepType left, CepType right)
        {
            return !(left == right);
        }

        public static bool operator >(CepType left, CepType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(CepType left, CepType right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >=(CepType left, CepType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(CepType left, CepType right)
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
        object IConvertible.ToType(System.Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this, conversionType, provider);
        }

        #endregion
    }
}