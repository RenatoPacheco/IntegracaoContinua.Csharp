using IntegracaoContinua.Csharp.Resources;
using System;
using System.Text;
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
            _cpfValue = value?.Trim() ?? string.Empty;
            _cpfIsValid = isValid;
        }

        private readonly string _cpfValue;
        private readonly bool _cpfIsValid;

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
            StringBuilder result = new StringBuilder();
            Random rdm = new Random();
            
            for (int i = 0; i < 9; i++)
                result.Append(rdm.Next(0, 9).ToString());

            return GenerateDigit(result.ToString());
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

        public bool IsValid() => _cpfIsValid;

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
                    return _cpfValue;

                case 'N':
                    return Regex.Replace(_cpfValue, @"[^\d]", string.Empty);

                default:
                    throw new ArgumentException(
                        nameof(format), Resource.TheValueIsNotValid);
            }
        }

        public override int GetHashCode()
        {
            return $"{_cpfValue}:{GetType()}".GetHashCode();
        }

        public bool Equals(CpfType other)
        {
            return _cpfValue == other._cpfValue;
        }

        public override bool Equals(object obj)
        {
            return obj is CpfType value && Equals(value);
        }

        public int CompareTo(CpfType other)
        {
            return _cpfValue.CompareTo(other._cpfValue);
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
            return _cpfValue;
        }

        /// <internalonly/>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(_cpfValue);
        }

        /// <internalonly/>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(_cpfValue);
        }

        /// <internalonly/>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(_cpfValue);
        }

        /// <internalonly/>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(_cpfValue);
        }

        /// <internalonly/>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(_cpfValue);
        }

        /// <internalonly/>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(_cpfValue);
        }

        /// <internalonly/>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(_cpfValue);
        }

        /// <internalonly/>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(_cpfValue);
        }

        /// <internalonly/>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(_cpfValue);
        }

        /// <internalonly/>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(_cpfValue);
        }

        /// <internalonly/>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(_cpfValue);
        }

        /// <internalonly/>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(_cpfValue);
        }

        /// <internalonly/>
        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(_cpfValue);
        }

        /// <internalonly/>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(_cpfValue);
        }

        /// <internalonly/>
        object IConvertible.ToType(System.Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(this, conversionType, provider);
        }

        #endregion
    }
}