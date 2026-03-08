using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorApp
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        public int Multiply(int a, int b) => a * b;
        public double Divide(int a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Cannot Divide by zero");
            return (double)a / b;
        }

    }
}