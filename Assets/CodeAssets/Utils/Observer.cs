using UnityEngine;
using System.Collections;

public interface Observer<T>
{
    void Notify(T obj);
}
