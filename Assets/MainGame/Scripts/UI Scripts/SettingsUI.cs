using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : BasePopup
{
    [SerializeField]
    Button closeBtn;

    [SerializeField]
    Button openBtn;

    private void Awake()
    {
       
    }


    private void Start()
    {
        Close();

        closeBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.AddListener(() => { Close(); });

        openBtn.onClick.RemoveAllListeners();
        openBtn.onClick.AddListener(() => { Open(); });
    }
}
