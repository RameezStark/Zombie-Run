using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUpgrader : MonoBehaviour
{
    [SerializeField]
    CapsuleCollider collector;
    public void increaseSpeed()
    {
        PlayerStats.instance.playerSpeed += 10;
    }

    public void increaseCapacity()
    {
        PlayerStats.instance.playerItemMaxCapacity += 10;
    }

    public void increaseRadius()
    {
        PlayerStats.instance.playerCollectCollider += 0.5f;
        collector.radius = PlayerStats.instance.playerCollectCollider;

    }
}
