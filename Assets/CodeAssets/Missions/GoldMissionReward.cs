using UnityEngine;
using System.Collections;

public class GoldMissionReward : AbstractMissionReward
{
    private int MoneyEarned { get; set; }
    public GoldMissionReward(int amount)
    {
        MoneyEarned = amount;
    }
    public override void OnReward()
    {
        GameState.Instance.money += MoneyEarned;
    }

    public override string Description()
    {
        return $"Earn {MoneyEarned} gold.";
    }
}
