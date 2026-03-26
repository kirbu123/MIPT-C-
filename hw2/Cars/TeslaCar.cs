namespace hw2.Cars;

public class TeslaCar : AAutomaticElectricCar
{
    public override string Brand => "Tesla";
    public override string Model => "Model S";
    public override int SeatCount => 5;
    public override string Country => "США";
    public override int Year => 2023;
    public override int BatteryCapacityKWh => 100;
    public override int RangeKm => 637;
    public override string TransmissionType => "одноступенчатый редуктор";

    protected override string GetExtrasDescription() =>
        "Android-система на борту, автопилот, беспроводная зарядка";
}
