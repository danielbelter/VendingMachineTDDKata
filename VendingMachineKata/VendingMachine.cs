using System;
using System.Linq;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        public decimal Sum(string insertedCoins)
        {
            var coins = insertedCoins.Split(new []{" ", ","}, StringSplitOptions.RemoveEmptyEntries);

            return coins.Select(Money.FromString).Sum(value => value.Amount);
        }
    }
}
