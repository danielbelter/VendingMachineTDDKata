using System;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class GetProductWithChange
    {
        [Property(Arbitrary = new[] {typeof(MoreThan1DollarGenerator)})]
        public Property CanBuyProductWithChange((string coins, string change) coinsWithChange)
        {
            var sut = new VendingMachine();
            var (coins, change) = coinsWithChange;
            Func<bool> property = () =>
            {
                sut.InsertMoney(coins);
                var boughtColaWithChange = sut.GetCola();
                return boughtColaWithChange.Contains("Cola") && boughtColaWithChange.Contains(change);
            };

            return property.ToProperty();
        }
    }

    internal class MoreThan1DollarGenerator
    {
        private static readonly decimal[] AllowedCoins = {1m, 0.5m, 0.25m};

        public static Arbitrary<(string,string)> MoreThan1D()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Where(x => x.Sum() > 1.0m)
                .Select(x => 
                    (string.Join(", ", x), (x.Sum() - 1.0m).ToString()))
                .ToArbitrary();
        }
    }
}