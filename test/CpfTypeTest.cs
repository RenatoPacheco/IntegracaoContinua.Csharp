using System;
using Xunit;

namespace IntegracaoContinua.Csharp.Teste
{
    public class CpfTypeTest
    {
        [Theory]
        [InlineData("153.179.966-35", "153.179.966-35")]
        [InlineData("15317996635", "153.179.966-35")]
        public void Check_format_is_valid(string input, string expected)
        {
            CpfType test = new(input);
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("153.179.966-00")]
        [InlineData("000.000.000-12")]
        [InlineData("15317996600")]
        public void Check_format_is_invalid(string input)
        {
            CpfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.False(test.IsValid());
        }

        [Fact]
        public void Check_generate_is_valid()
        {
            string input = CpfType.Generate().ToString();
            CpfType test = new(input);
            Assert.Equal(input?.Trim() ?? string.Empty, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("981.560.470-87", "981.560.470-87")]
        [InlineData("98156047087", "981.560.470-87")]
        public void Check_explicit_to_cpf_is_valid(string input, string expected)
        {
            CpfType test = (CpfType)input;
            Assert.Equal(expected, test.ToString());
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("981.560.470-87", "981.560.470-87")]
        [InlineData("98156047087", "981.560.470-87")]
        public void Check_explicit_to_string_is_valid(string input, string expected)
        {
            CpfType test = (CpfType)input;
            string result = (string)test;
            Assert.Equal(expected, result.ToString());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("any text")]
        [InlineData("981.560.470-00")]
        public void Check_parse_invalid(string input)
        {
            Assert.Throws<ArgumentException>(() => CpfType.Parse(input));
        }
        

        [Theory]
        [InlineData("153.179.966-35")]
        [InlineData("15317996635")]
        public void Check_parse_valid(string input)
        {
            CpfType test = CpfType.Parse(input);
            Assert.True(test.IsValid());
        }

        [Theory]
        [InlineData("153.179.966-35", null, "153.179.966-35")]
        [InlineData("153.179.966-35", "", "153.179.966-35")]
        [InlineData("153.179.966-35", "d", "153.179.966-35")]
        [InlineData("153.179.966-35", "n", "15317996635")]
        public void Check_to_string_format(string input, string format, string expected)
        {
            CpfType test = new(input);
            Assert.Equal(expected, test.ToString(format));
        }

        [Theory]
        [InlineData("153.179.966-35", "text")]
        [InlineData("153.179.966-35", "x")]
        public void Check_to_string_format_exception(string input, string format)
        {
            CpfType test = new(input);
            Assert.Throws<ArgumentException>(() => test.ToString(format));
        }

        [Theory]
        [InlineData("153.179.966-35", "153.179.966-35", true)]
        [InlineData("153.179.966-35", "15317996635", true)]
        [InlineData("153.179.966-35", "183.132.538-19", false)]
        [InlineData("153.179.966-35", "any value", false)]
        public void Compare_equal(string input, string compare, bool expected)
        {
            CpfType inputValue = new(input);
            CpfType compareValue = new(compare);
            Assert.Equal(expected, inputValue == compareValue);
            Assert.Equal(expected, inputValue.Equals(compareValue));            
            Assert.Equal(!expected, inputValue != compareValue);
        }

        [Theory]
        [InlineData("153.179.966-35", "153.179.966-35", true)]
        [InlineData("153.179.966-35", "15317996635", true)]
        [InlineData("153.179.966-35", "183.132.538-19", false)]
        [InlineData("153.179.966-35", "any value", false)]
        public void Compare_get_hash_code(string input, string compare, bool expected)
        {
            CpfType inputValue = new(input);
            CpfType compareValue = new(compare);
            Assert.Equal(expected, inputValue.GetHashCode() == compareValue.GetHashCode());
        }

        [Theory]
        [InlineData("153.179.966-35", "153.179.966-00", true)]
        [InlineData("153.179.966-35", "153.179.966-35", true)]
        [InlineData("153.179.966-35", "153.179.966-99", false)]
        public void Compare_greater_or_qual_than(string input, string compare, bool expected)
        {
            CpfType inputValue = new(input);
            CpfType compareValue = new(compare);
            Assert.Equal(expected, inputValue >= compareValue);
        }        

        [Theory]
        [InlineData("153.179.966-00", "153.179.966-35", true)]
        [InlineData("153.179.966-00", "153.179.966-00", true)]
        [InlineData("153.179.966-99", "153.179.966-35", false)]
        public void Compare_small_or_qual_than(string input, string compare, bool expected)
        {
            CpfType inputValue = new(input);
            CpfType compareValue = new(compare);
            Assert.Equal(expected, inputValue <= compareValue);
        }

        [Fact]
        public void Check_empty_value()
        {
            CpfType test = CpfType.Empty;
            Assert.False(test.IsValid());
            Assert.Equal("000.000.000-00", test.ToString());
        } 

        [Fact]
        public void Compare_equal_as_object()
        {
            CpfType compare = new ("153.179.966-35");

            Assert.False(compare.Equals(null));
            Assert.False(compare.Equals(compare.ToString()));
            Assert.False(compare.Equals(compare.ToString("n")));
        }

        [Theory]
        [InlineData("253.179.966-35", "153.179.966-35", true)]
        [InlineData("153.179.966-35", "253.179.966-35", false)]
        public void Compare_greater_than(string input, string compare, bool expected)
        {
            CpfType inputValue = new(input);
            CpfType compareValue = new(compare);
            Assert.Equal(expected, inputValue > compareValue);
            Assert.Equal(!expected, inputValue < compareValue);
        }

        [Fact]
        public void Compare_greater_than_as_object()
        {
            CpfType compare = new ("153.179.966-35");

            Assert.Equal(-1,compare.CompareTo(null));
            Assert.Equal(-1,compare.CompareTo(compare.ToString()));
            Assert.Equal(-1,compare.CompareTo(compare.ToString("n")));
        }

        [Fact]
        public void Check_get_type_code_has_string()
        {
            CpfType compare = new ("153.179.966-35");
            Assert.Equal(TypeCode.String, compare.GetTypeCode());
        }
        

        [Theory]
        [InlineData("153.179.966-35", typeof(string), true)]
        [InlineData("any value", typeof(string), true)]
        [InlineData("153.179.966-35", typeof(bool), false)]
        [InlineData("true", typeof(bool), true)]
        [InlineData("153.179.966-35", typeof(char), false)]
        [InlineData("n", typeof(char), true)]
        [InlineData("153.179.966-35", typeof(sbyte), false)]
        [InlineData("1", typeof(sbyte), true)]
        [InlineData("153.179.966-35", typeof(byte), false)]
        [InlineData("1", typeof(byte), true)]
        [InlineData("153.179.966-35", typeof(Int16), false)]
        [InlineData("1", typeof(Int16), true)]
        [InlineData("153.179.966-35", typeof(Int32), false)]
        [InlineData("1", typeof(Int32), true)]
        [InlineData("153.179.966-35", typeof(Int64), false)]
        [InlineData("1", typeof(Int64), true)]
        [InlineData("153.179.966-35", typeof(UInt16), false)]
        [InlineData("1", typeof(UInt16), true)]
        [InlineData("153.179.966-35", typeof(UInt32), false)]
        [InlineData("1", typeof(UInt32), true)]
        [InlineData("153.179.966-35", typeof(UInt64), false)]
        [InlineData("1", typeof(UInt64), true)]
        [InlineData("153.179.966-35", typeof(Single), false)]
        [InlineData("1", typeof(Single), true)]
        [InlineData("153.179.966-35", typeof(Double), false)]
        [InlineData("1", typeof(Double), true)]
        [InlineData("153.179.966-35", typeof(Decimal), false)]
        [InlineData("1", typeof(Decimal), true)]
        [InlineData("153.179.966-35", typeof(DateTime), false)]
        [InlineData("2021-01-01", typeof(DateTime), true)]
        public void Check_convertible_success(string input, Type type, bool expectedSuccess)
        {
            CpfType test = new(input);
            
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