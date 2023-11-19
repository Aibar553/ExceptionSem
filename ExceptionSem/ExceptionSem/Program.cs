using System;

class Calculator
{
    public static double DoubleTryParse(string input)
    {
        if(!double.TryParse(input, out double result))
        {
            throw new ArgumentException("Invalid input. Please enter a valid number.");
        }
        if(result < 0)
        {
            throw new ArgumentException("Negative numbers are not allowed.");
        }
        return result;
    }
    public static double Add(double a, double b)
    {
        return a + b;
    }
    public static double Subtract(double a, double b)
    {
        double result = a - b;
        if(result < 0)
        {
            throw new InvalidOperationException("Subtraction result cannot be negative.");
        }
        return result;
    }
    public static double Multiply(double a, double b)
    {
        return a * b;
    }
    public static double Divide(double a, double b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        double result = a / b;
        if(result < 0)
        {
            throw new InvalidOperationException("Division result cannot be negative");
        }
        return result;
    }
    static void Main()
    {
        try
        {
            Console.Write("Enter the first number: ");
            string input1 = Console.ReadLine();
            double num1 = DoubleTryParse(input1);

            Console.Write("Enter the second number: ");
            string input2 = Console.ReadLine();
            double num2 = DoubleTryParse(input2);

            Console.Write("Enter the operation (+, -, *, /): ");
            string operation = Console.ReadLine();

            switch (operation)
            {
                case "+":
                    Console.WriteLine($"Result: {Add(num1, num2)}");
                    break;
                case "-":
                    Console.WriteLine($"Result: {Subtract(num1, num2)}");
                    break;
                case "*":
                    Console.WriteLine($"Result: {Multiply(num1, num2)}");
                    break;
                case "/":
                    Console.WriteLine($"Result: {Divide(num1, num2)}");
                    break;
                default:
                    Console.WriteLine("Invalid operation.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
