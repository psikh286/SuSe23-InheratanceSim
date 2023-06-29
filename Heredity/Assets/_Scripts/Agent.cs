using System;
using UnityEngine;
using Random = System.Random;

public abstract class Agent : MonoBehaviour
{
    public DNA Dna { get; private set; }
    
    private int _age;
    private float _stressLevel;
    protected int _reproductionCooldown;
    protected int _reproductionCooldownCount;
    
    private float _stressTolerance;
    private int _lifeSpan;
    
    protected Vector2 _targetPoint;
    [SerializeField] protected float _radius = 2f;
    protected static readonly Random random = new();

    public Action OnDnaInitialized;
    
    public void InitializeDNA(DNA dna, DNA dna1 = null)
    {
        Dna = dna1 != null ? new DNA(dna, dna1) : new DNA(dna);
        
        OnDnaInitialized?.Invoke();
    }
    protected virtual void OnDnaInit()
    {
        OnDnaInitialized -= OnDnaInit;
        
        _stressTolerance = (float)Dna.GetValue(AlleleType.StressTolerance);
        _lifeSpan = (int)Dna.GetValue(AlleleType.LifeSpan);
        _reproductionCooldown = (int)Dna.GetValue(AlleleType.ReproductionCooldown);
    }
    
    private void Awake()
    {
        PickTargetPoint();
        OnDnaInitialized += OnDnaInit;
    }
    private void OnDestroy()
    {
        TickManager.OnTick -= OnTick;
    }

    private void Die()
    { 
        if(gameObject) Destroy(gameObject);
    }
    protected virtual void OnTick()
    {
        // Increase age
        _age++;
    
        // Death???
        if (_age >= _lifeSpan && GetRandomValue() > GlobalSettings.DeathResistancePercent)
        {
            Die();
        }
    }
    public void ModifyStressLevel(float value)
    {
        _stressLevel += value;
        
        if (_stressLevel >= _stressTolerance && GetRandomValue() > GlobalSettings.DeathResistancePercent)
        {
            Die();
        }
    }
    
    protected void PickTargetPoint()
    {
        var position = transform.position;
        var origin = new Vector2(position.x, position.y);
        
        var angle = 2f * Mathf.PI * (float)random.NextDouble();
        var distance = (float)random.NextDouble() * _radius;

        var dir = angle.ToVector2();
        
        _targetPoint = origin + dir * distance;

        var hit = Physics2D.Raycast(origin, dir, distance, 1 << 9);
        if (!hit) return;

        /*
        var oldDir = dir;
        dir = Vector2.Reflect(dir, hit.normal);
        var R = (Vector2.Angle(oldDir, dir) / 360f) * Mathf.PI;
        var R1 = Mathf.Acos(Vector2.Dot(oldDir, dir) / (oldDir.magnitude * dir.magnitude));
        var D = Mathf.Asin((Mathf.Sin(R) * hit.distance) / _radius);
        var X = (float)Math.PI - R - D;


        var x = (Mathf.Sin(X) * _radius) / Mathf.Sin(R);
        
        print(x);
        
        
        _targetPoint = hit.point + dir * x;

        
        Debug.DrawLine(origin, hit.point, Color.yellow, 5f);
        Debug.DrawRay(hit.point, dir, Color.cyan, 5f);
        
        Debug.DrawRay(hit.point, hit.normal, Color.green, 5f);*/
        
        dir = Vector2.Reflect(dir, hit.normal);
        _targetPoint = origin + dir * distance;
        
        hit = Physics2D.Raycast(origin, dir, distance, 1 << 9);
        if (!hit) return;
        
        _targetPoint = origin;
    }
    private static float GetRandomValue()
    {
        return (float)random.NextDouble();
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(_targetPoint, Vector3.one * 0.2f);
    }
}
