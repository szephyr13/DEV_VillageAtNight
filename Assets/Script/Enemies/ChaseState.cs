using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseState : State<EnemyController>
{
    private Transform target;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float stoppingDistance;

    //when entering this state, gets reference of the player, starts the attack animation
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        target = FindObjectOfType<Player>().transform;
        anim.SetBool("attack", true);
    }

    // every update, the enemy tries to be in the same position as the player (bats on xy axis, slime only on x axis)
    // - if it gets too near, changes to attack mode
    public override void OnUpdateState()
    {
        if(this.gameObject.name == "Bat")
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target.position,
                chaseSpeed * Time.deltaTime);
        }
        else if (this.gameObject.name == "Slime")
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(target.position.x, transform.position.y, 0),
                chaseSpeed * Time.deltaTime);
        }
        

        if(Vector3.Distance(transform.position, target.position) <= stoppingDistance)
        {
            controller.ChangeState(controller.AttackState);
        }
    }

    //when exiting the state, attack animation sets false
    public override void OnExitState()
    {
        anim.SetBool("attack", false);
    }

    //if the enemy loses track of the player, changes to patrol state
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            controller.ChangeState(controller.PatrolState);
        }
    }
}

