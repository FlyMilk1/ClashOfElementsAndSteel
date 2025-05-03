using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorFollower : MonoBehaviour
{
    InputSysAction inputAction;
    Vector2 mousePos;
    private void Start()
    {

        inputAction = new InputSysAction();
        inputAction.UnitControl.Enable();
        inputAction.UnitControl.Drag.performed += Drag;
    }
    private void Drag(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {

        
        transform.position = mousePos;
    }
}
