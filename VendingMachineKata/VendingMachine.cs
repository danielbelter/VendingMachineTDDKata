using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        private IEnumerable<string> _coins = new List<string>();

        public decimal Sum()
        {
            return _coins.Select(Money.FromString).Sum(value => value.Amount);
        }

        public void InsertMoney(string insertedCoins)
        {
            _coins = insertedCoins.Split(new []{" ", ","}, StringSplitOptions.RemoveEmptyEntries);
        }

        public string Return()
        {
            return string.Join(", ", _coins);
        }
    }
}
