using System;
using FsCheck;
using FsCheck.Xunit;

namespace VendingMachineKata.PropertyTesting
{
    public class PropertyBasedTests
    {
        [Property]
        public Property ReturnInsertedMoney(int input)
        {
            var sut = new VendingMachine();
            Func<bool> property = () =>
            {
                sut.InsertMoney(input.ToString());
                return sut.Return() == input.ToString();
            };

            return property.ToProperty();
        }
    }
}
/*
 * https://www.pluralsight.com/tech-blog/property-based-tdd/
 * */