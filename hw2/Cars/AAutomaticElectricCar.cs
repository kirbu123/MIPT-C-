namespace hw2.Cars;

public abstract class AAutomaticElectricCar : AElectricCar, IAutomatic
{
    public abstract string TransmissionType { get; }

    protected override string GetTransmissionDescription() =>
        $"автоматическая коробка передач ({TransmissionType})";
}
