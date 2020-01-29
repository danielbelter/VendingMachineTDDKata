using Chill;
using FluentAssertions;
using VendingMachineKata;
using Xunit;

namespace BDDTests
{
    namespace ForReturningMoney
    {
        public class WhenInsertedNoMoney : GivenSubject<VendingMachine, string>
        {
            public WhenInsertedNoMoney()
            {
                When(() => Subject.Return());
            }

            [Fact]
            public void ThenNoMoneyIsReturned()
            {
                Result.Should().BeEmpty();
            }
        }

        public class WhenInsertedMoney : GivenSubject<VendingMachine, string>
        {
            private const string InsertedMoney = "0.5";

            public WhenInsertedMoney()
            {
                Given(() => Subject.InsertMoney(InsertedMoney));

                When(() => Subject.Return());
            }

            [Fact]
            public void ThenInsertedMoneysAreReturned()
            {
                Result.Should().BeEquivalentTo(InsertedMoney);
            }
        }
    }
}
