using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BattleUnitAttributePrefab : MonoBehaviour
{
    public Image image;
    public CustomGuiText Text;
    public AbstractStatusEffect CorrespondingAttribute { get; }
    public BattleUnitAttributesHolder Holder {get;}


    public void Initialize(AbstractStatusEffect attr, BattleUnitAttributesHolder holder)
    {
        var protoSprite = attr.ProtoSprite.ToGameSpriteImage();
        image.sprite = protoSprite.Sprite;
        image.color = protoSprite.Color;
        Text.SetText(attr.Stacks.ToString());
        attr.CorrespondingPrefab = this;
    }
}