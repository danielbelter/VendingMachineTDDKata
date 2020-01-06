using System;
using System.Linq;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class GetProductWithoutChange
    {
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

}