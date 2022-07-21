using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public void Open()
    {
        GetComponent<Canvas>().enabled = true;
    }

    public void Close()
    {
        GetComponent<Canvas>().enabled = false;
    }
}
