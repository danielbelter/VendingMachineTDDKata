using System;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class GetProductPropertyTests
    {
        [Property(Arbitrary = new[] {typeof(ProductArbitraties)})]
        public Property CantBuyProduct(string coins)
        {
            var sut = new VendingMachine();
            Func<bool> property = () =>
            {
                sut.InsertMoney(coins);
                return sut.GetCola().Contains("not enough money");
            };

            return property.ToProperty();
        }

        [Property(Arbitrary = new[] {typeof(ColaWithoutChange)})]
        public Property CanBuyProduct(string coins)
        {
            var sut = new VendingMachine();
            Func<bool> property = () =>
            {
                sut.InsertMoney(coins);
                return sut.GetCola().Contains("Cola");
            };

            return property.ToProperty();
        }

        [Property(Arbitrary = new[] {typeof(ColaWithChange)})]
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

    internal class ProductArbitraties
    {
        private static readonly decimal[] AllowedCoins = {1m, 0.50m, 0.25m};

        public static Arbitrary<string> LessThan1D()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Select(x => x.Sum())
                .Where(x => x < 1m)
                .Select(x => 
                    string.Join(", ", x))
                .ToArbitrary();
        }
    }

    internal class ColaWithoutChange
    {
        private static readonly decimal[] AllowedCoins = {1m, 0.5m, 0.25m};

        public static Arbitrary<string> Exactly1D()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Select(x => x.Sum())
                .Select(x => x - 1.0m)
                .Where(x => x == 1.0m)
                .Select(x => 
                    string.Join(", ", x))
                .ToArbitrary();
        }
    }

    internal class ColaWithChange
    {
        private static readonly decimal[] AllowedCoins = {1m, 0.5m, 0.25m};

        public static Arbitrary<(string,string)> MoreThan1D()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Select(x => x.Sum())
                .Select(x => x - 1.0m)
                .Where(x => x > 1.0m)
                .Select(x => 
                    string.Join(", ",x))
                .Select(x => (x, (decimal.Parse(x) - 1.0m).ToString()))
                .ToArbitrary();
        }
    }
}