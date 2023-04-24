using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class DNA
{
    public List<Allele> Alleles { get; private set; } = new();
    private static readonly Random random = new();

    public DNA(List<Allele> alleles)
    {
        Alleles = alleles;
    }
    public DNA(DNA dna)
    {
        Alleles = new List<Allele>(dna.Alleles);
    }
    public DNA(DNA dna, DNA dna1)
    {
        foreach (var allele in dna.Alleles)
        {
            var type = allele.Type;

            var allele1 = dna1.Alleles.FirstOrDefault(r => r.Type == type);

            var c = allele.Domination[random.Next(2)];
            var c1 = allele1!.Domination[random.Next(2)];
            var domination = $"{c}{c1}";
                
            var calculator = AlleleCalculatorFactory.GetCalculator(type);
            var value = calculator.Calculate(allele.Value, allele1.Value, domination);
                
            Alleles.Add(new Allele(type, value, domination));
        }
    }
    
    public object GetValue(AlleleType type)
    {
        var allele = Alleles.FirstOrDefault(a => a.Type == type);
        if (allele == null)
        {
            throw new ArgumentException($"No allele found for type {type}");
        }
        return allele.Value;
    }
}
