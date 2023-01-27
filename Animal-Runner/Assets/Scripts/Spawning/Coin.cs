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
        //StartCoroutine(RotateTheCoin());
    }

   /* private void OnDisable()
    {
        StopCoroutine(RotateTheCoin());
    }*/

    private void RotateTheCoin()
    {
        transform.Rotate(3, 0, 0);
    }

    /*IEnumerator RotateTheCoin()
    {
        while (true)
        {
            transform.Rotate(3, 0, 0);
            yield return new WaitForSeconds(0f);
        }
    }*/
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
                transform.position = new Vector3(other.transform.position.x+0.35f, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(other.transform.position.x + -0.35f, transform.position.y, transform.position.z);
            }
        }

        if (other.CompareTag("Left"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x + 1.2f, transform.localPosition.y, transform.localPosition.z);
        }

        else if (other.CompareTag("Right"))
        {
            transform.localPosition = new Vector3(transform.localPosition.x - 1.2f, transform.localPosition.y, transform.localPosition.z);
        }
    }
}
