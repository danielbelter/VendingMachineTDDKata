using FluentAssertions;
using Xunit;

namespace VendingMachineKata.Tests
{
    public class BuyingProductTests
    {
        private readonly VendingMachine _vendingMachine;

        public BuyingProductTests()
        {
            _vendingMachine = new VendingMachine();
        }

        [Fact]
        public void BuyMultipleProductsWhenInsertedEnoughMoney()
        {
            _vendingMachine.InsertMoney("1");
            _vendingMachine.GetCola();
            _vendingMachine.InsertMoney("1");

            var result = _vendingMachine.GetCola();

            result.Should().Contain("Cola");
        }
        
        [Theory]
        [InlineData("1", "Cola")]
        [InlineData("0.5, 0.5", "Cola")]
        [InlineData("0.25, 0.25, 0.25, 0.25", "Cola")]
        public void BuyColaWhenInsertedEnoughMoney(string insertedCoins, string expectedProduct)
        {
            _vendingMachine.InsertMoney(insertedCoins);

            var result = _vendingMachine.GetCola();

            result.Should().Contain(expectedProduct);
        }

        [Theory]
        [InlineData("0.50, 0.25, 0.25, 0.25", "0.25")]
        [InlineData("0.50, 0.50, 0.25", "0.25")]
        public void GetColaWithChange(string insertedCoins, string expectedChange)
        {
            _vendingMachine.InsertMoney(insertedCoins);

            var result = _vendingMachine.GetCola();

            result.Should().ContainAll("Cola", expectedChange);
        }

        [Fact]
        public void GetCandy()
        {
            _vendingMachine.InsertMoney("0.50, 0.25");

            var result = _vendingMachine.GetCandy();

            result.Should().Contain("Candy");
        }

        [Theory]
        [InlineData("0.50, 0.50", "0.25")]
        [InlineData("1", "0.25")]
        public void GetCandyWithChange(string insertedCoins, string expectedChange)
        {
            _vendingMachine.InsertMoney(insertedCoins);

            var result = _vendingMachine.GetCandy();

            result.Should().ContainAll("Candy", expectedChange);
        }

        [Fact]
        public void GetChips()
        {
            _vendingMachine.InsertMoney("0.50");

            var result = _vendingMachine.GetChips();

            result.Should().Contain("Chips");
        }

        [Theory]
        [InlineData("0.50, 0.25, 0.25", "0.50")]
        [InlineData("0.50, 0.50", "0.50")]
        public void GetChipsWithCHange(string insertedCoins, string expectedChange)
        {
            _vendingMachine.InsertMoney(insertedCoins);

            var result = _vendingMachine.GetChips();

            result.Should().ContainAll("Chips", expectedChange);
        }

        [Fact]
        public void CantBuyColaWhenNotEnoughMoney()
        {
            _vendingMachine.InsertMoney("0.50");

            var result = _vendingMachine.GetCola();

            result.ToLower().Should().Contain("not enough money");
        }
    }
}