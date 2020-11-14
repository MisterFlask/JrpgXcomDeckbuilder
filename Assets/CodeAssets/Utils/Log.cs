using UnityEngine;
using System.Collections;

public class Log: MonoBehaviour
{
    public Log()
    {
        Instance = this;
    }

    public static Log Instance;

    public static void Info(string msg)
    {
        if (Instance == null)
        {
            return;
        }

        Instance.DebugInner(msg);
    }

    public static void Error(string msg, System.Diagnostics.StackTrace st = null)
    {
        if (Instance == null)
        {
            return;
        }
        Instance.Err(msg, st);
    }

    public void Err(string msg, System.Diagnostics.StackTrace st)
    {
        Debug.LogError(msg + $" [trace is <color=red>{st.ToString()}</color>]");
    }
    public void DebugInner(string msg)
    {
        Debug.Log(msg);
    }
}
