using System;
using Xunit;

namespace IntegracaoContinua.Csharp.Teste
{
    public class UfTypeTest
    {
        [Theory]
        [InlineData("SP", "SP")]
        [InlineData("01234 567", "SP")]
        [InlineData("01234567", "SP")]
        [InlineData(" 01234567 ", "SP")]
        public void Check_format_is_valid(string input, string expected)
        {
            UfType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234   567")]
        [InlineData("SP-89")]
        [InlineData("01234")]
        [InlineData("01234-abc")]
        public void Check_format_is_invalid(string input)
        {
            UfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Theory]
        [InlineData("SP", "SP")]
        [InlineData("01234 567", "SP")]
        [InlineData("01234567", "SP")]
        [InlineData(" 01234567 ", "SP")]
        public void Check_explicit_to_uf_is_valid(string input, string expected)
        {
            UfType test = (UfType)input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("SP", "SP")]
        [InlineData("01234 567", "SP")]
        [InlineData("01234567", "SP")]
        [InlineData(" 01234567 ", "SP")]
        public void Check_explicit_to_string_is_valid(string input, string expected)
        {
            UfType test = (UfType)input;
            string result = (string)test;
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("01234   567")]
        [InlineData("SP-89")]
        [InlineData("01234")]
        [InlineData("01234-abc")]
        public void Check_parse_invalid(string input)
        {
            Assert.Throws<ArgumentException>(() => UfType.Parse(input));
        }
        
        [Theory]
        [InlineData("SP")]
        [InlineData("01234 567")]
        [InlineData("01234567")]
        [InlineData(" 01234567 ")]
        public void Check_parse_valid(string input)
        {
            UfType test = UfType.Parse(input);
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("SP", null, "SP")]
        [InlineData("SP", "", "SP")]
        [InlineData("SP", "d", "SP")]
        [InlineData("SP", "n", "01234567")]
        public void Check_to_string_format(string input, string format, string expected)
        {
            UfType test = new(input);
            Assert.Equal(expected, test.ToString(format));
        }

        [Theory]
        [InlineData("SP", "text")]
        [InlineData("SP", "x")]
        public void Check_to_string_format_exception(string input, string format)
        {
            UfType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }

        [Theory]
        [InlineData("SP", "SP", true)]
        [InlineData("SP", "01234567", true)]
        [InlineData("SP", "01234-000", false)]
        [InlineData("SP", "any value", false)]
        public void Compare_equal(string input, string compare, bool expected)
        {
            UfType inputValue = new(input);
            UfType compareValue = new(compare);
            Assert.Equal(expected, inputValue == compareValue);
            Assert.Equal(expected, inputValue.Equals(compareValue));            
            Assert.Equal(!expected, inputValue != compareValue);
        }

        [Theory]
        [InlineData("SP", "SP", true)]
        [InlineData("SP", "01234567", true)]
        [InlineData("SP", "01234-000", false)]
        [InlineData("SP", "any value", false)]
        public void Compare_get_hash_code(string input, string compare, bool expected)
        {
            UfType inputValue = new(input);
            UfType compareValue = new(compare);
            Assert.Equal(expected, inputValue.GetHashCode() == compareValue.GetHashCode());
        }

        [Fact]
        public void Compare_equal_as_object()
        {
            UfType compare = new ("SP");

            Assert.False(compare.Equals(null));
            Assert.False(compare.Equals(compare.ToString()));
            Assert.False(compare.Equals(compare.ToString("n")));
        }

        [Theory]
        [InlineData("SP", "01234-000", true)]
        [InlineData("SP", "01234-678", false)]
        public void Compare_greater_than(string input, string compare, bool expected)
        {
            UfType inputValue = new(input);
            UfType compareValue = new(compare);
            Assert.Equal(expected, inputValue > compareValue);
            Assert.Equal(!expected, inputValue < compareValue);
        }

        [Fact]
        public void Compare_greater_than_as_object()
        {
            UfType compare = new ("SP");

            Assert.Equal(-1,compare.CompareTo(null));
            Assert.Equal(-1,compare.CompareTo(compare.ToString()));
            Assert.Equal(-1,compare.CompareTo(compare.ToString("n")));
        }

        [Fact]
        public void Check_get_type_code_has_string()
        {
            UfType compare = new ("SP");
            Assert.Equal(TypeCode.String, compare.GetTypeCode());
        }
        
        /*
        [Theory]
        [InlineData("SP", typeof(string), true)]
        [InlineData("any value", typeof(string), true)]
        [InlineData("SP", typeof(bool), false)]
        [InlineData("true", typeof(bool), true)]
        [InlineData("SP", typeof(char), false)]
        [InlineData("n", typeof(char), true)]
        [InlineData("SP", typeof(sbyte), false)]
        [InlineData("1", typeof(sbyte), true)]
        [InlineData("SP", typeof(byte), false)]
        [InlineData("1", typeof(byte), true)]
        [InlineData("SP", typeof(Int16), false)]
        [InlineData("1", typeof(Int16), true)]
        [InlineData("SP", typeof(Int32), false)]
        [InlineData("1", typeof(Int32), true)]
        [InlineData("SP", typeof(Int64), false)]
        [InlineData("1", typeof(Int64), true)]
        [InlineData("SP", typeof(UInt16), false)]
        [InlineData("1", typeof(UInt16), true)]
        [InlineData("SP", typeof(UInt32), false)]
        [InlineData("1", typeof(UInt32), true)]
        [InlineData("SP", typeof(UInt64), false)]
        [InlineData("1", typeof(UInt64), true)]
        [InlineData("SP", typeof(Single), false)]
        [InlineData("1", typeof(Single), true)]
        [InlineData("SP", typeof(Double), false)]
        [InlineData("1", typeof(Double), true)]
        [InlineData("SP", typeof(Decimal), false)]
        [InlineData("1", typeof(Decimal), true)]
        [InlineData("SP", typeof(DateTime), false)]
        [InlineData("2021-01-01", typeof(DateTime), true)]
        public void Check_convertible_success(string input, Type type, bool expectedSuccess)
        {
            UfType test = new(input);
            
            if (!expectedSuccess) {
                Assert.Throws<FormatException>(() => Convert.ChangeType(test, type));
                Assert.Throws<FormatException>(() => (test as IConvertible).ToType(type, null));
            } else {
                Convert.ChangeType(test, type);
                (test as IConvertible).ToType(type, null);
            }
        }*/
    }
}