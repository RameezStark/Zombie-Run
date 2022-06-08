using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    

    Vector3 offset;
    private void Awake()
    {
        offset = gameObject.transform.position - player.transform.position;
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x + offset.x, gameObject.transform.position.y, player.transform.position.z + offset.z);
    }
}
