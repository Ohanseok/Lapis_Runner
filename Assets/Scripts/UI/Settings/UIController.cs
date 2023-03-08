using UnityEngine;
using UnityEngine.Events;

public enum TYPE_POPUP
{
    MAILBOX,
    SETTINGS,
    RANKING,
    COLLECTIONS,
    DUNGEONS,
    SHOP,
    GACHA,
    MISSIONS,
    TRAINING,
    BUFFS,
    CHARACTERS,
    CULSUK,
    SKILL
}

public class UIController : MonoBehaviour
{
    public UnityAction Closed;

    [Header("Type")]
    [SerializeField] private TYPE_POPUP type_popup;

    public TYPE_POPUP TYPE => type_popup;

    public void CloseScreen()
    {
        Closed.Invoke();
    }
}
