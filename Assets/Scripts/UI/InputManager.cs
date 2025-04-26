using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject inventory_go;

    enum State
    {
        Normal,
        Inventory,
    };
    [SerializeField] State state;

    void OpenInventory()
    {
        inventory_go.SetActive(true);
        state = State.Inventory;
    }
    void CloseInventory()
    {
        inventory_go.SetActive(false);
        state = State.Normal;
    }



    public void OnMove(InputAction.CallbackContext cc)
    {
        player.Move(cc.ReadValue<Vector2>());
    }

    public void OnLeftClick(InputAction.CallbackContext cc)
    {
        if(state == State.Inventory)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new PointerEventData(GetComponent<EventSystem>()), results);
            print($"OnClick: {results[0]}");
            return;
        }

        if (cc.phase == InputActionPhase.Started)
        {
            player.StartSkill(0);
        }
        else if (cc.phase == InputActionPhase.Canceled)
        {
            player.EndSkill(0);
        }
    }
    public void OnRightClick(InputAction.CallbackContext cc)
    {
        if (state == State.Normal)
        {
            if (cc.phase == InputActionPhase.Started)
            {
                player.StartSkill(1);
            }
            else if (cc.phase == InputActionPhase.Canceled)
            {
                player.EndSkill(1);
            }
        }
    }

    public void OnInventory(InputAction.CallbackContext cc)
    {
        if (cc.phase == InputActionPhase.Started)
        {
            if (state == State.Inventory)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }


}
