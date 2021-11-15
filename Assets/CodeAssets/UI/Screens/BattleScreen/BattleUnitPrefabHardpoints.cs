using UnityEngine;

public class BattleUnitPrefabHardpoints: MonoBehaviour
{
    public Transform Center;
    public Transform Center2;
    public Transform Center3;

    public Transform LeftOrMuzzle;
    public Transform Bottom;
    public Transform Top;


    public Transform FromHardpointLocation(HardpointLocation location)
    {
        if (location == HardpointLocation.CENTER)
        {
            return Center;
        }
        if (location == HardpointLocation.LEFT)
        {
            return LeftOrMuzzle;
        }
        if (location == HardpointLocation.BOTTOM)
        {
            return Bottom;
        }

        return Center;
    }
}