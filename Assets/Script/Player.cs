using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("MovementSystem")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForceValue;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask whatIsJumpable;
    [SerializeField] private float maxNearFloor;
    private float inputH;

    [Header("Combat System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private float attackPower;

    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        MovementLogic();
        JumpingLogic();
        AttackAnimation();
    }




    private void AttackAnimation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    //executed by animation event
    private void Attack()
    {
        Collider2D[] touchedColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamageable);
        foreach (Collider2D enemy in touchedColliders)
        {
            LifeSystem playerLifes = enemy.gameObject.GetComponent<LifeSystem>();
            playerLifes.TakeDamage(attackPower);
        }
    }


    private void JumpingLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnTheFloor())
        {
            rb.AddForce(Vector2.up * jumpForceValue, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    //checks if player is on the floor
    private bool OnTheFloor()
    {
        bool floor = Physics2D.Raycast(feetPosition.position, Vector3.down, maxNearFloor, whatIsJumpable);
        return floor;
    }


    private void MovementLogic()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * movementSpeed, rb.velocity.y);

        if (inputH != 0)
        {
            anim.SetBool("Running", true);
            if(inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRadius);
    }
}
