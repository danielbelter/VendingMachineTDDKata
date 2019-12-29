using System;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class ReturnInsertedMoneyPropertyTests
    {
        [Property(Arbitrary = new[] {typeof(CoinArbitraries)})]
        public Property ReturnInsertedMoney(string coins)
        {
            var sut = new VendingMachine();
            Func<bool> property = () =>
            {
                sut.InsertMoney(coins);
                return sut.Return() == coins;
            };

            return property.ToProperty();
        }
    }

    internal static class CoinArbitraries
    {
        private static readonly string[] AllowedCoins = {"1", "0.50", "0.25"};

        public static Arbitrary<string> CoinGenerator()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Select(x => string.Join(", ", x))
                .ToArbitrary();
        }
    }
}
/*
 * https://www.pluralsight.com/tech-blog/property-based-tdd/
 * */