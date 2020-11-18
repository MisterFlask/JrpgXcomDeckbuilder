using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterDetailsInRoster : MonoBehaviour
{
    public AbstractBattleUnit BattleUnit { get; set; }

    public Image CharacterImage;
    public TMPro.TextMeshProUGUI Title;
    public Button GetCardRewardButton;

    // Use this for initialization
    void Start()
    {
        GetCardRewardButton.onClick.AddListener(() => {
            if (CharacterIsEligibleForCardReward(BattleUnit))
            {
                ActionManager.Instance.PromptCardReward(BattleUnit);
            }
            else
            {
                Log.Error("Clicked card reward for button, but the soldier isn't eligible");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleUnit != null)
        {
            CharacterImage.SetProtoSprite(BattleUnit.ProtoSprite);
            Title.text = BattleUnit.CharacterName;
        }
        GetCardRewardButton.gameObject.SetActive(CharacterIsEligibleForCardReward(BattleUnit));
    }

    bool CharacterIsEligibleForCardReward(AbstractBattleUnit unit)
    {
        if (unit == null)
        {
            return false;
        }

        if (unit.NumberCardRewardsEligibleFor > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
