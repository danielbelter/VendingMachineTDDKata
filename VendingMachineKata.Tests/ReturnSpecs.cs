using FluentAssertions;
using Xunit;

namespace VendingMachineKata.Tests
{
    public class ReturnSpecs
    {
        [Theory]
        [InlineData("1.00, 0.50", "1.00, 0.50")]
        [InlineData("0.50, 0.50", "0.50, 0.50")]
        [InlineData("0.25, 0.25", "0.25, 0.25")]
        [InlineData("0.25, 0.50", "0.25, 0.50")]
        [InlineData("1, 1, 1", "1, 1, 1")]
        [InlineData("0.25, 0.25, 0.25, 0.25", "0.25, 0.25, 0.25, 0.25")]
        public void ReturnsAllInsertedCoins(string insertedCoins, string expected)
        {
            var vendingMachine = new VendingMachine();
            vendingMachine.InsertMoney(insertedCoins);

            var returnedCoins = vendingMachine.Return();

            returnedCoins.Should().Be(expected);
        }

    }
}