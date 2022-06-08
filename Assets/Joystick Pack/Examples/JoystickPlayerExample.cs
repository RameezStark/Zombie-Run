using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    [SerializeField]
    float multiplier = 1f;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        //rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        gameObject.transform.position = (new Vector3(gameObject.transform.position.x + variableJoystick.Vertical, 0, gameObject.transform.position.z + variableJoystick.Horizontal)) * multiplier;
    }
}