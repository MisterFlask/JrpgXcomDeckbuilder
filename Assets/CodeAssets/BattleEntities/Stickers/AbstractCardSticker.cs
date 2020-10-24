using UnityEngine;
using System.Collections;

public abstract class AbstractCardSticker : MonoBehaviour
{

    public CardStickerPrefab Prefab { get; set; }
    public ProtoGameSprite ProtoSprite { get; set; } = ImageUtils.ProtoGameSpriteFromGameIcon();

    public string Title { get; set; } = "Card Sticker Title";

    public string Description { get; set; } = "Card Sticker Description";

    public virtual void OnCardPlayed(AbstractCard card)
    {

    }

    public virtual void OnCardDrawn(AbstractCard card)
    {

    }

    public virtual void OnEndOfTurn(AbstractCard card)
    {

    }
}
