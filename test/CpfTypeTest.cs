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
            Assert.Equal(expected, inputValue.Equals(compareValue.ToString()));

        }
    }
}