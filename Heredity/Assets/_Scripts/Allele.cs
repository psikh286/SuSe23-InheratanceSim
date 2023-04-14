[System.Serializable]
public class Allele
{
    public string Domination;
    public AlleleType Type;
    public object Value;

    public Allele(AlleleType type, object value, string domination)
    {
        Type = type;
        Value = value;
        Domination = domination;
    }
}
