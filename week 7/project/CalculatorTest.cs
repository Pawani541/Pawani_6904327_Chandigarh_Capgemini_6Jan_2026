using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using CalculatorApp;

namespace projects
{
    internal class CalculatorTest
    {
        private Calculator calc;
        [SetUp]
        public void Setup()
        {
            calc = new Calculator();
        }

        [Test]
        public void Add_TwoNumbers_ReturnsSum()
        {
            int a = 5, b = 3;
            int result = calc.Add(a, b);
            Assert.That(result, Is.EqualTo(8));
        }
        [Test]
        public void Subtract_TwoNumbers_ReturnsDifference()
        {
            int a = 10, b = 4;
            int result2 = calc.Subtract(a, b);
            Assert.That(result2, Is.EqualTo(6));

        }
    }
}