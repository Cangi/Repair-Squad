using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithObject : MonoBehaviour
{
    public Transform targetToFollow;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targetToFollow.position;
        transform.rotation = targetToFollow.rotation;
    }
}
