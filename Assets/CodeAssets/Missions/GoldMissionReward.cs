using UnityEngine;
using System.Collections;

public class GoldMissionReward : AbstractMissionReward
{
    private int MoneyEarned { get; set; }
    public GoldMissionReward(int amount)
    {
        MoneyEarned = amount;
        this.Description = $"Earn {amount} gold.";
    }
    public override void OnReward()
    {
        GameState.Instance.money += MoneyEarned;
    }
}
