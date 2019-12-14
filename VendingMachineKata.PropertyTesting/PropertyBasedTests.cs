using System;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class PropertyBasedTests
    {
        [Property(Arbitrary = new[] {typeof(Arbitraries)})]
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

    public static class Arbitraries
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