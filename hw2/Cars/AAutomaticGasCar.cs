namespace hw2.Cars;

public abstract class AAutomaticGasCar : AGasCar, IAutomatic
{
    public abstract string TransmissionType { get; }

    protected override string GetTransmissionDescription() =>
        $"автоматическая коробка передач ({TransmissionType})";
}
