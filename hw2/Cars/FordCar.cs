namespace hw2.Cars;

public class FordCar : AManualGasCar
{
    public override string Brand => "Ford";
    public override string Model => "Focus";
    public override int SeatCount => 5;
    public override string Country => "США";
    public override int Year => 2022;
    public override int HorsePower => 150;
    public override string FuelType => "Бензиновый";
    public override int GearCount => 6;

    protected override string GetExtrasDescription() =>
        "SYNC 3 мультимедиа, подогрев сидений, круиз-контроль";
}
