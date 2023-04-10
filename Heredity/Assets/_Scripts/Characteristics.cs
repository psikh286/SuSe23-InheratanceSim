public class Characteristics
{
    public float OptimumTemperature { get; private set; }
    public float OptimumHumidity { get; private set; }
    public float LifeSpan { get; private set; }

    public Characteristics(float optimumTemp, float optimumHumid, float lifeSpan)
    {
        OptimumTemperature = optimumTemp;
        OptimumHumidity = optimumHumid;
        LifeSpan = lifeSpan;
    }
}
