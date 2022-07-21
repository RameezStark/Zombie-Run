using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PumpkinUI : BasePopup
{
    public Button upgradePumpkinBtn;

    public Button openPopupBtn;
    public Button closePopupBtn;

    [SerializeField]
    GameObject[] pumpkinThumpnails;

    [SerializeField]
    GameObject currentPumpkinImg;

    [SerializeField]
    Text currentPumpkinName;

    [SerializeField]
    Text currentPumpkinDescription;

    [SerializeField]
    Image currentPumkinCostImg;

    [SerializeField]
    Image nextPumpkinCostImg;

    [SerializeField]
    Text currentPumpkinCost;

    [SerializeField]
    Text nextPumpkinCost;

    [SerializeField]
    Button prevBtn;

    [SerializeField]
    Button nextBtn;

    [SerializeField]
    Button upgradeBtn;

    [SerializeField]
    Text upgradeBtnTxt;

    int currentPumpkinCount;

    int currentPumpkinOnDescriptionCount;

    public static Action PumpkinUpgradeUI;

    public static Action<int> packageDeilvered;

    [SerializeField]
    Slider packageSlider;

    int currentPackagesDelivered = 0;

    private void Start()
    {
        Close();
        currentPumpkinCount = PumpkinManager.instance.currentPumpkinCount;
        currentPumpkinOnDescriptionCount = currentPumpkinCount;
        SetupUI();

        openPopupBtn.onClick.RemoveAllListeners();
        openPopupBtn.onClick.AddListener(() => { Open(); });

        closePopupBtn.onClick.RemoveAllListeners();
        closePopupBtn.onClick.AddListener(() => { Close(); });

        prevBtn.onClick.RemoveAllListeners();
        prevBtn.onClick.AddListener(() => OnClickPrev());

        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() => OnClickNext());

        upgradeBtn.onClick.RemoveAllListeners();
        upgradeBtn.onClick.AddListener(() => OnClickUpgrade());
   
    }

    private void OnEnable()
    {
        PumpkinUpgradeUI += UpgradePumpkin;
        packageDeilvered += AddPackagesDelivered;
    }

    private void OnDisable()
    {
        PumpkinUpgradeUI -= UpgradePumpkin;
        packageDeilvered -= AddPackagesDelivered;
    }

    public void AddPackagesDelivered(int amount)
    {
        currentPackagesDelivered += amount; 

        SetUpCurrentPumpkinDetails();
    }

    public void SetupUI()
    {
        int i = 0;
        foreach(var thumpnail in pumpkinThumpnails)
        {
            thumpnail.GetComponent<Image>().sprite = PumpkinManager.instance.pumpkins[i].pumpkinImg;
            if(i< PumpkinManager.instance.pumpkins.Count - 1)
            {
                i++;
            }
        }

        SetUpCurrentPumpkinDetails();

    }

    public void OnClickUpgrade()
    {
        FarmManager.instance.ResetAllPumpkins();
        currentPackagesDelivered = 0;
    }

    public void SetUpCurrentPumpkinDetails()
    {
        currentPumpkinImg.GetComponent<Image>().sprite = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount].pumpkinImg;
        currentPumpkinName.text = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount].name;
        currentPumpkinDescription.text = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount].description;
        currentPumkinCostImg.sprite = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount].pumpkinImg;
        currentPumpkinCost.text = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount].price.ToString();
        

        if(currentPumpkinOnDescriptionCount + 1 <= PumpkinManager.instance.pumpkins.Count - 1)
        {
            nextPumpkinCostImg.sprite = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount + 1].pumpkinImg;
            nextPumpkinCost.text = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount + 1].price.ToString();
        }

        else
        {
            nextPumpkinCostImg.sprite = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount].pumpkinImg;
            nextPumpkinCost.text = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount].price.ToString();
        }
        ManageUpgradeBtn();
    }

    public void OnClickNext()
    {
        if(PumpkinManager.instance.pumpkins.Count - 1 > currentPumpkinOnDescriptionCount)
        {
            currentPumpkinOnDescriptionCount++;
            SetUpCurrentPumpkinDetails();
        }

        else
        {
            currentPumpkinOnDescriptionCount = 0;
            SetUpCurrentPumpkinDetails();

        }

        
    }

    public void OnClickPrev()
    {
        if (0 < currentPumpkinOnDescriptionCount)
        {
            currentPumpkinOnDescriptionCount--;
            SetUpCurrentPumpkinDetails();
        }
        else
        {
            currentPumpkinOnDescriptionCount = PumpkinManager.instance.pumpkins.Count-1;
            SetUpCurrentPumpkinDetails();

        }

        
    }

    public void UpgradePumpkin()
    {
        currentPumpkinCount = PumpkinManager.instance.currentPumpkinCount;

        SetUpCurrentPumpkinDetails();
    }

    public void PackageDeliveredSliderUpdate()
    {
        packageSlider.maxValue = PumpkinManager.instance.pumpkins[currentPumpkinOnDescriptionCount].packagesReq;
        packageSlider.minValue = 0;

        if (currentPumpkinOnDescriptionCount == currentPumpkinCount + 1)
        {
            packageSlider.value = currentPackagesDelivered;

            if(packageSlider.value == packageSlider.maxValue)
            {
                upgradeBtn.interactable = true;
            }
        }

        else if (currentPumpkinOnDescriptionCount < currentPumpkinCount + 1)
        {
            packageSlider.value = packageSlider.maxValue;
            upgradeBtn.interactable = false;
        }

        else if (currentPumpkinOnDescriptionCount > currentPumpkinCount + 1)
        {
            packageSlider.value = packageSlider.minValue;
            upgradeBtn.interactable = false;
        }
    }

    public void ManageUpgradeBtn()
    {
        if(currentPumpkinOnDescriptionCount == currentPumpkinCount + 1)
        {
            upgradeBtn.interactable = false;
            upgradeBtnTxt.text = "Upgrade";
        }

        else if(currentPumpkinOnDescriptionCount < currentPumpkinCount + 1)
        {
            upgradeBtn.interactable = false;
            upgradeBtnTxt.text = "Unlocked";
        }

        else if(currentPumpkinOnDescriptionCount > currentPumpkinCount + 1)
        {
            upgradeBtn.interactable = false;
            upgradeBtnTxt.text = "Locked";
        }

        PackageDeliveredSliderUpdate();
    }


}
