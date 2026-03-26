namespace hw2;

public interface ICar
{
    string GetDescription();
}

public interface IElectric
{
    int BatteryCapacityKWh { get; }
    int RangeKm { get; }
}

public interface IMechanicalEngine
{
    int HorsePower { get; }
    string FuelType { get; }
}

public interface IManual
{
    int GearCount { get; }
}

public interface IAutomatic
{
    string TransmissionType { get; }
}
