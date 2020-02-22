using FluentAssertions;
using Xunit;

namespace VendingMachineKata.Tests
{
    public class BuyingProductTests
    {
        [Theory]
        [InlineData("1", "Cola")]
        [InlineData("0.5, 0.5", "Cola")]
        [InlineData("0.25, 0.25, 0.25, 0.25", "Cola")]
        public void BuyColaWhenInsertedEnoughMoney(string insertedCoins, string expectedProduct)
        {
            var subject = new VendingMachine();

            subject.InsertMoney(insertedCoins);

            var result = subject.GetCola();

            result.Should().Contain(expectedProduct);
        }

        [Fact]
        public void BuyMultipleProductsWhenInsertedEnoughMoney()
        {
            var subject = new VendingMachine();

            subject.InsertMoney("1");

            subject.GetCola();

            subject.InsertMoney("1");

            var result = subject.GetCola();

            result.Should().Contain("Cola");
        }
    }
}