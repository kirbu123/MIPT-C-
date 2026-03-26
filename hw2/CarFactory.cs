using hw2.Cars;

namespace hw2;

public static class CarFactory
{
    public static ICar Create(CarType type) => type switch
    {
        CarType.Tesla  => new TeslaCar(),
        CarType.BMW    => new BmwCar(),
        CarType.Toyota => new ToyotaCar(),
        CarType.Ford   => new FordCar(),
        CarType.Lada   => new LadaCar(),
        _ => throw new ArgumentOutOfRangeException(nameof(type), $"Неизвестный тип автомобиля: {type}")
    };

    public static bool TryParseCarType(string input, out CarType carType) =>
        Enum.TryParse(input, ignoreCase: true, out carType) && Enum.IsDefined(carType);

    public static IEnumerable<string> GetAvailableBrands() =>
        Enum.GetNames<CarType>();
}
