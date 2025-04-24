using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursor, cursor_clicked;

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            UnityEngine.Cursor.SetCursor(cursor_clicked, new Vector2(7.0f, 5.0f), CursorMode.Auto);
        }

        else if(context.phase == InputActionPhase.Canceled)
        {
            UnityEngine.Cursor.SetCursor(cursor, new Vector2(7.0f, 5.0f), CursorMode.Auto);
        }
    }
}
