using FluentAssertions;
using Xunit;

namespace VendingMachineKata.Tests
{
    public class MoneySum
    {
        [Theory]
        [InlineData("1.00, 0.50", 1.50)]
        [InlineData("0.50, 0.50", 1.00)]
        [InlineData("0.25, 0.25", 0.50)]
        [InlineData("0.25, 0.50", 0.75)]
        [InlineData("1, 1, 1", 3)]
        [InlineData("0.25, 0.25, 0.25, 0.25", 1.00)]
        public void CorrectlyCountMoneys(string value, decimal expected)
        {
            var sut = new VendingMachine();
            var result = sut.Sum(value);

            result.Should().Be(expected);
        }
    }
}