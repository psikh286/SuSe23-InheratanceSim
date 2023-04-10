using UnityEngine;

public class AsexualAgent : Agent
{
    

    
    
    

    private void Reproduce()
    {
        var path = ReproductionCount == GlobalSettings.ReproductionTypeCount
            ? "SexualAgent"
            : "AsexualAgent";
        var agent = Resources.Load<Agent>("Agents/" + path);
        
        var r = Instantiate(agent, transform.position, Quaternion.identity);
        r.Init(_dna);
    }
}