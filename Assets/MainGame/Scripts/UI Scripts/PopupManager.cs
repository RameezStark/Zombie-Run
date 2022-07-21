using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public BasePopup[] popups;
    public static PopupManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void CloseAllPopups()
    {
        foreach(var popup in  popups)
        {
            popup.Close();            
        }
    }
}


