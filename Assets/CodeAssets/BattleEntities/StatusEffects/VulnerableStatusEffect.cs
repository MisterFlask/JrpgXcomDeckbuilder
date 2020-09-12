using UnityEngine;
using System.Collections;

public class VulnerableStatusEffect : AbstractStatusEffect
{
    public VulnerableStatusEffect()
    {
        Name = "Vulnerable";
    }
    
    public override string Description => $"increases damage received by 50%";

    public override float DamageReceivedMultiplier()
    {
        return 1.5f;
    }
}
