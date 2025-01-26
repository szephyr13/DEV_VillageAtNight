using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackDamage;
    [SerializeField] private float movementSpeed;

    private Vector3 movementDirection;
    private float timer;

    // inicializes variables
    private void Start()
    {
        target = null;
        movementDirection = new Vector3();
        timer = 0f;
    }

    //when the player is detected by the ghoul, 
    // - starts timer, 
    // - focus on the player (another method) 
    // - sets+makes move on x, clamping it because its movement is kinematic
    private void Update()
    {
        if (target)
        {
            timer += Time.deltaTime;
            FocusOnDestination();
            movementDirection = new Vector3(target.position.x, transform.position.y, 0);
            transform.position = Vector3.MoveTowards(
                    transform.position,
                    movementDirection,
                    movementSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(transform.position.x, 39f, 74.0f);
            transform.position = new Vector3(clampedX, transform.position.y, 0);
        }
    }

    //called by animator - changes bool on player to make it possible to open the door - then destroys itself.
    private void DestroyBoss()
    {
        if(target.TryGetComponent(out Player player))
        {
            player.GhoulIsDead = true;
        }

        Destroy(this.gameObject);
    }

    //looks at player taking only as reference the x axis. 
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

    //corroutine to make the boss presentation (groar and then music)
    IEnumerator BossMusic()
    {
        yield return new WaitForSeconds(0.2f);
        AudioManager.instance.PlaySFX("BossPresentation");
        yield return new WaitForSeconds(1.8f);
        AudioManager.instance.PlayBGM("Boss");

    }

    //when the player is detected, starts the boss music coroutine and sets the player as the target
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDetection"))
        {
            StartCoroutine(BossMusic());
            target = collision.transform;
        }
    }

    //when colliding, the player gets damage
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            LifeSystem playerLifes = collision.gameObject.GetComponent<LifeSystem>();
            playerLifes.TakeDamage(attackDamage);
        }        
    }




}
