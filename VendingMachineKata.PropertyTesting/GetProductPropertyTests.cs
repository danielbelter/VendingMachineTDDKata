using System;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class GetProductPropertyTests
    {
        [Property(Arbitrary = new[] {typeof(LessThan1DollarGenerator)})]
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

        [Property(Arbitrary = new[] {typeof(Exactly1DollarGenerator)})]
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

    internal class LessThan1DollarGenerator
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

    internal class Exactly1DollarGenerator
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

    internal class MoreThan1DollarGenerator
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