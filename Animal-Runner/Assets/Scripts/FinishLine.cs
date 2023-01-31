using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")|| other.CompareTag("Truck"))
        {
            other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + 6f);
        }
    }
}
