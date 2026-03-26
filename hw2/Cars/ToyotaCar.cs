namespace hw2.Cars;

public class ToyotaCar : AAutomaticGasCar
{
    public override string Brand => "Toyota";
    public override string Model => "Camry";
    public override int SeatCount => 5;
    public override string Country => "Япония";
    public override int Year => 2023;
    public override int HorsePower => 181;
    public override string FuelType => "Бензиновый";
    public override string TransmissionType => "6-ступенчатый автомат";

    protected override string GetExtrasDescription() =>
        "Toyota Safety Sense, Apple CarPlay, Android Auto";
}
