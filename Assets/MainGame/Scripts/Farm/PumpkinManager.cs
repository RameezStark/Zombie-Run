using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManager : MonoBehaviour
{
    public static PumpkinManager instance;

    public int currentPumpkinCount;

    
    private void Awake()
    {
        instance = this;
    }

    public List<pumpkin> pumpkins;


}

[System.Serializable]
public class pumpkin
{
    public string name;
    public int price;
    public GameObject pumkin;
    public Sprite pumpkinImg;
    public string description;
    public int packagesReq;
    public bool isUnlocked;
}
