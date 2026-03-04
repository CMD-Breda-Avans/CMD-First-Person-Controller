/* * PLAYER MOVE
 * Rotates the Player object (horizontally) and the camera (vertically)
 * according to mouse input
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public float lookSpeed = 3.0f;
    private Vector2 rotation = Vector2.zero;

    void Start()
    {
      Cursor.visible = false;
    }

    void Update()
    {
      if (Mouse.current == null) return;

      Cursor.lockState = CursorLockMode.Locked;

      Vector2 mouseDelta = Mouse.current.delta.ReadValue();

      rotation.y += mouseDelta.x * 0.1f;
      rotation.x += -mouseDelta.y * 0.1f;

      rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
      transform.eulerAngles = new Vector2(0,rotation.y) * lookSpeed;
      Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
    }
}