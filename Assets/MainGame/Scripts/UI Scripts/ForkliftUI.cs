using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForkliftUI : BasePopup
{
    public Button speedBtn;
    public Button capcityBtn;
    public Button collectSpeedBtn;
    public Button openPopupBtn;
    public Button closePopupBtn;

    [SerializeField]
    Text capacityLeveltxt;

    [SerializeField]
    Text speedLevelTxt;

    [SerializeField]
    Text collectSpeedLevelTxt;

    public static Action updateForkliftUI;

    [SerializeField]
    Slider speedSlider;

    [SerializeField]
    Slider capacitySlider;

    [SerializeField]
    Slider collectSpeedSlider;

    [SerializeField]
    Text sliderTxtSpeed;

    [SerializeField]
    Text sliderTxtCapacity;

    [SerializeField]
    Text sliderTxtCollectSpeed;

    private void Start()
    {
        speedBtn.onClick.RemoveAllListeners();
        speedBtn.onClick.AddListener(() => OnClickUpgradeSpeed());

        capcityBtn.onClick.RemoveAllListeners();
        capcityBtn.onClick.AddListener(() => OnClickUpgradeCapacity());

        collectSpeedBtn.onClick.RemoveAllListeners();
        collectSpeedBtn.onClick.AddListener(() => OnClickUpgradeCollectSpeed());

        RefreshUI();
    }

    private void OnEnable()
    {
        updateForkliftUI += RefreshUI;
    }

    private void OnDisable()
    {
        updateForkliftUI -= RefreshUI;
    }
    private void RefreshUI()
    {
        UpdateLevelText();
        UpdateButtonStats();
        UpdateUpgradeMeter();
    }

    private void UpdateUpgradeMeter()
    {
        speedSlider.minValue = 0;
        speedSlider.maxValue = ForkliftStats.instance.numberOfSteps + 1;

        capacitySlider.minValue = 0;
        capacitySlider.maxValue = ForkliftStats.instance.numberOfSteps + 1;

        collectSpeedSlider.minValue = 0;
        collectSpeedSlider.maxValue = ForkliftStats.instance.numberOfSteps + 1;

        speedSlider.value = ForkliftStats.instance.currentStepforkliftSpeed;
        capacitySlider.value = ForkliftStats.instance.currentStepforkliftCapacity;
        collectSpeedSlider.value = ForkliftStats.instance.currentStepForkliftCollectSpeed;

        sliderTxtCapacity.text = ForkliftStats.instance.currentStepforkliftCapacity.ToString() + "/" + (ForkliftStats.instance.numberOfSteps + 1).ToString();
        sliderTxtSpeed.text = ForkliftStats.instance.currentStepforkliftSpeed.ToString() + "/" + (ForkliftStats.instance.numberOfSteps + 1).ToString();
        sliderTxtCollectSpeed.text = ForkliftStats.instance.currentStepForkliftCollectSpeed.ToString() + "/" + (ForkliftStats.instance.numberOfSteps + 1).ToString();
    }

    private void UpdateButtonStats()
    {
        if (ForkliftStats.instance.speedDone)
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

        if (ForkliftStats.instance.capacityDone)
        {
            capcityBtn.gameObject.GetComponentInChildren<Text>().text = "Complete";
            capcityBtn.interactable = false;
            CheakMaxCase();
        }

        else
        {
            capcityBtn.gameObject.GetComponentInChildren<Text>().text = "Upgrade";
            capcityBtn.interactable = true;
        }

        if (ForkliftStats.instance.collectSpeedDone)
        {
            collectSpeedBtn.gameObject.GetComponentInChildren<Text>().text = "Complete";
            collectSpeedBtn.interactable = false;
            CheakMaxCase();
        }

        else
        {
            collectSpeedBtn.gameObject.GetComponentInChildren<Text>().text = "Upgrade";
            collectSpeedBtn.interactable = true;
        }
    }

    private void CheakMaxCase()
    {
        if (ForkliftStats.instance.speedDone && ForkliftStats.instance.capacityDone)
        {
            if (ForkliftStats.instance.currentForkliftLevel == ForkliftDataManager.instance.forklifts.Length - 1)
            {
                capacityLeveltxt.text = "Max";
                speedLevelTxt.text = "Max";
                collectSpeedLevelTxt.text = "Max";


                capacityLeveltxt.fontSize = 40;
                speedLevelTxt.fontSize = 40;
                collectSpeedLevelTxt.fontSize = 40;
            }

        }
    }

    private void UpdateLevelText()
    {
        if (ForkliftStats.instance.capacityDone)
        {
            capacityLeveltxt.text = (ForkliftStats.instance.currentForkliftLevel + 1).ToString();
        }

        else
        {
            capacityLeveltxt.text = ForkliftStats.instance.currentForkliftLevel.ToString();
        }

        if (ForkliftStats.instance.speedDone)
        {
            speedLevelTxt.text = (ForkliftStats.instance.currentForkliftLevel + 1).ToString();
        }

        else
        {
            speedLevelTxt.text = ForkliftStats.instance.currentForkliftLevel.ToString();
        }

        if(ForkliftStats.instance.collectSpeedDone)
        {
            collectSpeedLevelTxt.text = (ForkliftStats.instance.currentForkliftLevel + 1).ToString();
        }

        else
        {
            collectSpeedLevelTxt.text = ForkliftStats.instance.currentForkliftLevel.ToString();
        }
    }

    private void OnClickUpgradeCollectSpeed()
    {
        ForkliftStats.instance.UpgradeCollectSpeed();
        RefreshUI();
    }

    private void OnClickUpgradeCapacity()
    {
        ForkliftStats.instance.UpgradeCapacity();
        RefreshUI();
    }

    private void OnClickUpgradeSpeed()
    {
        ForkliftStats.instance.UpgradeSpeed();
        RefreshUI();
    }
}
