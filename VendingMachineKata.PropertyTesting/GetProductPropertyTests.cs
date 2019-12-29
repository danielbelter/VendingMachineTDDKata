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
                return sut.GetCandy().Contains("not enough money");
            };

            return property.ToProperty();
        }

        [Property(Arbitrary = new[] {typeof(CandyWithoutChange)})]
        public Property CanBuyProduct(string coins)
        {
            var sut = new VendingMachine();
            Func<bool> property = () =>
            {
                sut.InsertMoney(coins);
                return sut.GetCandy().Contains("Candy");
            };

            return property.ToProperty();
        }

        [Property(Arbitrary = new[] {typeof(CandyWithChange)})]
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
        private static readonly string[] AllowedCoins = {"0.50", "0.25"};

        public static Arbitrary<string> LessThan1DollarCoinGenerator()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf(1)
                .Select(x => 
                    string.Join(", ", x))
                .ToArbitrary();
        }
    }

    internal class CandyWithoutChange
    {
        private static readonly decimal[] AllowedCoins = {1m, 0.5m, 0.25m};

        public static Arbitrary<string> MoreThan1D()
        {
            return Gen.Elements(AllowedCoins)
                .ListOf()
                .Select(x => x.Sum())
                .Select(x => x - 1.0m)
                .Where(x => x >= 1.0m)
                .Select(x => 
                    string.Join(", ", x))
                .ToArbitrary();
        }
    }

    internal class CandyWithChange
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