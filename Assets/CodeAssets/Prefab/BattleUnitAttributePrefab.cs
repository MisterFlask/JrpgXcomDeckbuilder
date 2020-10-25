using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleUnitAttributePrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public CustomGuiText Text;
    public AbstractStatusEffect CorrespondingAttribute { get; set; }
    public BattleUnitAttributesHolder Holder {get;}


    public void Initialize(AbstractStatusEffect attr, BattleUnitAttributesHolder holder)
    {
        this.CorrespondingAttribute = attr;
        image.SetProtoSprite(CorrespondingAttribute.ProtoSprite);
        Text.SetText(attr.Stacks.ToString());
        attr.CorrespondingPrefab = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.CorrespondingAttribute == null)
        {
            return;
        }
        ExplainerPanel.ShowStatusEffectHelp(this.CorrespondingAttribute);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExplainerPanel.Hide();
    }
}