using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float patrolSpeed;
    private Vector3 currentDestination;
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = movementPoints[currentIndex].position;
        StartCoroutine(PatrolMovement());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator PatrolMovement()
    {
        while (true)
        {
            while (transform.position != currentDestination)
            {
                transform.position = Vector3.MoveTowards(
                transform.position,
                currentDestination,
                patrolSpeed * Time.deltaTime);
                yield return null;
            }
            defineDestination();

        }
    }

    private void defineDestination()
    {
        currentIndex++;
        if(currentIndex >= movementPoints.Length)
        {
            currentIndex = 0;
        }
        currentDestination = movementPoints[currentIndex].position;
        FocusOnDestination();
    }

    private void FocusOnDestination()
    {
        if(currentDestination.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
