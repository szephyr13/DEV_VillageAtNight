using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractuable
{
    [SerializeField] private Transform closedPosition;
    [SerializeField] private Transform openPosition;
    [SerializeField] private float speed;
    [SerializeField] private Player player;
    private bool orderOpenDoor;

    //door starts closed
    private void Start()
    {
        orderOpenDoor = false;
    }


    //when interacting, only if player can open doors, starts opening coroutine
    public void Interact()
    {
        orderOpenDoor = true;
        if (player.CanOpenDoor && orderOpenDoor == true)
        {
            AudioManager.instance.PlaySFX("OpenDoor");
            StartCoroutine(OpenDoor());
        }
    }

    //opening coroutine gets the door to the open point
    IEnumerator OpenDoor()
    {
        while (transform.position != openPosition.position)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition.position,
                speed * Time.deltaTime);
            yield return null;
        }
    }

}
