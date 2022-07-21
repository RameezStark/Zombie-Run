using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuUI : BasePopup
{
    [SerializeField]
    Canvas[] upgradePanels;

    public void OpenPanel(int panelID)
    {
        CloseAllPanels();
        upgradePanels[panelID].enabled = true;
    }

    public void CloseAllPanels()
    {
        foreach(var panel in upgradePanels)
        {
            panel.enabled = false;
        }
    }
}
