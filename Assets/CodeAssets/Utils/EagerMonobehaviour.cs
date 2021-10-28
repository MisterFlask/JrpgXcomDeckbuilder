using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.CodeAssets.Utils
{
    /// <summary>
    ///  inheriting from this causes the AwakenOnSceneStart()
    /// </summary>
    public abstract class EagerMonobehaviour : MonoBehaviour
    {
        public abstract void AwakenOnSceneStart();


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void InitializeAllEagerMonobehaviours()
        {
            var types = Assembly
              .GetExecutingAssembly()
              .GetTypes();

            foreach (var t in types)
            {
                if (t.IsSubclassOf(typeof(EagerMonobehaviour)))
                {
                    var thingsOfThisClass = Resources.FindObjectsOfTypeAll(t);

                    foreach(var item in thingsOfThisClass)
                    {
                        var startable = (EagerMonobehaviour)item;
                        startable.AwakenOnSceneStart();
                        Log.Info("initializing eager monobehaviour: " + t.Name);
                    }
                }
            }
        }
    }
}