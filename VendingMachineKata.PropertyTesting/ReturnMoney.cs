using System;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class ReturnMoney
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
        private static readonly decimal[] AllowedCoins = {1m, 0.50m, 0.25m};

        public static Arbitrary<string> CoinGenerator()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Select(x => string.Join(", ", x))
                .ToArbitrary();
        }
    }
}
