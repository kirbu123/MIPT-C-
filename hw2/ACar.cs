namespace hw2;

public abstract class ACar : ICar
{
    public abstract string Brand { get; }
    public abstract string Model { get; }
    public abstract int SeatCount { get; }
    public abstract string Country { get; }
    public abstract int Year { get; }

    protected abstract string GetTransmissionDescription();
    protected abstract string GetEngineDescription();
    protected virtual string GetExtrasDescription() => string.Empty;

    public virtual string GetDescription()
    {
        var extras = GetExtrasDescription();
        var extrasPart = string.IsNullOrEmpty(extras) ? string.Empty : $", {extras}";
        return $"{Brand} {Model}: {GetEngineDescription()}, {GetTransmissionDescription()}, {SeatCount} мест(а), производство {Country}, год выпуска {Year}{extrasPart}";
    }
}
