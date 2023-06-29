public interface IAlleleCalculator
{
    object Calculate(object data, object data1, string domination);
}

public class TemperatureCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return ((float)data + (float)data1) * 0.5f;
    }
}

public class HumidityCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return (float)data;
    }
}

public class LifeSpanCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return (int)(((int)data + (int)data1) * 0.5f);
    }
}

public class RejectionTimeCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return (int)data;
    }
}

public class ReproductionCooldownCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return (int)data;
    }
}

public class StressToleranceCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return (float)data;
    }
}