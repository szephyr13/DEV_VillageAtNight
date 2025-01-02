using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForceValue;
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
        AttackLogic();
    }




    private void AttackLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }




    private void JumpingLogic()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForceValue, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
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
}
