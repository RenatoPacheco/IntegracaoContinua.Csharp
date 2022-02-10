using System;
using System.Text;
using System.Text.RegularExpressions;
using IntegracaoContinua.Csharp.Resources;

namespace IntegracaoContinua.Csharp
{
    public struct PisType
        : IFormattable, IComparable,
        IComparable<PisType>, IEquatable<PisType>, IConvertible
    {
        public PisType(string input)
        {
            TryParse(input, out PisType output);
            this = output;
        }        

        private PisType(string value, bool isValid)
        {
            _value = value?.Trim() ?? string.Empty;
            _isValid = isValid;
        }

        private readonly string _value;
        private readonly bool _isValid;

        public static explicit operator string(PisType input) => input.ToString();
        public static explicit operator PisType(string input) => new PisType(input);

        /// <summary>
        /// Return value 000.00000.00-0
        /// </summary>
        public static readonly PisType Empty = new PisType("000.00000.00-0", false);

        public static PisType Parse(string input)
        {
            if (TryParse(input, out PisType result))
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

        public static bool TryParse(string input, out PisType output)
        {
            input = input?.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                string pattern = @"^\d{3}[\. ]?\d{5}[\. ]?\d{2}[\- ]?\d{1}$";
                if (Regex.IsMatch(input, pattern))
                {
                    string newValue = Regex.Replace(input, @"[^\d]", string.Empty);
                    output = GenerateDigit(newValue.Substring(0, 10));

                    if (output.ToString("N") == newValue)
                        return true;
                }
            }
            output = new PisType(input, false);
            return false;
        }

        /// <summary>
        /// Generate a valid PIS
        /// </summary>
        /// <returns>A object PisType with a PIS valid</returns>
        public static PisType Generate()
        {
            StringBuilder result = new StringBuilder();
            Random rdm = new Random();
            
            for (int i = 0; i < 10; i++)
                result.Append(rdm.Next(0, 9).ToString());

            return GenerateDigit(result.ToString());
        }

        private static PisType GenerateDigit(string partialPis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(partialPis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string pattern = @"^(\d{3})(\d{5})(\d{2})(\d{1})$";
            string tempValue = Regex.Replace(
                    partialPis + resto, pattern, "$1.$2.$3-$4");
            
            return new PisType(tempValue, true);
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

        public bool Equals(PisType other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is PisType phone && Equals(phone);
        }

        public int CompareTo(PisType other)
        {
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            return obj is PisType other ? CompareTo(other) : -1;
        }

        public static bool operator ==(PisType left, PisType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PisType left, PisType right)
        {
            return !(left == right);
        }

        public static bool operator >(PisType left, PisType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(PisType left, PisType right)
        {
            return left.CompareTo(right) == -1;
        }

        public static bool operator >=(PisType left, PisType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(PisType left, PisType right)
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