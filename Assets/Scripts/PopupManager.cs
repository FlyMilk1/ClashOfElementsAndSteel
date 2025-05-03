using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public void Open(GameObject popup)
    {
        popup.SetActive(true);
    }
    public void Close(GameObject popup)
    {
        popup.SetActive(false);
    }
}
