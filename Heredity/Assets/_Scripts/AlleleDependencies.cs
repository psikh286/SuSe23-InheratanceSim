using UnityEngine;


[CreateAssetMenu(fileName = "Allele", menuName = "Allele")] 
public class AlleleDependencies : ScriptableObject
{
    public AlleleType Type; 
    public Domination Domination;
    public Other Other;
}

public enum AlleleType
{
    Temperature,
    Humidity,
    LifeSpan
}
public enum Domination
{
    Dominant,
    Recessive,
    Heterozygous
}
public enum Other
{
    None,
    CoDominant,
    Incomplete
}
