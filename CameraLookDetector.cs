/* * CAMERA LOOK DETECTOR
 * Detects what the camera is looking at
 * If it is looking at something that has a collider, it will attempt 
 * to tell the MouseCursor script to change its cursor
 * If the Interact key is pressed while looking at an object
 * it will attempt to run the 'Interact Function' of the targets' Object Interaction script
 */

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CameraLookDetector : MonoBehaviour
{
    Camera cam;
    public MouseCursors mouseCursors;

    public Key interactKey = Key.E;

    public float maxInteractionDistance = 2.0f;

    ObjectInteraction currentLookTarget;

    void Start()
    {
        cam = GetComponent<Camera>();
        currentLookTarget = null;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxInteractionDistance))
          LookAtInteractible(hit.transform.GetComponent<ObjectInteraction>() ?? null);
        else
          LookAtInteractible(null);

        if (Keyboard.current[interactKey].wasPressedThisFrame && currentLookTarget != null)
        {
          currentLookTarget.OnInteract();
        }
    }

    void LookAtInteractible(ObjectInteraction obj)
    {
      currentLookTarget = obj;
      if (obj != null && obj.cursor != null)
        mouseCursors.SetCursor(obj.cursor);
      else 
        mouseCursors.SetCursor();
    }
}