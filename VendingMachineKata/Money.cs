using System;

namespace VendingMachineKata
{
    public class Money
    {
        public decimal Amount { get; }

        private Money(decimal amount)
        {
            Amount = amount;
        }
        
        public static Money FromString(string amount) => new Money(Convert.ToDecimal(amount));

        private Money Add(Money summand)
        {
            return new Money(Amount + summand.Amount);
        }

        public static Money operator +(Money summand1, Money summand2) =>
            summand1.Add(summand2);
    }
}