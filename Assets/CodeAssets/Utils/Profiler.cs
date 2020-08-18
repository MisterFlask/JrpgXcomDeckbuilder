using UnityEngine;
using System.Collections;
using System;

public static class Profiler
{

    public static void Profile(string name, Action func)
    {
        var startTime = DateTime.Now;
        func();
        var endTime = DateTime.Now;
        var millis = endTime - startTime;
        Debug.Log($"Finished {name} in {millis.TotalMilliseconds}");
    }

}
