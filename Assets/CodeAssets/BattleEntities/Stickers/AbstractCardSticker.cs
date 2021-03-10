using UnityEngine;
using System.Collections;

public abstract class AbstractCardSticker : MonoBehaviour
{

    public CardStickerPrefab Prefab { get; set; }
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public string Title_deprecated { get; set; } = "Card Sticker Title";

    public string Description_deprecated { get; set; } = "Card Sticker Description";

    /// <summary>
    /// Added onto the end of the card description.
    /// </summary>
    public string CardDescriptionAddendum { get; set; } = "";

    public virtual void OnAddedToCardInner(AbstractCard card)
    {
        // do stuff like change its attack damage or whatever
    }

    public void OnAddedToCard(AbstractCard card)
    {
        
    }

    public AbstractCardSticker CopySticker()
    {
        return this.MemberwiseClone() as AbstractCardSticker;
    }

    public virtual void OnCardPlayed(AbstractCard card, AbstractBattleUnit target)
    {

    }

    public virtual void OnCardDrawn(AbstractCard card)
    {

    }

    public virtual void OnEndOfTurn(AbstractCard card)
    {

    }

    /// <summary>
    /// Return "false" if you want to avoid having this sticker attached to the card provided.
    /// </summary>
    public virtual bool IsCardTagApplicable(AbstractCard card)
    {
        return true;
    }
}
