using UnityEngine;

public class DnaInitializer : MonoBehaviour
{
    [SerializeField] private Stats _stats;

    private Agent agent;

    private void Start()
    {
        agent = GetComponent<Agent>();
        agent.InitializeDNA(_stats.Dna);
    }
}
