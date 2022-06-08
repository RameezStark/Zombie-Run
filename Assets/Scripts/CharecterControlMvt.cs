using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterControlMvt : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    //private float jumpHeight = 1.0f;
    //private float gravityValue = -9.81f;
    public VariableJoystick variableJoystick;
    public Animator playerGraphics;

    

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(variableJoystick.Horizontal, 0, variableJoystick.Vertical);
        controller.Move(move * Time.fixedDeltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            playerGraphics.SetBool("IsRunning", true);
            gameObject.transform.forward = move;
        }

        else
        {
            playerGraphics.SetBool("IsRunning", false);
        }
    }

    
}
