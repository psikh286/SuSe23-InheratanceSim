using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

[System.Serializable]
public class DNA
{
    public List<Allele> Alleles;
    
    public void Init(DNA dna, DNA dna1 = null)
    {
        if (dna1 == null)
        {
            Alleles = new List<Allele>(dna.Alleles);
        }
        else
        {
            foreach (var allele in dna.Alleles)
            {
                var type = allele.Type;

                var allele1 = dna1.Alleles.FirstOrDefault(r => r.Type == type);

                var c = allele.Domination[Random.Range(0, 2)];
                var c1 = allele1!.Domination[Random.Range(0, 2)];
                var domination = $"{c}{c1}";
                
                var calculator = AlleleCalculatorFactory.GetCalculator(type);
                var value = calculator.Calculate(allele.Value, allele1.Value, domination);
                
                Alleles.Add(new Allele(type, value, domination));
            }
        }
    }
}
