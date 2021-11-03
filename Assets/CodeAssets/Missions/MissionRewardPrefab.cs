using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MissionRewardPrefab : MonoBehaviour
{

    public Image image;
    public TMPro.TextMeshProUGUI Text;

    public AbstractMissionReward Reward;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Text.text = Reward.GenericDescription();
        image.SetProtoSprite(Reward.ProtoSprite);
    }
}
