using UnityEngine;
using System.Collections;

public abstract class Intent
{
    public abstract void Execute();

    public abstract GameObject GeneratePrefab(GameObject parent);
}
