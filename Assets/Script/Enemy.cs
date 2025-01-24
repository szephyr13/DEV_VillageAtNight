using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float attackDamage;
    private Vector3 currentDestination;
    private int currentIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = movementPoints[currentIndex].position;
        StartCoroutine(PatrolMovement());
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator PatrolMovement()
    {
        while (true)
        {
            while (transform.position != currentDestination)
            {
                transform.position = Vector3.MoveTowards(
                transform.position,
                currentDestination,
                patrolSpeed * Time.deltaTime);
                yield return null;
            }
            defineDestination();

        }
    }

    private void defineDestination()
    {
        currentIndex++;
        if (currentIndex >= movementPoints.Length)
        {
            currentIndex = 0;
        }
        currentDestination = movementPoints[currentIndex].position;
        FocusOnDestination();
    }

    private void FocusOnDestination()
    {
        if (currentDestination.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }


    protected abstract void AttackSystem();

    protected virtual void PlayerChase()
    {

    }
    protected virtual void StopChasing()
    {

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            Debug.Log("Player detectado");
        }
        else if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            LifeSystem playerLifes = collision.gameObject.GetComponent<LifeSystem>();
            playerLifes.TakeDamage(attackDamage);
        }
    }
}
