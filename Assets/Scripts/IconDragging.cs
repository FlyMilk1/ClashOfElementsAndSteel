using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class IconDragging : MonoBehaviour
{
    private InputSysAction inputAction;
    public Sprite unitIcon;
    public bool isEnabled = true;
    public GameObject dragIcon;
    public GameObject unitPrefab;
    public int Count;
    private TMP_Text Counter;
    private Vector2 fingerPos;
    private GameObject instance;
    GraphicRaycaster raycaster;
    private GameObject canvas;
    private Image image;
    private RectTransform rectTransform;
    PointerEventData data;
    List<RaycastResult> results;
    
    void Start()
    {
        inputAction = new InputSysAction();
        inputAction.UnitControl.Enable();
        inputAction.UnitControl.Touch.canceled += TouchingRelease;
        inputAction.UnitControl.Drag.performed += Drag;
        canvas = GameObject.Find("Canvas");
        raycaster = canvas.GetComponent<GraphicRaycaster>();
        data = new PointerEventData(EventSystem.current);
        results = new List<RaycastResult>();
        image = gameObject.GetComponent<Image>();
        rectTransform = image.rectTransform;

        Counter = GetComponentInChildren<TMP_Text>();
        UpdateCounter(0);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (instance != null)
        {
            instance.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(fingerPos).x, Camera.main.ScreenToWorldPoint(fingerPos).y, 0);
            instance.GetComponent<Collider2D>().enabled = true;

            instance.layer = 7;

        }
    }
    private void Drag(InputAction.CallbackContext context)
    {
        fingerPos = context.ReadValue<Vector2>();

        if (instance == null)
        {


            bool isInRect = RectTransformUtility.RectangleContainsScreenPoint(rectTransform, fingerPos);
            //Debug.Log("Seeking:" + rectTransform.position.x + " " + rectTransform.position.y);
            //Debug.Log("Actual:" + fingerPos + isInRect);
            if (isInRect && Count>0)
            {
                //Debug.Log("Instantiating icon");
                instance = Instantiate(dragIcon, new Vector3(fingerPos.x, fingerPos.y, 0f), Quaternion.identity);
                instance.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<Image>().sprite;
                instance.GetComponent<DragUnit>().unitIcon = this;
                instance.GetComponent<DragUnit>().unitPrefab = unitPrefab;
                instance.transform.SetParent(transform.parent.parent);

            }
        }
        else
        {
            Camera.main.gameObject.GetComponent<Swipe>().enabled = false;
            Camera.main.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

    }
    //public void Pressed()
    //{
    //    instance = Instantiate(gameObject, new Vector3(fingerPos.x, fingerPos.y, 0f), Quaternion.identity);
    //    instance.GetComponent<Image>().color = new Color(instance.GetComponent<Image>().color.r, instance.GetComponent<Image>().color.g, instance.GetComponent<Image>().color.b, instance.GetComponent<Image>().color.a);
    //    instance.transform.SetParent(transform.parent.parent);
    //    instance.GetComponent<RectTransform>().sizeDelta = new Vector2(128, 128);
    //}
    //private void Touching(InputAction.CallbackContext context)
    void TouchingRelease(InputAction.CallbackContext context)
    {
        if(instance != null)
        {
            instance.GetComponent<SpriteRenderer>().enabled = false;
        }
        Camera.main.gameObject.GetComponent<Swipe>().enabled = true;

    }
    public void UpdateCounter(int change)
    {
        Count += change;
        Counter.text = "x" + Count.ToString();
        if(Count <= 1)
        {
            Counter.text = "";
        }
        if(Count <= 0)
        {
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
        }
        else
        {
            GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }
    }

}
