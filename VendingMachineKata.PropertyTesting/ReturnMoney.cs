using System;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    internal static class CoinsGenerator
    {
        public static decimal[] AllowedCoins = { 1m, 0.50m, 0.25m };

        public static Arbitrary<string> GenerateCoins()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Select(coins => string.Join(", ", coins))
                .ToArbitrary();
        }
    }

    public class ReturnMoneyTests
    {
        [Property(Arbitrary = new[] { typeof(CoinsGenerator) })]
        public Property ReturnsInsertedMoney(string coins)
        {
            var vendingMachine = new VendingMachine();
            Func<bool> returnMoneyProperty = () =>
            {
                vendingMachine.InsertMoney(coins);

                return vendingMachine.Return() == coins;
            };

            return returnMoneyProperty.ToProperty();
        }
    }
}