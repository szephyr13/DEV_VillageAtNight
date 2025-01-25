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

    private void Start()
    {
        orderOpenDoor = false;
    }


    public void Interact()
    {
        orderOpenDoor = true;
        if (player.CanOpenDoor && orderOpenDoor == true)
        {
            AudioManager.instance.PlaySFX("OpenDoor");
            StartCoroutine(OpenDoor());
        }
    }


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
