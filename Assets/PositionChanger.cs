using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChanger : MonoBehaviour
{
    private bool isPosition1 = true; // Flag to track current position

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TogglePosition();
        }
    }

    void TogglePosition()
    {
        Transform objectTransform = transform; // Assuming the script is attached to the same GameObject

        if (isPosition1)
        {
            // Change the local position to (1, 1.8, -4) relative to the current position
            objectTransform.localPosition = new Vector3(1f, 1.8f, -4f);
        }
        else
        {
            // Change the local position to (0, 1, 0) relative to the current position
            objectTransform.localPosition = new Vector3(0f, 1f, 0f);
        }

        // Toggle the flag for the next press
        isPosition1 = !isPosition1;
    }
}