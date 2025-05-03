using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Swipe : MonoBehaviour
{
    private InputSysAction inputAction;
    [SerializeField] private float minimumSwipeMagnitute = 1f;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    private Vector2 _swipeDirection;
    // Start is called before the first frame update
    void Start()
    {
        inputAction = new InputSysAction();
        inputAction.ScreenMovement.Enable();
        inputAction.ScreenMovement.Touch.started += ProcessTouchComplete;
        inputAction.ScreenMovement.Swipe.performed += ProcessSwipeDelta;
    }
    private void ProcessSwipeDelta(InputAction.CallbackContext context)
    {
        _swipeDirection = context.ReadValue<Vector2>();
    }
    private void ProcessTouchComplete(InputAction.CallbackContext context)
    {
        
        

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //transform.position = new Vector3(_swipeDirection.x, transform.position.y, transform.position.z);



        //Vector2 convPos = Camera.main.ScreenToWorldPoint(_swipeDirection);
        //gameObject.GetComponent<Rigidbody2D>().MovePosition(convPos);

    }
    private void Update()
    {
        //if(transform.position.x <= maxX && transform.position.x >= minX)
        //{
            gameObject.GetComponent<Rigidbody2D>().velocity = -transform.right * _swipeDirection.x;
        //}
        //else
        //{
            
        //}
        //else if(transform.position.x > maxX)
        //{
        //    transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        //}
        //else if(transform.position.x < minX)
        //{
        //    transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        //}
        
    }
}
