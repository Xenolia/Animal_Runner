using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private Transform parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")|| other.CompareTag("Barrier"))
        {
            parent = other.transform.parent;
            parent.position = new Vector3(parent.position.x, parent.position.y, parent.position.z + 6f);
        }
    }
}
