[System.Serializable]
public class Allele
{
    public readonly string Domination;
    public readonly AlleleType Type;
    public readonly object Value;

    public Allele(AlleleType type, object value, string domination)
    {
        Type = type;
        Value = value;
        Domination = domination;
    }
}
