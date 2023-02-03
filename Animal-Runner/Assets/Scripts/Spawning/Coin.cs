using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    int random;
    private void OnEnable()
    {
        Invoke("CheckPlayersPosition", 5.5f);
    }

    private void RotateTheCoin()
    {
        transform.Rotate(250*Time.deltaTime, 0, 0);
    }

    private void Update()
    {
        RotateTheCoin();
    }

    private void CheckPlayersPosition()
    {
        if (transform.position.z < playerTransform.position.z)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            random = Random.Range(0, 2);

            if (random == 0)
            {
                transform.position = new Vector3(other.transform.position.x+0.55f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(other.transform.position.x + -0.55f, transform.position.y, transform.position.z);
            }
        }
        else if (other.CompareTag("Truck"))
        {
            random = Random.Range(0, 2);

            if (random == 0)
            {
                transform.position = new Vector3(other.transform.position.x + 0.65f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(other.transform.position.x + -0.65f, transform.position.y, transform.position.z);
            }
        }
        else if (other.CompareTag("Deathzone"))
        {
            random = Random.Range(0, 2);

            if (random == 0)
            {
                transform.localPosition = new Vector3(other.transform.position.x + 0.55f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.localPosition = new Vector3(other.transform.position.x + -0.55f, transform.position.y, transform.position.z);
            }
        }
        else if (other.CompareTag("Barrier"))
        {
            random = Random.Range(0, 2);

            if (random == 0)
            {
                transform.localPosition = new Vector3(transform.position.x , transform.position.y, other.transform.position.z + 1f);
            }
            else
            {
                transform.localPosition = new Vector3(transform.position.x , transform.position.y, other.transform.position.z+1f);
            }
        }

        if (other.CompareTag("Left"))
        {
            float xPos = Random.Range(-0.65f, 0.65f);
            transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
        }

        else if (other.CompareTag("Right"))
        {
            float xPos = Random.Range(-0.65f, 0.65f);
            transform.localPosition = new Vector3(xPos, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
