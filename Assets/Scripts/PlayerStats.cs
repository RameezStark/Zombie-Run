using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    private void Awake()
    {
        instance = this;
    }


    public float playerSpeed;


    public float playerCollectCollider;


    public float playerItemMaxCapacity;
}
