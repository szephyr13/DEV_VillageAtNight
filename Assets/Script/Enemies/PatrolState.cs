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

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);

        foreach (Transform point in route)
        {
            routeMarks.Add(point.position);
        }
        currentDestination = routeMarks[destinationIndex];
    }

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

    private void SetNewDestination()
    {
        destinationIndex++;

        if (destinationIndex > routeMarks.Count - 1)
        {
            destinationIndex = 0;
        }

        currentDestination = routeMarks[destinationIndex];

    }

    public override void OnExitState()
    {
        routeMarks.Clear();
        destinationIndex = 0;

    }

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
