using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private float lifes;
    [SerializeField] private int droppingRatio;
    [SerializeField] private GameObject droppingPrefab;
    [SerializeField] private Animator anim;
    private int droppingDice;

    private SpriteRenderer myImage;
    private int currentColor;
    private int colorChangesCounter;

    public float Lifes { get => lifes; set => lifes = value; }

    //on start, gets the sprite renderer of its game object
    private void Start()
    {
        myImage = this.gameObject.GetComponent<SpriteRenderer>();
        currentColor = 0;
        colorChangesCounter = 6;
    }

    //manages visual feedback, plays damage and sends to manage death if necessary 
    //also manages untouchableness for the player when showing visual feedback
    //last, updates life on hud
    public void TakeDamage(float damage)
    {
        if (this.gameObject.CompareTag("PlayerHitbox"))
        {
            if (gameObject.GetComponent<Player>().Untouchable)
            {
            }
            else
            {
                AudioManager.instance.PlaySFX("KunoichiDamage");
                lifes -= damage;
                StartCoroutine(ColorVisualFeedback());
            }
            gameObject.GetComponent<Player>().UpdateLifes(lifes);
        }
        else if (this.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlaySFX("EnemyDamage");
            lifes -= damage;
            StartCoroutine(ColorVisualFeedback());
        }

        if (lifes <= 0)
        {
            DeathManagement();
        }
    }

    //for the player, starts youlost screen
    //for enemies, manages the dropping system 
    //also, for bats and slimes, informs spawner
    // for the ghoul, manages the music, bools and sfx after its death
    //last, starts death animation
    private void DeathManagement()
    {
        if (this.gameObject.CompareTag("PlayerHitbox"))
        {
            FindAnyObjectByType<UIManager>().YouLost();
        }

        if (this.gameObject.CompareTag("Enemy"))
        {
            droppingDice = Random.Range(0, droppingRatio);
            if (droppingDice == 1)
            {
                Instantiate(droppingPrefab, this.gameObject.transform.position, Quaternion.identity);
            }
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            
            if (this.gameObject.name != "Wizard" && this.gameObject.name != "Ghoul")
            {
                GameObject killedEnemy = this.gameObject.transform.parent.gameObject;
                killedEnemy.transform.parent.gameObject.GetComponent<Spawner>().EnemyCounter--;
            } else
            {
            }
            
            if (this.gameObject.name == "Ghoul")
            {
                Destroy(this.gameObject.GetComponent<CircleCollider2D>());
                AudioManager.instance.PlaySFX("BossPresentation");
                AudioManager.instance.StopMusic();
                FindAnyObjectByType<Player>().GhoulIsDead = true;
            }
            anim.SetBool("death", true);

        }
    }

    //visual feedback for damage. sets differences for player and enemies (untouchable system)
    // changes from red to white the sprite renderer
    IEnumerator ColorVisualFeedback()
    {
        if (this.gameObject.TryGetComponent(out Player player))
        {
            player.Untouchable = true;
            for (int i = 0; i < colorChangesCounter; i++)
            {
                if (currentColor == 0)
                {
                    myImage.color = Color.red;
                    currentColor = 1;
                }
                else if (currentColor == 1)
                {
                    myImage.color = Color.white;
                    currentColor = 0;
                }
                yield return new WaitForSeconds(0.1f);
            }
            player.Untouchable = false;
        }
        else
        {
            for (int i = 0; i < colorChangesCounter; i++)
            {
                if (currentColor == 0)
                {
                    myImage.color = Color.red;
                    currentColor = 1;
                }
                else if (currentColor == 1)
                {
                    myImage.color = Color.white;
                    currentColor = 0;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
