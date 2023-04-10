public class Allele
{
    public string Domination;
    public AlleleType Type;

    public Allele(AlleleType type, string domination)
    {
        Type = type;
        Domination = domination;
    }
}
