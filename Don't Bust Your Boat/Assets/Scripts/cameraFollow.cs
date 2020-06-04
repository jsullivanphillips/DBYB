using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 1.5f;
    public Vector3 offset;
    public float lookAtOffset = 0;

    void LateUpdate()
    {
        
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;

    }

  
}
