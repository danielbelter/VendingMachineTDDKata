using System;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public static class CoinsWithChangeGenerator
    {
        private static readonly decimal[] AllowedCoins = { 1m, 0.50m, 0.25m };

        public static Arbitrary<(string, string)> GenerateCoinsWithChange()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Where(x => x.Sum() > 1)
                .Select(coins => (string.Join(", ", coins), (coins.Sum() - 1m).ToString()))
                .ToArbitrary();
        }
    }

    public class BuyingProductWithChange
    {
        [Property(Arbitrary = new []{typeof(CoinsWithChangeGenerator)}, MaxTest = 1000)]
        public Property GetsProductWithCorrectChange((string coins, string change) coinsWithChange)
        {
            var vendingMachine = new VendingMachine();
            var (coins, change) = coinsWithChange;
            Func<bool> productWithChangeProperty = () =>
            {
                vendingMachine.InsertMoney(coins);

                var productWithChange = vendingMachine.GetCola();

                return productWithChange.Contains("Cola") && productWithChange.Contains(change);
            };

            return productWithChangeProperty.ToProperty();
        }
    }
}