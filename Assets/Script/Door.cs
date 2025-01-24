using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractuable
{
    [SerializeField] private Transform closedPosition;
    [SerializeField] private Transform openPosition;
    [SerializeField] private float speed;
    [SerializeField] private Player player;




    public void Interact()
    {
        Debug.Log(player.CanOpenDoor);
        if (player.CanOpenDoor)
        {
            StartCoroutine(OpenDoor());
        }
    }

    private IEnumerator OpenDoor()
    {
        while (true)
        {
            while (transform.position != openPosition.position)
            {
                Debug.Log("Opening door");
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    openPosition.position,
                    speed * Time.deltaTime);
                yield return null;
            }
        }
    }

}
