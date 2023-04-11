[System.Serializable]
public class Characteristics
{
    public float OptimumTemperature { get; private set; }
    public float OptimumHumidity { get; private set; }
    public float LifeSpan { get; private set; }
    public float StressTolerance = 0.6f;
    public float RejectionTime = 5f;

    public Characteristics(float optimumTemp = 0f, float optimumHumid = 0f, float lifeSpan = 20f)
    {
        OptimumTemperature = optimumTemp;
        OptimumHumidity = optimumHumid;
        LifeSpan = lifeSpan;
    }
}
