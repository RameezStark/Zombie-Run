using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{

    GameObject BGblur;

    private void Awake()
    {
        BGblur = GameObject.FindGameObjectWithTag("BGblur");        
    }

    public void Open()
    {
        PopupManager.instance.CloseAllPopups();
        AudioManager.Play("Click");
        //BGblur.SetActive(true);
        GetComponent<Canvas>().enabled = true;
    }

    public void Close()
    {
        AudioManager.Play("Click");
        //BGblur.SetActive(false);
        GetComponent<Canvas>().enabled = false;
    }
}
