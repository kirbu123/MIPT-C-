namespace hw2.Cars;

public class BmwCar : AAutomaticGasCar
{
    public override string Brand => "BMW";
    public override string Model => "5 Series";
    public override int SeatCount => 5;
    public override string Country => "Германия";
    public override int Year => 2023;
    public override int HorsePower => 252;
    public override string FuelType => "Бензиновый";
    public override string TransmissionType => "8-ступенчатый Steptronic";

    protected override string GetExtrasDescription() =>
        "iDrive мультимедиа, кожаный салон, адаптивная подвеска";
}
