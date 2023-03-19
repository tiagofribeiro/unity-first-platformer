using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour player;

    private void Update()
    {
        if (player.GetComponent<PlayerMovement>().CanMove)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
