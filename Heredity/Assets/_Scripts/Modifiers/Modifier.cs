using UnityEngine;

[RequireComponent(typeof(Agent))]
public abstract class Modifier : MonoBehaviour
{
    protected Agent _agent;
    
    private void Awake()
    {
        _agent = GetComponent<Agent>();
        _agent.OnDnaInitialized += GatherValues;
    }
    private void OnDestroy()
    {
        _agent.OnDnaInitialized -= GatherValues;
        TickManager.OnTick -= OnTick;
    }
    
    protected virtual void GatherValues()
    {
        TickManager.OnTick += OnTick;
    }
    
    protected virtual void OnTick(){}
}
