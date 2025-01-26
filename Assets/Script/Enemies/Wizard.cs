using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private Transform target;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform spawnPoint;
    private Animator anim;

    //gets its animator component on start
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }


    //called by animator (instantiates fireball)
    private void fireBallLaunching()
    {
        Instantiate(fireBall, spawnPoint.position, transform.rotation);
    }

    //when player enters on detection layer, starts attack animation
    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.transform;
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            anim.SetBool("attack", true);
        }
    }

    //when player exits the trigger, stops attack animation
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            anim.SetBool("attack", false);
        }
    }

}
