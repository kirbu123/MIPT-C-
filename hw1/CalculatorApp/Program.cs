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
                    Console.WriteLine(Environment.NewLine + "Введите первое число:");
                    string? input1 = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(input1))
                    {
                        Console.WriteLine("Ошибка: пустой ввод. Введите число или 'q' для выхода.");
                        continue;
                    }
                    
                    if (input1.Trim().ToLowerInvariant() == "q")
                        break;
                    
                    if (!double.TryParse(input1, out double num1))
                    {
                        Console.WriteLine("Ошибка: введите корректное число!");
                        continue;
                    }
                    
                    Console.WriteLine("Введите второе число:");
                    string? input2 = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(input2))
                    {
                        Console.WriteLine("Ошибка: пустой ввод. Введите число или 'q' для выхода.");
                        continue;
                    }
                    
                    if (input2.Trim().ToLowerInvariant() == "q")
                        break;
                    
                    double num2 = Convert.ToDouble(input2);
                    
                    Console.WriteLine("Выберите операцию (+, -, *, /):");
                    string? operation = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(operation))
                    {
                        Console.WriteLine("Ошибка: пустой ввод. Введите операцию или 'q' для выхода.");
                        continue;
                    }
                    
                    if (operation.Trim().ToLowerInvariant() == "q")
                        break;
                    
                    string op = operation.Trim();
                    double result = 0;
                    bool validOperation = true;
                    
                    switch (op)
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
                        Console.WriteLine($"Результат: {num1} {op} {num2} = {result}");
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