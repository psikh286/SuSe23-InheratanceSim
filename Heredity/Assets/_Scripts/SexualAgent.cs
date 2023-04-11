using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SexualAgent : Agent
{
    public bool Male;
    private SexualAgent _targetMate;
    private List<SexualAgent> _unimpressedFemales = new(); 
    private SexualAgentStates _state = SexualAgentStates.LookingForMate;

    private void OnEnable() => TickManager.OnTick += OnTick;
    private void OnDisable() => TickManager.OnTick -= OnTick;
    
    private void Start()
    {
        Init(new List<Allele>());
        _stats = new Characteristics();
    }
    private void Update()
    {
        switch (_state)
        {
            case SexualAgentStates.Cooldown:
                
                //do time logic here
                _state = SexualAgentStates.LookingForMate;
                break;
            case SexualAgentStates.LookingForMate:
                if (Vector3.Distance(transform.position, _targetPoint) == 0f)
                {
                    _targetPoint = _camera.ViewportToWorldPoint(new Vector2(Random.value, Random.value)) + Vector3.forward * 10f;
                }

                transform.position = Vector3.MoveTowards(transform.position, _targetPoint, Time.deltaTime);
            
                break;
            case SexualAgentStates.WaitingForMate:
                break;
            case SexualAgentStates.WalkingToMate:
                if (Vector3.Distance(transform.position, _targetPoint) == 0f)
                {
                    _targetMate.SendDNA(_dna);
                    _state = SexualAgentStates.Cooldown;
                    _targetPoint = _camera.ViewportToWorldPoint(new Vector2(Random.value, Random.value)) + Vector3.forward * 10f;
                }

                transform.position = Vector3.MoveTowards(transform.position, _targetPoint, Time.deltaTime);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void Init(List<Allele> stats, List<Allele> stats1 = null)
    {
        base.Init(stats, stats1);

        //Male = Random.value > 0.5f;
    }

    protected override void OnTick()
    {
        base.OnTick();
        
        if(Male && _state == SexualAgentStates.LookingForMate) LookForMate();
    }

    
    private void LookForMate()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, 200f);

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent(out SexualAgent agent)) continue;
            if (_unimpressedFemales.Contains(agent)) continue;
            if (PotentialMateFound(agent)) break;
        }
    }
    private bool RequestMate(SexualAgent male)
    {
        if (_targetMate != null) return false;
        
        _targetMate = male;
        _state = SexualAgentStates.WaitingForMate;
        
        return true;
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
            StartCoroutine(ForgetRejection(_stats.RejectionTime, female));
        }

        return accepted;
    }
    private IEnumerator ForgetRejection(float time, SexualAgent female)
    {
        if (time <= 0) yield break;
        
        yield return time;
        _unimpressedFemales.Remove(female);
    }

    private void SendDNA(List<Allele> dna)
    {
        Reproduce(dna);
        _state = SexualAgentStates.Cooldown;
    }


    private void Reproduce(List<Allele> maleDna)
    {
        var r = Instantiate(this, transform.position, Quaternion.identity);
        r.Init(maleDna, _dna);
    }
    // private void Reproduce()
    // {
    //     var path = _willingToSwitch && ReproductionCount == GlobalSettings.ReproductionTypeCount
    //         ? "AsexualAgent"
    //         : "SexualAgent";
    //     var agent = Resources.Load<Agent>("Agents/" + path);
    //
    //     var r = Instantiate(agent, transform.position, Quaternion.identity);
    //     r.Init(_dna);
    // }
}

public enum SexualAgentStates
{
    Cooldown,
    LookingForMate,
    WalkingToMate,
    WaitingForMate
}