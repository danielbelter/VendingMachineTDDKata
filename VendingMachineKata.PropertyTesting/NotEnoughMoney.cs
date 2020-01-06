using System;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class NotEnoughMoney
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
    }

    internal class LessThan1DollarGenerator
    {
        private static readonly decimal[] AllowedCoins = {1m, 0.50m, 0.25m};

        public static Arbitrary<string> LessThan1Dollar()
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
}