using System;
using Xunit;

namespace IntegracaoContinua.Csharp.Teste
{
    public class PisTypeTest
    {
        [Theory]
        [InlineData("844.34617.73-4", "844.34617.73-4")]
        [InlineData("844 34617 73 4", "844.34617.73-4")]
        [InlineData("84434617734", "844.34617.73-4")]
        [InlineData(" 84434617734 ", "844.34617.73-4")]
        public void Check_format_is_valid(string input, string expected)
        {
            PisType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("844   34617   73     4")]
        [InlineData("844.34617.73-48")]
        [InlineData("84434617")]
        [InlineData("844.34617-abc")]
        public void Check_format_is_invalid(string input)
        {
            PisType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Fact]
        public void Check_generate_is_valid()
        {
            string input = PisType.Generate().ToString();
            PisType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("844.34617.73-4", "844.34617.73-4")]
        [InlineData("844 34617 73 4", "844.34617.73-4")]
        [InlineData("84434617734", "844.34617.73-4")]
        [InlineData(" 84434617734 ", "844.34617.73-4")]
        public void Check_explicit_to_cep_is_valid(string input, string expected)
        {
            PisType test = (PisType)input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("844.34617.73-4", "844.34617.73-4")]
        [InlineData("844 34617 73 4", "844.34617.73-4")]
        [InlineData("84434617734", "844.34617.73-4")]
        [InlineData(" 84434617734 ", "844.34617.73-4")]
        public void Check_explicit_to_string_is_valid(string input, string expected)
        {
            PisType test = (PisType)input;
            string result = (string)test;
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("844   34617   73     4")]
        [InlineData("844.34617.73-48")]
        [InlineData("84434617")]
        [InlineData("844.34617-abc")]
        public void Check_parse_invalid(string input)
        {
            Assert.Throws<ArgumentException>(() => PisType.Parse(input));
        }
        
        [Theory]
        [InlineData("844.34617.73-4")]
        [InlineData("844 34617 73 4")]
        [InlineData("84434617734")]
        [InlineData(" 84434617734 ")]
        public void Check_parse_valid(string input)
        {
            PisType test = PisType.Parse(input);
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("844.34617.73-4", null, "844.34617.73-4")]
        [InlineData("844.34617.73-4", "", "844.34617.73-4")]
        [InlineData("844.34617.73-4", "d", "844.34617.73-4")]
        [InlineData("844.34617.73-4", "n", "84434617734")]
        public void Check_to_string_format(string input, string format, string expected)
        {
            PisType test = new(input);
            Assert.Equal(expected, test.ToString(format));
        }

        [Theory]
        [InlineData("844.34617.73-4", "text")]
        [InlineData("844.34617.73-4", "x")]
        public void Check_to_string_format_exception(string input, string format)
        {
            PisType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }

        [Theory]
        [InlineData("844.34617.73-4", "844.34617.73-4", true)]
        [InlineData("844.34617.73-4", "84434617734", true)]
        [InlineData("844.34617.73-4", "844.34617.73-2", false)]
        [InlineData("844.34617.73-4", "any value", false)]
        public void Compare_equal(string input, string compare, bool expected)
        {
            PisType inputValue = new(input);
            PisType compareValue = new(compare);
            Assert.Equal(expected, inputValue == compareValue);
            Assert.Equal(expected, inputValue.Equals(compareValue));            
            Assert.Equal(!expected, inputValue != compareValue);
        }

        [Theory]
        [InlineData("844.34617.73-4", "844.34617.73-4", true)]
        [InlineData("844.34617.73-4", "84434617734", true)]
        [InlineData("844.34617.73-4", "844.34617.73-2", false)]
        [InlineData("844.34617.73-4", "any value", false)]
        public void Compare_get_hash_code(string input, string compare, bool expected)
        {
            PisType inputValue = new(input);
            PisType compareValue = new(compare);
            Assert.Equal(expected, inputValue.GetHashCode() == compareValue.GetHashCode());
        }

        [Fact]
        public void Compare_equal_as_object()
        {
            PisType compare = new ("844.34617.73-4");

            Assert.False(compare.Equals(null));
            Assert.False(compare.Equals(compare.ToString()));
            Assert.False(compare.Equals(compare.ToString("n")));
        }

        [Theory]
        [InlineData("844.34617.73-4", "844.34617.73-2", true)]
        [InlineData("844.34617.73-4", "844.34617.73-5", false)]
        public void Compare_greater_than(string input, string compare, bool expected)
        {
            PisType inputValue = new(input);
            PisType compareValue = new(compare);
            Assert.Equal(expected, inputValue > compareValue);
            Assert.Equal(!expected, inputValue < compareValue);
        }

        [Theory]
        [InlineData("844.34617.73-4", "844.34617.73-2", true)]
        [InlineData("844.34617.73-4", "844.34617.73-4", true)]
        [InlineData("844.34617.73-4", "844.34617.73-5", false)]
        public void Compare_greater_or_qual_than(string input, string compare, bool expected)
        {
            PisType inputValue = new(input);
            PisType compareValue = new(compare);
            Assert.Equal(expected, inputValue >= compareValue);
        }        

        [Theory]
        [InlineData("844.34617.73-2", "844.34617.73-4", true)]
        [InlineData("844.34617.73-2", "844.34617.73-2", true)]
        [InlineData("844.34617.73-5", "844.34617.73-4", false)]
        public void Compare_small_or_qual_than(string input, string compare, bool expected)
        {
            PisType inputValue = new(input);
            PisType compareValue = new(compare);
            Assert.Equal(expected, inputValue <= compareValue);
        }

        [Fact]
        public void Check_empty_value()
        {
            PisType test = PisType.Empty;
            Assert.False(test.IsValid());
            Assert.Equal("000.00000.00-0", test.ToString());
        }

        [Fact]
        public void Compare_greater_than_as_object()
        {
            PisType compare = new ("844.34617.73-4");

            Assert.Equal(-1,compare.CompareTo(null));
            Assert.Equal(-1,compare.CompareTo(compare.ToString()));
            Assert.Equal(-1,compare.CompareTo(compare.ToString("n")));
        }

        [Fact]
        public void Check_get_type_code_has_string()
        {
            PisType compare = new ("844.34617.73-4");
            Assert.Equal(TypeCode.String, compare.GetTypeCode());
        }
        
        [Theory]
        [InlineData("844.34617.73-4", typeof(string), true)]
        [InlineData("any value", typeof(string), true)]
        [InlineData("844.34617.73-4", typeof(bool), false)]
        [InlineData("true", typeof(bool), true)]
        [InlineData("844.34617.73-4", typeof(char), false)]
        [InlineData("n", typeof(char), true)]
        [InlineData("844.34617.73-4", typeof(sbyte), false)]
        [InlineData("1", typeof(sbyte), true)]
        [InlineData("844.34617.73-4", typeof(byte), false)]
        [InlineData("1", typeof(byte), true)]
        [InlineData("844.34617.73-4", typeof(Int16), false)]
        [InlineData("1", typeof(Int16), true)]
        [InlineData("844.34617.73-4", typeof(Int32), false)]
        [InlineData("1", typeof(Int32), true)]
        [InlineData("844.34617.73-4", typeof(Int64), false)]
        [InlineData("1", typeof(Int64), true)]
        [InlineData("844.34617.73-4", typeof(UInt16), false)]
        [InlineData("1", typeof(UInt16), true)]
        [InlineData("844.34617.73-4", typeof(UInt32), false)]
        [InlineData("1", typeof(UInt32), true)]
        [InlineData("844.34617.73-4", typeof(UInt64), false)]
        [InlineData("1", typeof(UInt64), true)]
        [InlineData("844.34617.73-4", typeof(Single), false)]
        [InlineData("1", typeof(Single), true)]
        [InlineData("844.34617.73-4", typeof(Double), false)]
        [InlineData("1", typeof(Double), true)]
        [InlineData("844.34617.73-4", typeof(Decimal), false)]
        [InlineData("1", typeof(Decimal), true)]
        [InlineData("844.34617.73-4", typeof(DateTime), false)]
        [InlineData("2021-01-01", typeof(DateTime), true)]
        public void Check_convertible_success(string input, Type type, bool expectedSuccess)
        {
            PisType test = new(input);
            
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