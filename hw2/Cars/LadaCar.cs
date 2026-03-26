namespace hw2.Cars;

public class LadaCar : AManualGasCar
{
    public override string Brand => "Lada";
    public override string Model => "Vesta";
    public override int SeatCount => 5;
    public override string Country => "Россия";
    public override int Year => 2022;
    public override int HorsePower => 106;
    public override string FuelType => "Бензиновый";
    public override int GearCount => 5;

    protected override string GetExtrasDescription() =>
        "кондиционер, медиасистема Lada EnjoY Pro, парктроник";
}
