using UnityEngine;
using System.Collections;

public interface AwaitableCoroutiner
{
    void Run();

    bool IsFinished();
}
