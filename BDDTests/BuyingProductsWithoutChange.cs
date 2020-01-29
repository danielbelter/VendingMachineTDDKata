using Chill;
using FluentAssertions;
using VendingMachineKata;
using Xunit;

namespace BDDTests
{
    namespace ForBuyingProductsWithoutChange
    {
        public class WhenInsertedEnoughMoneyForCola : GivenSubject<VendingMachine, string>
        {
            public WhenInsertedEnoughMoneyForCola()
            {
                Given(() => Subject.InsertMoney("1"));

                When(() => Subject.GetCola());
            }

            [Fact]
            public void ShouldGetCola()
            {
                Result.Should().Contain("Cola");
            }
        }

        public class WhenInsertedEnoughMoneyForCandy : GivenSubject<VendingMachine, string>
        {
            public WhenInsertedEnoughMoneyForCandy()
            {
                Given(() => Subject.InsertMoney("0.75"));

                When(() => Subject.GetCandy());
            }

            [Fact]
            public void ShouldGetCola()
            {
                Result.Should().Contain("Candy");
            }
        }

        public class WhenInsertedEnoughMoneyForChips : GivenSubject<VendingMachine, string>
        {
            public WhenInsertedEnoughMoneyForChips()
            {
                Given(() => Subject.InsertMoney("0.50"));

                When(() => Subject.GetChips());
            }

            [Fact]
            public void ShouldGetCola()
            {
                Result.Should().Contain("Chips");
            }
        }
    }
}