using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== КАЛЬКУЛЯТОР ===");
            Console.WriteLine("Для выхода введите 'q' в любой момент");
            
            while (true)
            {
                try
                {
                    Console.WriteLine("\nВведите первое число:");
                    string input1 = Console.ReadLine();
                    
                    if (input1.ToLower() == "q")
                        break;
                    
                    double num1 = Convert.ToDouble(input1);
                    
                    Console.WriteLine("Введите второе число:");
                    string input2 = Console.ReadLine();
                    
                    if (input2.ToLower() == "q")
                        break;
                    
                    double num2 = Convert.ToDouble(input2);
                    
                    Console.WriteLine("Выберите операцию (+, -, *, /):");
                    string operation = Console.ReadLine();
                    
                    if (operation.ToLower() == "q")
                        break;
                    
                    double result = 0;
                    bool validOperation = true;
                    
                    switch (operation)
                    {
                        case "+":
                            result = num1 + num2;
                            break;
                        case "-":
                            result = num1 - num2;
                            break;
                        case "*":
                            result = num1 * num2;
                            break;
                        case "/":
                            if (num2 == 0)
                            {
                                Console.WriteLine("Ошибка: деление на ноль!");
                                validOperation = false;
                            }
                            else
                            {
                                result = num1 / num2;
                            }
                            break;
                        default:
                            Console.WriteLine("Ошибка: неизвестная операция!");
                            validOperation = false;
                            break;
                    }
                    
                    if (validOperation)
                    {
                        Console.WriteLine($"Результат: {num1} {operation} {num2} = {result}");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введите корректное число!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }
            
            Console.WriteLine("Программа завершена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}