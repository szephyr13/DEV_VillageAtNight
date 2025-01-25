using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    private Transform target;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform spawnPoint;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }


    //called by animator
    private void fireBallLaunching()
    {
        Instantiate(fireBall, spawnPoint.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.transform;
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            anim.SetBool("attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            anim.SetBool("attack", false);
        }
    }

}
