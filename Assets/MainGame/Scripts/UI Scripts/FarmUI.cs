using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmUI : BasePopup
{
    public Button fertilizerUpgradeBtn;
    public Button harvesterUpgradeBtn;

    [SerializeField]
    Text harvesterLevelTxt;

    [SerializeField]
    Text fertilizerLevelTxt;

    public static Action updateFarmUI;

    [SerializeField]
    Slider harvesterSlider;

    [SerializeField]
    Slider fertilizerSlider;

    [SerializeField]
    Text sliderTxtFertilizer;

    [SerializeField]
    Text sliderTxtHarvester;

    private void Awake()
    {
        fertilizerUpgradeBtn.onClick.RemoveAllListeners();
        fertilizerUpgradeBtn.onClick.AddListener(() => OnClickUpgradeFertilizer());

        harvesterUpgradeBtn.onClick.RemoveAllListeners();
        harvesterUpgradeBtn.onClick.AddListener(() => OnClickUpgradeHarvester());
    }

    private void Start()
    {
        RefreshUI();
    }

    private void OnEnable()
    {
        updateFarmUI += RefreshUI;
    }

    private void OnDisable()
    {
        updateFarmUI -= RefreshUI;
    }

    

    public void OnClickUpgradeHarvester()
    {
        FarmStats.instance.UpgradeHarvester();
        RefreshUI();
    }

    public void OnClickUpgradeFertilizer()
    {
        FarmStats.instance.UpgradeFertilizer();
        RefreshUI();
    }

    public void RefreshUI()
    {
        UpdateLevelText();
        UpdateButtonStats();
        UpdateUpgradeMeter();
    }

    public void UpdateUpgradeMeter()
    {
        harvesterSlider.minValue = 0;
        harvesterSlider.maxValue = FarmStats.instance.numberOfSteps + 1;

        fertilizerSlider.minValue = 0;
        fertilizerSlider.maxValue = FarmStats.instance.numberOfSteps + 1;

        harvesterSlider.value = FarmStats.instance.currentStepHarvester;
        fertilizerSlider.value = FarmStats.instance.currentStepFertilizer;

        sliderTxtFertilizer.text = FarmStats.instance.currentStepFertilizer.ToString() + "/" + (FarmStats.instance.numberOfSteps + 1).ToString();
        sliderTxtHarvester.text = FarmStats.instance.currentStepHarvester.ToString() +  "/" +(FarmStats.instance.numberOfSteps + 1).ToString();
    }

    public void UpdateButtonStats()
    {
        if(FarmStats.instance.fertilizerDone)
        {
            fertilizerUpgradeBtn.gameObject.GetComponentInChildren<Text>().text = "Complete";
            fertilizerUpgradeBtn.interactable = false;
            CheakMaxCase();
        }

        else
        {
            fertilizerUpgradeBtn.gameObject.GetComponentInChildren<Text>().text = "Upgrade";
            fertilizerUpgradeBtn.interactable = true;
        }

        if (FarmStats.instance.harvesterDone)
        {
            harvesterUpgradeBtn.gameObject.GetComponentInChildren<Text>().text = "Complete";
            harvesterUpgradeBtn.interactable = false;
            CheakMaxCase();
        }

        else
        {
            harvesterUpgradeBtn.gameObject.GetComponentInChildren<Text>().text = "Upgrade";
            harvesterUpgradeBtn.interactable = true;
        }
    }

    private void CheakMaxCase()
    {
        if(FarmStats.instance.fertilizerDone && FarmStats.instance.harvesterDone)
        {
            if(FarmStats.instance.currentFarmLevel == FarmStats.instance.farmMaxLevel - 1)
            {
                harvesterLevelTxt.text = "Max";
                fertilizerLevelTxt.text = "Max";

                harvesterLevelTxt.fontSize = 40;
                fertilizerLevelTxt.fontSize = 40;
            }
        }
    }

    public void UpdateLevelText()
    {
        if(FarmStats.instance.fertilizerDone)
        {
            fertilizerLevelTxt.text = (FarmStats.instance.currentFarmLevel + 1).ToString();            
        }

        else
        {
            fertilizerLevelTxt.text = (FarmStats.instance.currentFarmLevel).ToString();
        }

        if(FarmStats.instance.harvesterDone)
        {
            harvesterLevelTxt.text = (FarmStats.instance.currentFarmLevel + 1).ToString();
        }

        else
        {
            harvesterLevelTxt.text = (FarmStats.instance.currentFarmLevel).ToString();
        }
    }
}
