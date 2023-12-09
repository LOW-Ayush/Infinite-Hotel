using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshaircursor : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = CursorPos;
    }
}
