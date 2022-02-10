using IntegracaoContinua.Csharp.Resources;
using System;
using System.Text.RegularExpressions;

namespace IntegracaoContinua.Csharp
{
    public struct CpfType
        : IFormattable, IComparable,
        IComparable<CpfType>, IEquatable<CpfType>, IConvertible
    {
        public CpfType(string input)
        {
            TryParse(input, out CpfType output);
            this = output;
        }

        private CpfType(string value, bool isValid)
        {
            _value = value?.Trim() ?? string.Empty;
            _isValid = isValid;
        }

        private readonly string _value;
        private readonly bool _isValid;

        public static explicit operator string(CpfType input) => input.ToString();
        public static explicit operator CpfType(string input) => new CpfType(input);

        /// <summary>
        /// Return value 000.000.000-00
        /// </summary>
        public static readonly CpfType Empty = new CpfType("000.000.000-00", false);

        public static CpfType Parse(string input)
        {
            if (TryParse(input, out CpfType result))
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

        public static bool TryParse(string input, out CpfType output)
        {
            input = input?.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                string pattern = @"^\d{3}[\. ]?\d{3}[\. ]?\d{3}[\- ]?\d{2}$";
                if (Regex.IsMatch(input, pattern))
                {
                    string newValue = Regex.Replace(input, @"[^\d]", string.Empty);
                    output = GenerateDigit(newValue.Substring(0, 9));

                    if (output.ToString("N") == newValue)
                        return true;
                }
            }
            output = new CpfType(input, false);
            return false;
        }

        /// <summary>
        /// Generate a valid CPF
        /// </summary>
        /// <returns>A object CpfType with a CPF valid</returns>
        public static CpfType Generate()
        {
            string partialValue = string.Empty;
            for (int i = 0; i < 9; i++)
                partialValue += new Random().Next(0, 9).ToString();

            return GenerateDigit(partialValue);
        }

        private static CpfType GenerateDigit(string partialValue)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            tempCpf = partialValue;
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            string pattern = @"^(\d{3})(\d{3})(\d{3})(\d{2})$";
            string tempValue = Regex.Replace(
                partialValue + digito, pattern, "$1.$2.$3-$4");

            return new CpfType(tempValue, true);
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

        public bool Equals(CpfType other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            return obj is CpfType value && Equals(value);
        }

        public int CompareTo(CpfType other)
        {
            return _value.CompareTo(other._value);
        }

        public int CompareTo(object obj)
        {
            return obj is CpfType other ? CompareTo(other) : -1;
        }

        public static bool operator ==(CpfType left, CpfType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CpfType left, CpfType right)
        {
            return !(left == right);
        }

        public static bool operator >(CpfType left, CpfType right)
        {
            return left.CompareTo(right) == 1;
        }

        public static bool operator <(CpfType left, CpfType right)
        {
            return left.CompareTo(right) == -1;
        }
        

        public static bool operator >=(CpfType left, CpfType right)
        {
            return left > right || left == right;
        }

        public static bool operator <=(CpfType left, CpfType right)
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
        object IConvertible.ToType(System.Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(this, type, provider);
        }

        #endregion
    }
}