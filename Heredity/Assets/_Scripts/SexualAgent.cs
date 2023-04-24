using System.Collections.Generic;
using UnityEngine;

public class SexualAgent : Agent
{
    public bool Male;
    private SexualAgent _targetMate;
    private List<SexualAgent> _unimpressedFemales = new(); 
    private SexualAgentStates _state = SexualAgentStates.LookingForMate;

    private bool _isWalking;

    private float _speed;
    
    private void Start()
    {
        Male = random.Next(2) == 1;
        GetComponent<SpriteRenderer>().color = Male ? Color.cyan : Color.magenta;
        _speed = _radius / TickManager.TickDelay;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, _targetPoint) == 0f)
        {
            _isWalking = false;
            return;
        }
        
        _isWalking = true;

        transform.position = Vector3.MoveTowards(transform.position, _targetPoint,_speed * Time.deltaTime);
    }
    
    protected override void OnTick()
    {
        base.OnTick();
        
        switch (_state)
        {
            case SexualAgentStates.Cooldown:
                _reproductionCooldownCount--;
                if (_reproductionCooldownCount > 0) break;
                PickTargetPoint();
                _reproductionCooldownCount = _reproductionCooldown;
                _targetMate = null;
                _state = SexualAgentStates.LookingForMate;
                break;
            
            case SexualAgentStates.LookingForMate:
                if(Male) LookForMate();
                if(!_isWalking) PickTargetPoint();
                break;
            
            case SexualAgentStates.WalkingToMate:
                if (_isWalking) break;
                _state = SexualAgentStates.Reproducing;
                break;
            
            case SexualAgentStates.WaitingForMate:
                break;
            
            case SexualAgentStates.Reproducing:
                _targetMate.Impregnate(Dna);
                _state = SexualAgentStates.Cooldown;
                break;
        }
    }

    
    private void LookForMate()
    {
        // ReSharper disable once Unity.PreferNonAllocApi
        var hits = Physics2D.OverlapCircleAll(transform.position, _radius);

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out SexualAgent agent)) continue;
            if (agent.Male) continue;
            if (_unimpressedFemales.Contains(agent)) continue;
            if (PotentialMateFound(agent)) break;
        }
    }
    private bool PotentialMateFound(SexualAgent female)
    {
        var accepted = female.RequestMate(this);

        if (accepted)
        {
            _targetMate = female;
            _targetPoint = _targetMate.transform.position;
            _state = SexualAgentStates.WalkingToMate;
        }
        else
        {
            _unimpressedFemales.Add(female);
        }

        return accepted;
    }
    
    private bool RequestMate(SexualAgent male)
    {
        if (_targetMate != null) return false;
        
        _targetMate = male;
        _targetPoint = transform.position;
        _state = SexualAgentStates.WaitingForMate;
        
        return true;
    }
    private void Impregnate(DNA maleDna)
    {
        var r = Instantiate(this, transform.position, Quaternion.identity);
        r.InitializeDNA(maleDna, Dna);
        _state = SexualAgentStates.Cooldown;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

public enum SexualAgentStates
{
    Cooldown,
    LookingForMate,
    WalkingToMate,
    WaitingForMate,
    Reproducing
}