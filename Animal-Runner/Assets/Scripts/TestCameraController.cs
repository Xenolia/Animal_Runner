using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    //ilk deðer 0.7f
    private float yOffset = 2.03f;
    private float zOffset = -1.5f;

    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x,player.position.y+yOffset,player.position.z+zOffset);
    }
}
