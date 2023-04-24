using UnityEngine;

public class TempSensitive : Modifier
{
    private float _temperature;

    protected override void GatherValues()
    {
        base.GatherValues();
        
        _temperature = (float)_agent.Dna.GetValue(AlleleType.Temperature);
    }

    protected override void OnTick()
    {
        base.OnTick();

        var tempDif = Mathf.Abs(_temperature - GlobalSettings.Temperature) * 0.01f;

        // Increase stress level
        if (tempDif > GlobalSettings.TempPercent) _agent.ModifyStressLevel(tempDif);
    }
}
