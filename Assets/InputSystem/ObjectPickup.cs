using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private bool isHolding = false;
    private GameObject heldObject;
    private bool isMouseMovementEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHolding)
            {
                DropObject();
            }
            else
            {
                TryPickUpObject();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isMouseMovementEnabled = true;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            isMouseMovementEnabled = false;
        }

        if (isHolding)
        {
            UpdateHeldObjectPosition();
            UpdateObjectDistance();
        }
    }

    void TryPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Pickupable"))
            {
                PickUpObject(hit.collider.gameObject);
            }
        }
    }

    void PickUpObject(GameObject objToPickup)
    {
        isHolding = true;
        heldObject = objToPickup;
        heldObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    void DropObject()
    {
        isHolding = false;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;
        heldObject = null;
    }

    void UpdateHeldObjectPosition()
    {
        if (heldObject != null && isMouseMovementEnabled)
        {
            // Get the mouse movement
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Adjust the position of the held object based on the mouse movement
            Vector3 newPosition = heldObject.transform.position + new Vector3(mouseX, mouseY, 0f);
            heldObject.transform.position = newPosition;

            // Rotate the held object based on the player's rotation
            heldObject.transform.rotation = Quaternion.Euler(transform.eulerAngles);
        }
    }

    void UpdateObjectDistance()
    {
        if (heldObject != null)
        {
            // Get the mouse scroll wheel input
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

            // Adjust the distance of the held object based on the scroll wheel input
            float newDistance = Vector3.Distance(heldObject.transform.position, transform.position);
            newDistance += scrollWheel * 2f; // Adjust the multiplier as needed

            // Clamp the distance to prevent the object from getting too close or too far
            newDistance = Mathf.Clamp(newDistance, 1f, 10f);

            // Update the position of the held object
            heldObject.transform.position = transform.position + transform.forward * newDistance;
        }
    }
}