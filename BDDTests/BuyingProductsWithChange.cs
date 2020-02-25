using Chill;
using FluentAssertions;
using VendingMachineKata;
using Xunit;

namespace BDDTests
{
    namespace BuyingProduct
    {
        public class ColaWithChange : GivenSubject<VendingMachine, string>
        {
            public ColaWithChange()
            {
                Given(() => Subject.InsertMoney("1, 0.50"));
                When(() => Subject.GetCola());
            }

            [Fact]
            public void ThenGetsColaWithCorrectChange()
            {
                Result.Should().ContainAll("Cola", "0.50");
            }
        }
    }
}