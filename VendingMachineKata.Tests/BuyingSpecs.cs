using FluentAssertions;
using Xunit;

namespace VendingMachineKata.Tests
{
    public class BuyingSpecs
    {
        private readonly VendingMachine _sut;

        public BuyingSpecs()
        {
            _sut = new VendingMachine();
        }


        [Fact]
        public void SoldOutWhenBoughtMoreTHanAvailable()
        {
            _sut.InsertMoney("1");
            _sut.GetCola();
            
            _sut.InsertMoney("1");
            var result = _sut.GetCola();

            result.Should().Contain("Item sould out");
        }

        [Fact]
        public void GetCandy()
        {
            _sut.InsertMoney("0.50, 0.25");

            var result = _sut.GetCandy();

            result.Should().Contain("Candy");
        }

        [Theory]
        [InlineData("0.50, 0.50", "0.25")]
        [InlineData("1", "0.25")]
        public void GetCandyWithChange(string insertedCoins, string expectedChange)
        {
            _sut.InsertMoney(insertedCoins);

            var result = _sut.GetCandy();

            result.Should().ContainAll("Candy", expectedChange);
        }

        [Theory]
        [InlineData("0.50, 0.25, 0.25", "0.50")]
        [InlineData("0.50, 0.50", "0.50")]
        public void GetChipsWithCHange(string insertedCoins, string expectedChange)
        {
            _sut.InsertMoney(insertedCoins);

            var result = _sut.GetChips();

            result.Should().ContainAll("Chips", expectedChange);
        }

        [Theory]
        [InlineData("0.50, 0.25, 0.25, 0.25", "0.25")]
        [InlineData("0.50, 0.50, 0.25", "0.25")]
        public void GetColaWithChange(string insertedCoins, string expectedChange)
        {
            _sut.InsertMoney(insertedCoins);

            var result = _sut.GetCola();

            result.Should().ContainAll("Cola", expectedChange);
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
    }
}