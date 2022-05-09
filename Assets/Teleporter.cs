using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 destination;
    GameObject teleportTarget;

    private void OnTriggerEnter(Collider other)
    {
        teleportTarget = other.gameObject;
        teleportTarget.transform.position = destination;
    }
}
