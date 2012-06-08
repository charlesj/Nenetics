using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Nenetics.Tests
{
    public class Sandbox
    {
        [Fact]
        public void Play()
        {
            double chance = Math.Round(50.12345, 3);
            int intChance = Convert.ToInt32(chance * 1000);
            int value = RandomNumberSource.GetNext(100000);
            if( value < intChance)
            {
                Console.WriteLine("true");
            }
            else
            {
                Console.WriteLine("false");
            }
        }
    }
}
