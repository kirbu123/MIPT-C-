namespace hw2.Cars;

public abstract class AManualGasCar : AGasCar, IManual
{
    public abstract int GearCount { get; }

    protected override string GetTransmissionDescription() =>
        $"механическая коробка передач ({GearCount} передач)";
}
