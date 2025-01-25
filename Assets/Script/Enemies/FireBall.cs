using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float shootImpulse;
    [SerializeField] private float lifeSpan;
    [SerializeField] private float attackDamage;

    [SerializeField] private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * shootImpulse, ForceMode2D.Impulse);
        timer = 0f;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer > lifeSpan)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            LifeSystem playerLifes = collision.gameObject.GetComponent<LifeSystem>();
            playerLifes.TakeDamage(attackDamage);
            Destroy(this.gameObject);
        }
    }
}
