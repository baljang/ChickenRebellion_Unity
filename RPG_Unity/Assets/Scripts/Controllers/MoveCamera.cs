using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float edgeBorder = 50.0f; // This is the pixel border from the edge of the screen.
    public float moveSpeed = 10.0f; // The speed at which the camera should move.

    void Update()
    {
        Vector3 pos = transform.position;

        // Check left edge
        if (Input.mousePosition.x < edgeBorder)
            pos.x -= moveSpeed * Time.deltaTime;

        // Check right edge
        if (Input.mousePosition.x > Screen.width - edgeBorder)
            pos.x += moveSpeed * Time.deltaTime;

        // Check bottom edge
        if (Input.mousePosition.y < edgeBorder)
            pos.z -= moveSpeed * Time.deltaTime; // Z axis because this is a 3D game.

        // Check top edge
        if (Input.mousePosition.y > Screen.height - edgeBorder)
            pos.z += moveSpeed * Time.deltaTime; // Z axis because this is a 3D game.

        // Update the camera's position
        transform.position = pos;
    }
}
