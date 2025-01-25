using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackPauseTiming;
    [SerializeField] private float attackPower;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(AttackRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(attackPauseTiming);
        }
    }

    //called by animator
    private void fireBallLaunching()
    {
        Instantiate(fireBall, spawnPoint.position, transform.rotation);
    }
}
