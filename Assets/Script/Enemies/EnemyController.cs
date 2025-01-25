using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PatrolState patrolState;
    private ChaseState chaseState;
    private AttackState attackState;

    private State<EnemyController> currentState;

    public PatrolState PatrolState { get => patrolState; }
    public ChaseState ChaseState { get => chaseState; }
    public AttackState AttackState { get => attackState; }

    // Start is called before the first frame update
    void Start()
    {
        patrolState = GetComponent<PatrolState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();

        ChangeState(patrolState);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState)
        {
            currentState.OnUpdateState();
        }
    }

    public void ChangeState(State<EnemyController> newState)
    {
        if (currentState)
        {
            currentState.OnExitState();
        }
        currentState = newState;
        currentState.OnEnterState(this);
    }
}
