using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Agent : MonoBehaviour
{
    #region Static

   // public int ReproductionCount;
   // protected bool _willingToSwitch;
    protected List<Allele> _dna = new();

    protected Characteristics _stats;

    #endregion

    #region Dynamic

    private float _age;
    private float _stressLevel;
    protected float _reproduceCooldown;

    #endregion

    protected Vector2 _targetPoint;
    private float _speed = 1f;
    protected Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _targetPoint = _camera.ViewportToWorldPoint(new Vector2(Random.value, Random.value)) + Vector3.forward * 10f;
    }
    
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
    private void Die()
    {
        Destroy(gameObject);
    }

    


    protected virtual void OnTick()
    {
        var tempDif = Mathf.Abs(_stats.OptimumTemperature - GlobalSettings.Temperature) * 0.01f;

        //increase StressLevel
        if (tempDif > GlobalSettings.TempPercent)
        {
            _stressLevel += tempDif;
        }
        
        //increase age
        _age++;

        //death???
        if ((_stressLevel >= _stats.StressTolerance || _age >= _stats.LifeSpan) && Random.value > GlobalSettings.DeathResistancePercent)
        {
            Die();
        }
        
        var stressDif = Mathf.Abs(_stats.StressTolerance - _stressLevel);
        
        //switch reproduction type
        //  if (stressDif > GlobalSettings.ReproductionMutationPercent)
        // {
        //     ReproductionCount = GlobalSettings.ReproductionTypeCount;
        //     _willingToSwitch = true;
        // }
    }
}
