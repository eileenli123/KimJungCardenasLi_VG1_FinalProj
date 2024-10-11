using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;

    void Update()
    {
        transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
    }
}
