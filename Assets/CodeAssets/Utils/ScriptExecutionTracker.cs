using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScriptExecutionTracker
{

    public static bool HasCardInstantiatorLoaded = false;

    public static bool IsUiStateManagerReadyToDisableGameobjects => HasCardInstantiatorLoaded;




// TODO: I'm not actually doing this yet, but I should.
    public static List<string> ScriptsFinishedExecuting = new List<string>();
}
