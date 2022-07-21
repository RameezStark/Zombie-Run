using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUpgrader : MonoBehaviour
{
    [SerializeField]
    CapsuleCollider collector;
    public void increaseSpeed()
    {
        ForkliftStats.instance.speed += 10;
    }

    public void increaseCapacity()
    {
        ForkliftStats.instance.capacity += 10;
    }

    public void increaseRadius()
    {
        ForkliftStats.instance.collectSpeed += 0.5f;
        collector.radius = ForkliftStats.instance.collectSpeed;

    }
}
