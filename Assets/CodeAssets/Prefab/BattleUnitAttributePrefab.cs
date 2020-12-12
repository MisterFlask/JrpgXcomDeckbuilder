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

    public void Update()
    {
        if (CorrespondingAttribute == null)
        {
            return;
        }

        if (CorrespondingAttribute.Stacks != 1 && CorrespondingAttribute.Stacks != 0)
        {
            Text.SetText(CorrespondingAttribute.Stacks.ToString());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExplainerPanel.Hide();
    }
}