using Assets.CodeAssets.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.CodeAssets.UI
{
    public class BattleAnnouncementText : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI Text;

        public static BattleAnnouncementText Instance;

        public void Awake()
        {
            Instance = this;
            Hide();
        }

        public static void Show(string text)
        {
            Instance.gameObject.SetActive(true);
            Instance.Text.text = text;
        }

        public static void Hide()
        {
            Instance.gameObject.SetActive(false);
        }
    }
}