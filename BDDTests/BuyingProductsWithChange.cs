using Chill;
using FluentAssertions;
using VendingMachineKata;
using Xunit;

namespace BDDTests
{
    namespace BuyingProduct
    {
        public class ColaWithWithChange : GivenSubject<VendingMachine, string>
        {
            public ColaWithWithChange()
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