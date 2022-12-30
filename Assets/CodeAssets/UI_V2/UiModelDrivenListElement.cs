using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CodeAssets.UI_V2
{
    public abstract class UiModelDrivenListElement<T>: MonoBehaviour
    {
        public T ModelObject { get; set; }

        public abstract void PopulateFromModelObject();
    }
}
