using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeAssets.UI_V2.Buttons
{
    [RequireComponent(typeof(Button))]
    public class InitCampaignButton: MonoBehaviour
    {
        public void Start()
        {
            this.gameObject.GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            Debug.Log("Iniitailizing campaign");
            CampaignStarter.InitializeRoster();
        }

    }
}
