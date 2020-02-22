using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        private ICollection<decimal> _coins = new List<decimal>();
        private decimal _insertedMoneySum;
        private readonly Dictionary<string, int> _productsQuantity;

        public VendingMachine()
        {
            _productsQuantity = new Dictionary<string, int>
            {
                {
                    "Cola", 1
                },
                {
                    "Candy", 1
                },
                {
                    "Chips", 1
                }
            };
        }

        public void InsertMoney(string insertedCoins)
        {
            _coins = insertedCoins
                .Split(new[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(decimal.Parse)
                .ToList();

            _insertedMoneySum = _coins.Sum();
        }

        public string Return()
        {
            return string.Join(", ", _coins);
        }

        public string GetCola()
        {
            return GetProductWithChange(1m, "Cola");
        }

        public string GetChips()
        {
            return GetProductWithChange(0.50m, "Chips");
        }

        public string GetCandy()
        {
            return GetProductWithChange(0.75m, "Candy");
        }

        private string GetProductWithChange(decimal price, string product)
        {
            if (_productsQuantity[product] == 0)
            {
                return "item sold out";
            }
            
            if (_insertedMoneySum == price)
            {
                _productsQuantity[product]--;
                return product;
            }

            if (_insertedMoneySum > price)
            {
                _productsQuantity[product]--;
                var change = _insertedMoneySum - price;
                return $"{product} with {change.ToString()}";
            }

            return "not enough money";
        }
    }
}