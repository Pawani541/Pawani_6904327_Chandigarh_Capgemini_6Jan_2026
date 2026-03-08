namespace CalculatorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator a = new Calculator();
            Console.WriteLine(a.Multiply(5, 6));
            Console.WriteLine(a.Divide(10, 2));
            Console.WriteLine(a.Add(4, 9));
            Console.WriteLine(a.Subtract(5, 5));
        }
    }
}