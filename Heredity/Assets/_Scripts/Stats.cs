using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
public class Stats : ScriptableObject
{
    [SerializeField] private float _temp;
    [SerializeField] private float _humidity;
    [SerializeField] private int _lifeSpan;
    [SerializeField] private float _stressTolerance;
    [SerializeField] private int _rejectionTime;
    [SerializeField] private int _reproductionCooldown;

    public DNA Dna { get; private set; }

    private void OnValidate()
    {
        var alleles = new List<Allele>()
        {
            new(AlleleType.Temperature, _temp, "AA"),
            new(AlleleType.Humidity, _humidity, "AA"),
            new(AlleleType.LifeSpan, _lifeSpan, "AA"),
            new(AlleleType.StressTolerance, _stressTolerance, "AA"),
            new(AlleleType.RejectionTime, _rejectionTime, "AA"),
            new(AlleleType.ReproductionCooldown, _reproductionCooldown, "AA")
        };

        Dna = new DNA(alleles);
    }
}
