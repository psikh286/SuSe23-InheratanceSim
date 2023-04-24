using System.Collections.Generic;

[System.Serializable]
public class Characteristics
{
    public readonly float OptimumTemperature;
    public readonly float OptimumHumidity;
    public readonly float LifeSpan;
    public readonly float StressTolerance;
    public readonly float RejectionTime;
    public readonly int ReproductionCooldown;

    public Dictionary<AlleleType, object> Values { get; private set; }
    
    public Characteristics(Dictionary<AlleleType, object> values)
    {
        Values = values;
    }

}