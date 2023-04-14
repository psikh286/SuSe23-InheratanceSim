public interface IAlleleCalculator
{
    object Calculate(object data, object data1, string domination);
}

public class TemperatureCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return (float)data;
    }
}

public class HumidityCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return (int)data;
    }
}

public class LifeSpanCalculator : IAlleleCalculator
{
    public object Calculate(object data, object data1, string domination)
    {
        return (double)data;
    }
}