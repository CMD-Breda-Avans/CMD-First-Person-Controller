/* * PLAYER MOVE
 * Moves the Player object according to key inputs.
 * Crouching and jumping are optional
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public float walkingSpeed = 3.0f;

    public bool jumpEnabled;

    public float jumpSpeed = 1.0f;
    public bool crouchEnabled;
    public float crouchHeight = 0.4f;
    private float normalHeight;
    
    public Key forwardKey = Key.W;
    public Key backKey = Key.S;
    public Key leftKey = Key.A;
    public Key rightKey = Key.D;
    public Key jumpKey = Key.Space;
    public Key crouchKey = Key.LeftCtrl;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalHeight = transform.localScale.y;
    }

    void FixedUpdate()
    {
        if (Keyboard.current == null) return;

        Vector3 movement = new Vector3();
        bool hasInput = false;

        // Walking
        if (Keyboard.current[forwardKey].isPressed)
        {
            movement += transform.forward * walkingSpeed;
            hasInput = true;
        }

        if (Keyboard.current[backKey].isPressed)
        {
            movement += -transform.forward * walkingSpeed;
            hasInput = true;
        }

        if (Keyboard.current[rightKey].isPressed)
        {
            movement += transform.right * walkingSpeed;
            hasInput = true;
        }

        if (Keyboard.current[leftKey].isPressed)
        {
            movement += -transform.right * walkingSpeed;
            hasInput = true;
        }

        // Jumping
        if (jumpEnabled && Keyboard.current[jumpKey].isPressed && isGrounded()) 
        {
            movement += transform.up * jumpSpeed;
        }

        // make sure the rigidbody isn't sliding around when there's no input
        if (!hasInput) {
          rb.constraints = 
            RigidbodyConstraints.FreezePositionX | 
            RigidbodyConstraints.FreezePositionZ |
            RigidbodyConstraints.FreezeRotationY | 
            RigidbodyConstraints.FreezeRotationZ;
        } else {
          rb.constraints = 
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;
        }

        // maintain vertical speed
        movement.y += rb.linearVelocity.y;

        // apply movement to rigidbody
        rb.linearVelocity = movement ;
    }

    void Update() {
        if (Keyboard.current == null) return;

        if (crouchEnabled && Keyboard.current[crouchKey].wasPressedThisFrame) {
          // Crouching
          transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);
        } else if (crouchEnabled && Keyboard.current[crouchKey].wasReleasedThisFrame) {
          // Not crouching
          transform.localScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);
        }
    }

    // Check if player is on the ground
    bool isGrounded() {
      return Physics.Raycast(transform.position, -Vector3.up, 0.1f + transform.localScale.y);
    }
}