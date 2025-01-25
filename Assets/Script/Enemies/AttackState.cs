using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackState : State<EnemyController>
{
    private Transform target;
    private Rigidbody2D rb;


    [SerializeField] private float attackDistance;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float timeBetweenAttacks;

    private float timer = 0;

    public float AttackDamage { get => attackDamage; set => attackDamage = value; }

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        timer = timeBetweenAttacks;
        rb = this.GetComponent<Rigidbody2D>();
        target = FindObjectOfType<Player>().transform;
    }

    public override void OnUpdateState()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenAttacks)
        {
            if (this.gameObject.name == "Bat")
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target.position,
                    attackSpeed * Time.deltaTime);
            }
            else if (this.gameObject.name == "Slime")
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    new Vector3(target.position.x, transform.position.y, 0),
                    attackSpeed * Time.deltaTime);
            }

        }
        if (Vector3.Distance(transform.position, target.position) > attackDistance)
        {
            controller.ChangeState(controller.ChaseState);
        }
        FocusOnDestination(); 
    }

    private void FocusOnDestination()
    {
        if (target.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public override void OnExitState()
    {
        timer = 0f;
        anim.SetBool("attack", false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (collision.CompareTag("PlayerHitbox"))
            {
                LifeSystem playerLifes = collision.gameObject.GetComponent<LifeSystem>();
                playerLifes.TakeDamage(attackDamage);
            }
        }
    }
}


