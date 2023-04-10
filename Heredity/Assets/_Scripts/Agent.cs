using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Agent : MonoBehaviour
{
    public int ReproductionCount;
    protected List<Allele> _dna = new();
    
    public virtual void Init(List<Allele> stats, List<Allele> stats1 = null)
    {
        if (stats1 == null)
        {
            _dna = stats;
            return;
        }

        for (var i = 0; i < stats.Count; i++)
        {
            var type = stats[0].Type;
            var c = stats[i].Domination[Random.Range(0, 2)];
            var c1 = stats1[i].Domination[Random.Range(0, 2)];
            var domination = $"{c}{c1}";
            
            _dna.Add(new Allele(type, domination));
        }
    }
}
