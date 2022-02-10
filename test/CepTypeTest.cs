using System;
using Xunit;

namespace IntegracaoContinua.Csharp.Teste
{
    public class CepTypeTest
    {
        [Theory]
        [InlineData("01234-567", "01234-567")]
        [InlineData("01234 567", "01234-567")]
        [InlineData("01234567", "01234-567")]
        [InlineData(" 01234567 ", "01234-567")]
        public void Check_format_is_valid(string input, string expected)
        {
            CepType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234   567")]
        [InlineData("01234-567-89")]
        [InlineData("01234")]
        [InlineData("01234-abc")]
        public void Check_format_is_invalid(string input)
        {
            CepType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Fact]
        public void Check_generate_is_valid()
        {
            string input = CepType.Generate().ToString();
            CepType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("01234-567", "01234-567")]
        [InlineData("01234 567", "01234-567")]
        [InlineData("01234567", "01234-567")]
        [InlineData(" 01234567 ", "01234-567")]
        public void Check_explicit_to_cep_is_valid(string input, string expected)
        {
            CepType test = (CepType)input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("01234-567", "01234-567")]
        [InlineData("01234 567", "01234-567")]
        [InlineData("01234567", "01234-567")]
        [InlineData(" 01234567 ", "01234-567")]
        public void Check_explicit_to_string_is_valid(string input, string expected)
        {
            CepType test = (CepType)input;
            string result = (string)test;
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234   567")]
        [InlineData("01234-567-89")]
        [InlineData("01234")]
        [InlineData("01234-abc")]
        public void Check_parse_invalid(string input)
        {
            Assert.Throws<ArgumentException>(() => CepType.Parse(input));
        }
        
        [Theory]
        [InlineData("01234-567")]
        [InlineData("01234 567")]
        [InlineData("01234567")]
        [InlineData(" 01234567 ")]
        public void Check_parse_valid(string input)
        {
            CepType test = CepType.Parse(input);
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("01234-567", null, "01234-567")]
        [InlineData("01234-567", "", "01234-567")]
        [InlineData("01234-567", "d", "01234-567")]
        [InlineData("01234-567", "n", "01234567")]
        public void Check_to_string_format(string input, string format, string expected)
        {
            CepType test = new(input);
            Assert.Equal(expected, test.ToString(format));
        }

        [Theory]
        [InlineData("01234-567", "text")]
        [InlineData("01234-567", "x")]
        public void Check_to_string_format_exception(string input, string format)
        {
            CepType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }

        [Theory]
        [InlineData("01234-567", "01234-567", true)]
        [InlineData("01234-567", "01234567", true)]
        [InlineData("01234-567", "01234-000", false)]
        [InlineData("01234-567", "any value", false)]
        public void Compare_equal(string input, string compare, bool expected)
        {
            CepType inputValue = new(input);
            CepType compareValue = new(compare);
            Assert.Equal(expected, inputValue == compareValue);
            Assert.Equal(expected, inputValue.Equals(compareValue));            
            Assert.Equal(!expected, inputValue != compareValue);
        }

        [Theory]
        [InlineData("01234-567", "01234-567", true)]
        [InlineData("01234-567", "01234567", true)]
        [InlineData("01234-567", "01234-000", false)]
        [InlineData("01234-567", "any value", false)]
        public void Compare_get_hash_code(string input, string compare, bool expected)
        {
            CepType inputValue = new(input);
            CepType compareValue = new(compare);
            Assert.Equal(expected, inputValue.GetHashCode() == compareValue.GetHashCode());
        }

        [Fact]
        public void Compare_equal_as_object()
        {
            CepType compare = new ("01234-567");

            Assert.False(compare.Equals(null));
            Assert.False(compare.Equals(compare.ToString()));
            Assert.False(compare.Equals(compare.ToString("n")));
        }

        [Theory]
        [InlineData("01234-567", "01234-000", true)]
        [InlineData("01234-567", "01234-678", false)]
        public void Compare_greater_than(string input, string compare, bool expected)
        {
            CepType inputValue = new(input);
            CepType compareValue = new(compare);
            Assert.Equal(expected, inputValue > compareValue);
            Assert.Equal(!expected, inputValue < compareValue);
        }

        [Theory]
        [InlineData("01234-567", "01234-000", true)]
        [InlineData("01234-567", "01234-567", true)]
        [InlineData("01234-567", "01234-678", false)]
        public void Compare_greater_or_qual_than(string input, string compare, bool expected)
        {
            CepType inputValue = new(input);
            CepType compareValue = new(compare);
            Assert.Equal(expected, inputValue >= compareValue);
        }        

        [Theory]
        [InlineData("01234-000", "01234-567", true)]
        [InlineData("01234-000", "01234-000", true)]
        [InlineData("01234-678", "01234-567", false)]
        public void Compare_small_or_qual_than(string input, string compare, bool expected)
        {
            CepType inputValue = new(input);
            CepType compareValue = new(compare);
            Assert.Equal(expected, inputValue <= compareValue);
        }

        [Fact]
        public void Compare_greater_than_as_object()
        {
            CepType compare = new ("01234-567");

            Assert.Equal(-1,compare.CompareTo(null));
            Assert.Equal(-1,compare.CompareTo(compare.ToString()));
            Assert.Equal(-1,compare.CompareTo(compare.ToString("n")));
        }

        [Fact]
        public void Check_get_type_code_has_string()
        {
            CepType compare = new ("01234-567");
            Assert.Equal(TypeCode.String, compare.GetTypeCode());
        }
        

        [Theory]
        [InlineData("01234-567", typeof(string), true)]
        [InlineData("any value", typeof(string), true)]
        [InlineData("01234-567", typeof(bool), false)]
        [InlineData("true", typeof(bool), true)]
        [InlineData("01234-567", typeof(char), false)]
        [InlineData("n", typeof(char), true)]
        [InlineData("01234-567", typeof(sbyte), false)]
        [InlineData("1", typeof(sbyte), true)]
        [InlineData("01234-567", typeof(byte), false)]
        [InlineData("1", typeof(byte), true)]
        [InlineData("01234-567", typeof(Int16), false)]
        [InlineData("1", typeof(Int16), true)]
        [InlineData("01234-567", typeof(Int32), false)]
        [InlineData("1", typeof(Int32), true)]
        [InlineData("01234-567", typeof(Int64), false)]
        [InlineData("1", typeof(Int64), true)]
        [InlineData("01234-567", typeof(UInt16), false)]
        [InlineData("1", typeof(UInt16), true)]
        [InlineData("01234-567", typeof(UInt32), false)]
        [InlineData("1", typeof(UInt32), true)]
        [InlineData("01234-567", typeof(UInt64), false)]
        [InlineData("1", typeof(UInt64), true)]
        [InlineData("01234-567", typeof(Single), false)]
        [InlineData("1", typeof(Single), true)]
        [InlineData("01234-567", typeof(Double), false)]
        [InlineData("1", typeof(Double), true)]
        [InlineData("01234-567", typeof(Decimal), false)]
        [InlineData("1", typeof(Decimal), true)]
        [InlineData("01234-567", typeof(DateTime), false)]
        [InlineData("2021-01-01", typeof(DateTime), true)]
        public void Check_convertible_success(string input, Type type, bool expectedSuccess)
        {
            CepType test = new(input);
            
            if (!expectedSuccess) {
                Assert.Throws<FormatException>(() => Convert.ChangeType(test, type));
                Assert.Throws<FormatException>(() => (test as IConvertible).ToType(type, null));
            } else {
                Convert.ChangeType(test, type);
                (test as IConvertible).ToType(type, null);
            }
        }
    }
}