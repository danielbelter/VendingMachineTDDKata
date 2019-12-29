using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineKata
{
    public class VendingMachine
    {
        private IEnumerable<string> _coins = new List<string>();
        private decimal _insertedMoneySum;
        private readonly Dictionary<string, int> _productsQuantity;

        public VendingMachine()
        {
            _productsQuantity = new Dictionary<string, int>
            {
                { "Cola", 100 },
                { "Candy", 1 },
                { "Chips", 1 }
            };
        }

        public void InsertMoney(string insertedCoins)
        {
            _coins = insertedCoins.Split(new[] {" ", ","}, StringSplitOptions.RemoveEmptyEntries);
            _insertedMoneySum = _coins.Select(decimal.Parse).Sum();
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
                return "Item sould out";
            }

            if (_insertedMoneySum > price)
            {
                var change = _insertedMoneySum - price;
                _productsQuantity[product]--;
                return $"{product} with {change.ToString()}";
            }

            if (_insertedMoneySum == price)
            {
                _productsQuantity[product]--;
                return product;
            }

            return "not enough money";
        }
    }
}
/*
 *
 * Select Product

As a vendor
I want customers to select products
So that I can give them an incentive to put money in the machine
There are three products: cola for $1.00, chips for $0.50, and candy for $0.75. 
When the respective button is pressed and enough money has been inserted, the product is dispensed and the machine displays THANK YOU. 
If the display is checked again, it will display INSERT COIN and the current amount will be set to $0.00. 
If there is not enough money inserted then the machine displays PRICE and the price of the item and subsequent
checks of the display will display either INSERT COIN or the current amount as appropriate.

* Make Change

As a vendor
I want customers to receive correct change
So that they will use the vending machine again
When a product is selected that costs less than the amount of money in the machine, then the remaining amount is placed in the coin return.

* Return Coins

As a customer
I want to have my money returned
So that I can change my mind about buying stuff from the vending machine
When the return coins button is pressed, the money the customer has placed in the machine is returned and the display shows INSERT COIN.

* Sold Out

As a customer
I want to be told when the item I have selected is not available
So that I can select another item
When the item selected by the customer is out of stock, the machine displays SOLD OUT. 
If the display is checked again, it will display the amount of money remaining in the machine or INSERT COIN if there is no money in the machine.

* Exact Change Only

As a customer
I want to be told when exact change is required
So that I can determine if I can buy something with the money I have before inserting it
When the machine is not able to make change with the money in the machine for any of the items that it sells, it will display EXACT CHANGE ONLY instead of INSERT COIN.
*/