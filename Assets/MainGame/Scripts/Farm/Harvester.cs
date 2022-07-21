using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvester : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pumpkin>() != null)
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.GetComponent<Pumpkin>() != null)
        {
            Destroy(collision.other.gameObject);
        }
    }
}
