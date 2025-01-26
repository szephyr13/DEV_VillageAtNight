using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolState : State<EnemyController>
{
    [SerializeField] private Transform route;
    [SerializeField] private float patrolSpeed;
    private List<Vector3> routeMarks = new List<Vector3>();
    private Vector3 currentDestination;
    private int destinationIndex;

    //when entering the state, gets the patrol points and lists them
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        foreach (Transform point in route)
        {
            routeMarks.Add(point.position);
        }
        currentDestination = routeMarks[destinationIndex];
    }

    //moves to the patrol points. when reaching it, changes the patrol point
    public override void OnUpdateState()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentDestination,
            patrolSpeed * Time.deltaTime);

        if (transform.position == currentDestination)
        {
            SetNewDestination();
        }

        FocusOnDestination();
    }

    //looks at the destination taking only as reference the x axis. 
    private void FocusOnDestination()
    {
        if (currentDestination.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    //changes the index destination
    private void SetNewDestination()
    {
        destinationIndex++;

        if (destinationIndex > routeMarks.Count - 1)
        {
            destinationIndex = 0;
        }

        currentDestination = routeMarks[destinationIndex];

    }

    //when exiting the state, resets all route points logic
    public override void OnExitState()
    {
        routeMarks.Clear();
        destinationIndex = 0;

    }

    //when detecting the player, changes to chase state
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.TryGetComponent(out Player player))
        {
            Debug.Log(player.name);
            controller.ChangeState(controller.ChaseState);
        }*/
        if (collision.CompareTag("PlayerDetection"))
        {
            controller.ChangeState(controller.ChaseState);
        }

    }

}
