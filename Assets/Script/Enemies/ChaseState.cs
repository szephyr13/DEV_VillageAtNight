using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChaseState : State<EnemyController>
{
    private Transform target;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float stoppingDistance;

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        target = FindObjectOfType<Player>().transform;
        anim.SetBool("attack", true);
    }

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

    public override void OnExitState()
    {
        anim.SetBool("attack", false);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            controller.ChangeState(controller.PatrolState);
        }
    }
}

