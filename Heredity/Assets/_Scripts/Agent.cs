using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Agent : MonoBehaviour
{
    #region Static

    // public int ReproductionCount;
    // protected bool _willingToSwitch;
    //protected List<Allele> _dna = new();

    protected DNA _dna { get; private set; } = new();

    //protected Characteristics _stats;

    #endregion

    #region Dynamic

    private float _age;
    private float _stressLevel;
    protected int _reproductionCooldown;

    #endregion

    protected Vector2 _targetPoint;
    protected Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        if (_camera) _targetPoint = _camera.ViewportToWorldPoint(new Vector2(Random.value, Random.value)) +
                                    Vector3.forward * 10f;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    // protected virtual void OnTick()
    // {
    //     var tempDif = Mathf.Abs(_dna.Characteristics.OptimumTemperature - GlobalSettings.Temperature) * 0.01f;
    //
    //     //increase StressLevel
    //     if (tempDif > GlobalSettings.TempPercent)
    //     {
    //         _stressLevel += tempDif;
    //     }
    //     
    //     //increase age
    //     _age++;
    //
    //     //death???
    //     if ((_stressLevel >= _dna.Characteristics.StressTolerance || _age >= _dna.Characteristics.LifeSpan) && Random.value > GlobalSettings.DeathResistancePercent)
    //     {
    //         Die();
    //     }
    //     
    //     var stressDif = Mathf.Abs(_dna.Characteristics.StressTolerance - _stressLevel);
    //     
    //     //switch reproduction type
    //     //  if (stressDif > GlobalSettings.ReproductionMutationPercent)
    //     // {
    //     //     ReproductionCount = GlobalSettings.ReproductionTypeCount;
    //     //     _willingToSwitch = true;
    //     // }
    // }
}