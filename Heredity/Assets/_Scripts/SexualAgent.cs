using System.Collections.Generic;
using UnityEngine;

public class SexualAgent : Agent
{
    public bool Male;

    public override void Init(List<Allele> stats, List<Allele> stats1 = null)
    {
        base.Init(stats, stats1);

        Male = Random.value > 0.5f;
    }

    private void Reproduce()
    {
        var path = ReproductionCount == GlobalSettings.ReproductionTypeCount
            ? "AsexualAgent"
            : "SexualAgent";
        var agent = Resources.Load<Agent>("Agents/" + path);
        
        var r = Instantiate(agent, transform.position, Quaternion.identity);
        r.Init(_dna);
    }
}
