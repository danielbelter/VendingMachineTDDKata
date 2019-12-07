using FluentAssertions;
using Xunit;

namespace VendingMachineKata.Tests
{
    public class BuyingSpecs
    {
        public BuyingSpecs()
        {
            _sut = new VendingMachine();
        }

        private readonly VendingMachine _sut;
        
        [Fact]
        public void GetCandy()
        {
            _sut.InsertMoney("0.50, 0.25");

            var result = _sut.GetCandy();

            result.Should().Contain("Candy");
        }

        [Theory]
        [InlineData("0.50, 0.50", "Candy", "0.25")]
        [InlineData("1", "Candy", "0.25")]
        [InlineData("0.50, 0.25, 0.25", "Candy", "0.25")]
        [InlineData("0.50, 0.50, 0.50", "Candy", "0.75")]
        public void GetCandyWithChange(string insertedCoins, string expectedProduct, string expectedChange)
        {
            _sut.InsertMoney(insertedCoins);

            var result = _sut.GetCandy();

            result.Should().ContainAll(expectedProduct, expectedChange);
        }

        [Fact]
        public void CantBuyWhenNotEnoughMoney()
        {
            _sut.InsertMoney("0.50");

            var result = _sut.GetCola();

            result.ToLower().Should().Contain("not enough money");
        }

        [Fact]
        public void GetChips()
        {
            _sut.InsertMoney("0.50");

            var result = _sut.GetChips();

            result.Should().Contain("Chips");
        }

        [Fact]
        public void GetChipsWithChange()
        {
            _sut.InsertMoney("0.50, 0.25");

            var result = _sut.GetChips();

            result.Should().ContainAll("Chips", "0.25");
        }

        [Fact]
        public void GetCola()
        {
            _sut.InsertMoney("1");

            var result = _sut.GetCola();

            result.Should().Contain("Cola");
        }

        [Fact]
        public void GetColaWithChange()
        {
            _sut.InsertMoney("1, 0.50");

            var result = _sut.GetCola();

            result.Should().ContainAll("Cola", "0.50");
        }
    }
}