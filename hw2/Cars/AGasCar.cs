namespace hw2.Cars;

public abstract class AGasCar : ACar, IMechanicalEngine
{
    public abstract int HorsePower { get; }
    public abstract string FuelType { get; }

    protected override string GetEngineDescription() =>
        $"{FuelType} двигатель {HorsePower} л.с.";
}
