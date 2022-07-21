using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterControlMvt : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
   
    public VariableJoystick variableJoystick;
    public Animator playerGraphics;

    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical);
        controller.Move(move * Time.fixedDeltaTime * ForkliftStats.instance.speed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        else
        {
            
        }
    }

    
}
