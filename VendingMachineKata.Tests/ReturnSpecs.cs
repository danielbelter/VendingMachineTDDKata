using FluentAssertions;
using Xunit;

namespace VendingMachineKata.Tests
{
    public class ReturnSpecs
    {
        [Theory]
        [InlineData("1, 0.5, 0.5", "1, 0.5, 0.5")]
        [InlineData("", "")]
        [InlineData("1", "1")]
        [InlineData("0.25, 0.25", "0.25, 0.25")]
        public void ReturnsAllInsertedMoney(string insertedMoney, string expectedReturn)
        {
            var subject = new VendingMachine();

            subject.InsertMoney(insertedMoney);

            var result = subject.Return();

            result.Should().Be(expectedReturn);
        }
    }
}