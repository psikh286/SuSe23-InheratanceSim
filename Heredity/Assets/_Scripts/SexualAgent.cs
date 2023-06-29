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
    private const float _searchingRadius = 2.5f;

    private SpriteRenderer _renderer;

    protected override void OnDnaInit()
    {
        base.OnDnaInit();
        
        _renderer = GetComponent<SpriteRenderer>();
        PickGender();
        _speed = _radius / 20f;
        
        _reproductionCooldownCount = _reproductionCooldown;
        _state = SexualAgentStates.Cooldown;
        
        TickManager.OnTick += OnTick;
    }

    protected override void OnTick()
    {
        base.OnTick();

        switch (_state)
        {
            case SexualAgentStates.LookingForMate:
                if(Male) LookForMate();
                if(_targetMate is not null) break;
                
                if(_pointReached) PickTargetPoint();
                Walk();
                break;
            
            case SexualAgentStates.WalkingToMate:
                if (_pointReached)
                {
                    _state = SexualAgentStates.Reproducing;
                    _targetMate._state = SexualAgentStates.Reproducing;
                    break;
                }
                Walk();
                break;
            
            case SexualAgentStates.WaitingForMate:
                if (_targetMate is null) _state = SexualAgentStates.LookingForMate;
                break;
            
            case SexualAgentStates.Reproducing:
                if(Male) _targetMate.Impregnate(Dna);
                _reproductionCooldownCount = _reproductionCooldown;
                _targetMate = null;
                _state = SexualAgentStates.Cooldown;
                break;
            
            case SexualAgentStates.Cooldown:
                _reproductionCooldownCount--;
                
                if(_pointReached) PickTargetPoint();
                Walk();
                
                if (_reproductionCooldownCount > 0) break;
                
                _state = SexualAgentStates.LookingForMate;
                break;
        }
    }
    
    private void LookForMate()
    {
        // ReSharper disable once Unity.PreferNonAllocApi
        var hits = Physics2D.OverlapCircleAll(transform.position, _searchingRadius);

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out SexualAgent agent)) continue;
            if (agent.Male) continue;
            if (_unimpressedFemales.Contains(agent)) continue;
            
            PotentialMateFound(agent);
            if (_targetMate is not null) break;
        }
    }
    private void PotentialMateFound(SexualAgent female)
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
    }
    
    private bool RequestMate(SexualAgent male)
    {
        if (_targetMate is not null || _state != SexualAgentStates.LookingForMate) return false;
        if ((float)random.NextDouble() >= 0.8f) return false;
        
        _targetMate = male;
        _targetPoint = transform.position;
        _state = SexualAgentStates.WaitingForMate;
        
        return true;
    }
    private void Impregnate(DNA maleDna)
    {
        if(!this) return;
        var l = this;
        Destroy(l.gameObject.GetComponent<DnaInitializer>());
        var r = Instantiate(l, transform.position, Quaternion.identity);
        r.InitializeDNA(maleDna, Dna);
    }

    private void PickGender()
    {
        Male = random.Next(2) == 1;
        _renderer.color = Male ? Color.cyan : Color.magenta;
    }

    private void Walk() => transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed);
    private bool _pointReached => Vector3.Distance(transform.position, _targetPoint) == 0f;

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        var position = transform.position;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, _radius);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(position, _searchingRadius);
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