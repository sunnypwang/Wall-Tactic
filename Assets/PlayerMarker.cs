using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : MonoBehaviour {
    public int row;
    public int col;
    public void SetPos(int i, int j)
    {
        row = i;
        col = j;
        float x = 152.5f + (float)(50 * j);
        float y = 352.5f + (float)(40 * i);
        transform.position = new Vector2(x, y);
    }

}
