using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("MovementSystem")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForceValue;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private LayerMask whatIsJumpable;
    [SerializeField] private float maxNearFloor;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private float jumpDelay;
    private float inputH;
    private bool doubleJumpSkill;
    private bool canOpenDoor;

    [Header("Combat System")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private float attackPower;
    [SerializeField] private GameObject lifeUI;
    [SerializeField] private GameObject lifeUIFill;

    private Animator anim;

    public bool DoubleJumpSkill { get => doubleJumpSkill; set => doubleJumpSkill = value; }
    public bool CanOpenDoor { get => canOpenDoor; set => canOpenDoor = value; }
    public float AttackPower { get => attackPower; set => attackPower = value; }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        doubleJumpSkill = false;
        canOpenDoor = false;
        canDoubleJump = false;
        UpdateLifes(this.gameObject.GetComponent<LifeSystem>().Lifes);
    }



    // Update is called once per frame
    void Update()
    {
        MovementLogic();
        JumpingLogic();
        AttackAnimation();
    }

    public void UpdateLifes(float lifes)
    {
        lifeUI.GetComponent<Slider>().value = lifes;
        if (lifes > 50)
        {
            lifeUIFill.GetComponent<Image>().color = Color.green;
        }
        else if (lifes <= 50 && lifes > 25)
        {
            lifeUIFill.GetComponent<Image>().color = Color.yellow;
        }
        else if (lifes <= 25 && lifes > 0)
        {
            lifeUIFill.GetComponent<Image>().color = Color.red;
        } else if (lifes <= 0)
        {
            lifeUIFill.GetComponent<Image>().color = Color.black;
        }
    }


    private void AttackAnimation()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.X))
        { 
            anim.SetTrigger("Attack");
        }
    }

    //executed by animation event
    private void Attack()
    {
        AudioManager.instance.PlaySFX("KunoichiAttack");
        Collider2D[] touchedColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamageable);
        foreach (Collider2D enemy in touchedColliders)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                LifeSystem playerLifes = enemy.gameObject.GetComponent<LifeSystem>();
                playerLifes.TakeDamage(attackPower);
            }
        }
    }


    private void JumpingLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnTheFloor())
        {
            AudioManager.instance.PlaySFX("KunoichiJump");
            rb.AddForce(Vector2.up * jumpForceValue, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            if (doubleJumpSkill)
            {
                canDoubleJump = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            AudioManager.instance.PlaySFX("KunoichiJump");
            rb.AddForce(Vector2.up * jumpForceValue/3*2, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            canDoubleJump = false;
        }
    }

    //checks if player is on the floor
    private bool OnTheFloor()
    {
        bool floor = Physics2D.Raycast(feetPosition.position, Vector3.down, maxNearFloor, whatIsJumpable);
        if (floor == true)
        {
            canDoubleJump = false;
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BossEvent"))
        {
            AudioManager.instance.PlayBGM("Boss");
            Destroy(collision);
        }
        else if (collision.CompareTag("YouWon"))
        {

            FindAnyObjectByType<UIManager>().YouWon();
        }
        else if (collision.CompareTag("Item"))
        {
            collision.gameObject.GetComponent<IInteractuable>().Interact();
        }
    }
}
