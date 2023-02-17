using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICharacterButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ScrollRect ParentSR;

    private void Awake()
    {
        ParentSR = transform.parent.parent.parent.parent.parent.GetComponent<ScrollRect>();
    }

    public void OnBeginDrag(PointerEventData e)
    {
        ParentSR.OnBeginDrag(e);
    }

    public void OnDrag(PointerEventData e)
    {
        ParentSR.OnDrag(e);
    }

    public void OnEndDrag(PointerEventData e)
    {
        ParentSR.OnEndDrag(e);
    }
}