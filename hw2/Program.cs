using hw2;

var brands = string.Join(", ", CarFactory.GetAvailableBrands());
Console.WriteLine($"Доступные марки: {brands}");
Console.WriteLine();

while (true)
{
    Console.Write("Введите марку автомобиля или done для остановки ввода: ");
    var input = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(input))
        continue;

    if (input.Equals("done", StringComparison.OrdinalIgnoreCase))
        break;

    if (CarFactory.TryParseCarType(input, out var carType))
    {
        var car = CarFactory.Create(carType);
        Console.WriteLine($"«{car.GetDescription()}»");
    }
    else
    {
        Console.WriteLine($"Марка «{input}» не найдена. Доступные марки: {brands}");
    }

    Console.WriteLine();
}

Console.WriteLine("Программа завершена.");
