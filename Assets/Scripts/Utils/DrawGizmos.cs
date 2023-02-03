using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    [SerializeField]
    private Transform topLeft;
    public Transform TopLeft { get { return topLeft; } }

    [SerializeField]
    private Transform bottomRight;
    public Transform BottomRight { get { return bottomRight; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 top = TopLeft.position;
        Vector3 bottom = BottomRight.position;

        Vector3 curr = new Vector3();
        curr.x = top.x + ((bottom.x - top.x) / 2);
        curr.y = bottom.y + ((top.y - bottom.y) / 2);
        curr.z = 0;

        Vector3 size = new Vector3();
        size.x = bottom.x - top.x;
        size.y = top.y - bottom.y;
        size.z = 0;

        Gizmos.DrawWireCube(curr, size);
    }
}
