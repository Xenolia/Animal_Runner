using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseObstacle : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private void OnEnable()
    {
        Invoke("CheckPlayersPosition", 5.5f);
    }

    private void CheckPlayersPosition()
    {
        if (transform.position.z < playerTransform.position.z)
        {
            gameObject.SetActive(false);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 6f);
        }
    }*/
}
