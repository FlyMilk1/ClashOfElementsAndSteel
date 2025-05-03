using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class unitPlace : MonoBehaviour
{
    private InputSysAction inputAction;
    bool isCollided = false;
    private GameObject currentDragIcon;
    public GameObject instance;
    private IconDragging unitIcon;
    public int queuePlace;
    public bool isEnemy;
    private void Start()
    {
        inputAction = new InputSysAction();
        inputAction.UnitControl.Enable();
        inputAction.UnitControl.Touch.canceled += TouchingRelease;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isEnemy)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0.5f);
            Debug.Log("DetectedPlacing");
            isCollided = true;
            if (currentDragIcon == null)
            {
                currentDragIcon = collision.gameObject;
                unitIcon = currentDragIcon.GetComponent<DragUnit>().unitIcon;

            }
        }
        
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(!isEnemy)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 0f);
            isCollided = false;
        }
        
    }
    void TouchingRelease(InputAction.CallbackContext context)
    {
        if(currentDragIcon != null && isCollided && !isEnemy && instance == null)
        {
            instance = Instantiate(currentDragIcon.GetComponent<DragUnit>().unitPrefab, transform.parent);
            instance.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            unitIcon.UpdateCounter(-1);
            GameObject.Find("QueueMaker").GetComponent<QueueMaker>().queuedUnits[queuePlace] = instance.GetComponent<Unit>();

            
        }
        Destroy(currentDragIcon);

    }
    
    public void ClearPlace()
    {
        unitIcon.UpdateCounter(1);
        Destroy(instance);
        unitIcon = null;
        GameObject.Find("QueueMaker").GetComponent<QueueMaker>().queuedUnits[queuePlace] = null;


    }
    public void AssignUnit(GameObject unitPrefab)
    {
        instance = Instantiate(unitPrefab, transform.parent);
        instance.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        instance.GetComponent<Unit>().isEnemy = true;
        GameObject.Find("QueueMaker").GetComponent<QueueMaker>().enemyQueuedUnits[queuePlace] = instance.GetComponent<Unit>();
        instance.transform.localScale = new Vector3(-instance.transform.localScale.x, instance.transform.localScale.y, instance.transform.localScale.z);
    }
}
