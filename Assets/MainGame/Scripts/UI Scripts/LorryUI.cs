using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LorryUI : BasePopup
{
    public Button capacityBtn;
    public Button speedBtn;

    [SerializeField]
    Text capacityLevelTxt;

    [SerializeField]
    Text speedLeveltxt;

    public static Action updateLorryUI;
   

    [SerializeField]
    Slider speedSlider;

    [SerializeField]
    Slider capacitySlider;

    [SerializeField]
    Text sliderTxtSpeed;

    [SerializeField]
    Text sliderTxtCapacity;

    private void Awake()
    {

        capacityBtn.onClick.RemoveAllListeners();
        capacityBtn.onClick.AddListener(() => OnClickUpgradeCapacity());

        speedBtn.onClick.RemoveAllListeners();
        speedBtn.onClick.AddListener(() => OnClickUpgradeSpeed());

        

    }

    private void Start()
    {
        RefreshUI();
    }

    private void OnEnable()
    {
        updateLorryUI += RefreshUI;
        
    }

    private void OnDisable()
    {
        updateLorryUI -= RefreshUI;
    }

    public void OnClickUpgradeCapacity()
    {
        LorryStats.instance.UpgradeCapacity();
        RefreshUI();

    }

    public void OnClickUpgradeSpeed()
    {
        LorryStats.instance.UpgradeSpeed();
        RefreshUI();
    }

    public void RefreshUI()
    {
        UpdateLevelText();
        UpdateButtonStats();
        UpdateUpgradeMeter();
    }

    public void UpdateButtonStats()
    {
        if(LorryStats.instance.speedDone)
        {
            speedBtn.gameObject.GetComponentInChildren<Text>().text = "Complete";
            speedBtn.interactable = false;
            CheakMaxCase();
        }

        else
        {
            speedBtn.gameObject.GetComponentInChildren<Text>().text = "Upgrade";
            speedBtn.interactable = true;
        }

        if(LorryStats.instance.capacityDone)
        {
            capacityBtn.gameObject.GetComponentInChildren<Text>().text = "Complete";
            capacityBtn.interactable = false;
            CheakMaxCase();
        }

        else
        {
            capacityBtn.gameObject.GetComponentInChildren<Text>().text = "Upgrade";
            capacityBtn.interactable = true;
        }
    }


    public void UpdateLevelText()
    {
        if (LorryStats.instance.capacityDone)
        {
            capacityLevelTxt.text = (LorryStats.instance.currentLorryLevel + 1).ToString();        
        }

        else
        {
            capacityLevelTxt.text = LorryStats.instance.currentLorryLevel .ToString();
        }

        if (LorryStats.instance.speedDone)
        {
            speedLeveltxt.text = (LorryStats.instance.currentLorryLevel + 1).ToString();
        }

        else
        {
            speedLeveltxt.text = LorryStats.instance.currentLorryLevel .ToString();
        }
    }

    public void UpdateUpgradeMeter()
    {
        speedSlider.minValue = 0;
        speedSlider.maxValue = LorryStats.instance.numberOfSteps + 1;

        capacitySlider.minValue = 0;
        capacitySlider.maxValue = LorryStats.instance.numberOfSteps + 1;

        speedSlider.value = LorryStats.instance.currentStepSpeed;
        capacitySlider.value = LorryStats.instance.currentStepCapacity;

        sliderTxtCapacity.text = LorryStats.instance.currentStepCapacity.ToString() + "/" + (LorryStats.instance.numberOfSteps + 1).ToString();
        sliderTxtSpeed.text = LorryStats.instance.currentStepSpeed.ToString() + "/" + (LorryStats.instance.numberOfSteps + 1).ToString();
    }

    public void CheakMaxCase()
    {
        if (LorryStats.instance.speedDone && LorryStats.instance.capacityDone)
        {
            if (LorryStats.instance.currentLorryLevel == LorryDataManager.instance.lorryData.Length - 1)
            {
                capacityLevelTxt.text = "Max";
                speedLeveltxt.text = "Max";
                capacityLevelTxt.fontSize = 40;
                speedLeveltxt.fontSize = 40;
            }
            
        }

       
    }
}
