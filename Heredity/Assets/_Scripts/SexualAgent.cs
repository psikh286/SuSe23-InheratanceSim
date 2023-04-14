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

    //private void OnEnable() => TickManager.OnTick += OnTick;
    //private void OnDisable() => TickManager.OnTick -= OnTick;
    
    private void Start()
    {
        Male = Random.value > 0.5f;
        if(!Male) GetComponent<SpriteRenderer>().color = Color.magenta;
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
                    _state = SexualAgentStates.Reproducing;
                    _targetPoint = _camera.ViewportToWorldPoint(new Vector2(Random.value, Random.value)) + Vector3.forward * 10f;
                }

                transform.position = Vector3.MoveTowards(transform.position, _targetPoint, Time.deltaTime);
                break;
            case SexualAgentStates.Reproducing:
                if(_targetMate) _targetMate.Impregnate(_dna);
                _state = SexualAgentStates.Cooldown;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    // protected override void OnTick()
    // {
    //     base.OnTick();
    //     
    //     if(Male && _state == SexualAgentStates.LookingForMate) LookForMate();
    // }

    #region LookingForMate
    private void LookForMate()
    {
        // ReSharper disable once Unity.PreferNonAllocApi
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
            //StartCoroutine(ForgetRejection(_dna.Characteristics.RejectionTime, female));
        }

        return accepted;
    }
    private IEnumerator ForgetRejection(float time, SexualAgent female)
    {
        if (time <= 0) yield break;
        
        yield return new WaitForSeconds(time);
        _unimpressedFemales.Remove(female);
    }
    

    #endregion

    #region Reproduction
    private void Impregnate(DNA maleDna)
    {
        var r = Instantiate(this, transform.position, Quaternion.identity);
        r._dna.Init(maleDna, _dna);
        _state = SexualAgentStates.Cooldown;
    }
    #endregion
    
}

public enum SexualAgentStates
{
    Cooldown,
    LookingForMate,
    WalkingToMate,
    WaitingForMate,
    Reproducing
}