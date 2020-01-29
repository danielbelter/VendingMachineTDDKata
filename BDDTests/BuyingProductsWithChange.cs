using Chill;
using FluentAssertions;
using VendingMachineKata;
using Xunit;

namespace BDDTests
{
    namespace ForBuyingProductsWithChange
    {
        public class WhenInsertedMoreMoneyThanPriceOfProduct : GivenSubject<VendingMachine, string>
        {
            public WhenInsertedMoreMoneyThanPriceOfProduct()
            {
                Given(() => Subject.InsertMoney("1, 0.50"));

                When(() => Subject.GetCola());
            }

            [Fact]
            public void ShouldGetColaWithChange()
            {
                Result.Should().ContainAll("Cola", "0.50");
            }
        }
    }
}