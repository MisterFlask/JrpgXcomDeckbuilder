using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


// Monobehavior that can perform topological sorting for appropriate initialization order.
public abstract class TopoMonobehavior : MonoBehaviour
{
    public TopoMonobehavior(string scriptName)
    {
        this.ScriptName = scriptName;
    }
    public virtual string ScriptName { get; set; }

    public virtual List<string> Dependencies { get; set; } = new List<string>();


    private bool Initialized = false;
    // Use this for initialization
    void Start()
    {
        InnerStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Initialized)
        {
            var allDependenciesExecuted = Dependencies.TrueForAll(item => ScriptExecutionTracker.ScriptsFinishedExecuting.Contains(item));
            if (!Initialized && allDependenciesExecuted)
            {
                Initialize();
                ScriptExecutionTracker.ScriptsFinishedExecuting.Add(ScriptName);
                this.Initialized = true;
            }
            InnerUpdate();
        }
    }

    public abstract void Initialize();

    public virtual void InnerStart()
    {

    }
    public virtual void InnerUpdate()
    {

    }
}
