using System;
using System.Collections.Generic;

public static class AlleleCalculatorFactory
{
    private static readonly Dictionary<AlleleType, IAlleleCalculator> _calculators = new()
    {
        { AlleleType.Temperature, new TemperatureCalculator() },
        { AlleleType.Humidity, new HumidityCalculator() },
        { AlleleType.LifeSpan, new LifeSpanCalculator() },
        { AlleleType.RejectionTime, new RejectionTimeCalculator() },
        { AlleleType.ReproductionCooldown, new RejectionTimeCalculator() },
        { AlleleType.StressTolerance, new StressToleranceCalculator() }
    };

    public static IAlleleCalculator GetCalculator(AlleleType type)
    {
        if (!_calculators.ContainsKey(type))
            throw new ArgumentException($"No calculator found for allele type {type}");

        return _calculators[type];
    }
}