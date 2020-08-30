using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BattleUnitAttributePrefab : MonoBehaviour
{
    public Image image;
    public CustomGuiText Text;
    public AbstractBattleUnitAttribute CorrespondingAttribute { get; }
    public BattleUnitAttributesHolder Holder {get;}


    public void Initialize(AbstractBattleUnitAttribute attr, BattleUnitAttributesHolder holder)
    {
        var protoSprite = attr.ProtoSprite.ToGameSpriteImage();
        image.sprite = protoSprite.Sprite;
        image.color = protoSprite.Color;
        Text.SetText(attr.Stacks.ToString());
        attr.CorrespondingPrefab = this;
    }
}