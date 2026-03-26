namespace hw2.Cars;

public abstract class AElectricCar : ACar, IElectric
{
    public abstract int BatteryCapacityKWh { get; }
    public abstract int RangeKm { get; }

    protected override string GetEngineDescription() =>
        $"электрический двигатель, батарея {BatteryCapacityKWh} кВт·ч, запас хода {RangeKm} км";
}
