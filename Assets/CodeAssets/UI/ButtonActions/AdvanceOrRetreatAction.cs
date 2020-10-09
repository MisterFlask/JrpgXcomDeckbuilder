using UnityEngine;
using System.Collections;

public class AdvanceOrRetreatAction : MonoBehaviour
{
    public BattleUnitPrefab battleUnitPrefab;
    private AbstractBattleUnit battleUnit => battleUnitPrefab.UnderlyingEntity;

    // Use this for initialization
    void Start()
    {
        Require.NotNull(battleUnitPrefab);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AdvanceOrRetreatAsAppropriate()
    {
        if (BattleRules.CanAdvance(battleUnit))
        {
            ActionManager.Instance.PerformAdvanceActionIfPossible(battleUnit);
        }
        else if (BattleRules.CanFallBack(battleUnit))
        {
            ActionManager.Instance.PerformFallbackActionIfPossible(battleUnit);
        }
        else
        {
            ActionManager.Instance.Shout(battleUnit, "I can neither advance nor fall back!");
        }
    }
}
