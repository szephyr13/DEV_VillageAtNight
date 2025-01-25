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

    private void Start()
    {
        myImage = this.gameObject.GetComponent<SpriteRenderer>();
        currentColor = 0;
        colorChangesCounter = 6;
    }

    public void TakeDamage(float damage)
    {
        if (this.gameObject.CompareTag("PlayerHitbox"))
        {
            AudioManager.instance.PlaySFX("KunoichiDamage");
        } else if (this.gameObject.CompareTag("Enemy"))
        {
            AudioManager.instance.PlaySFX("EnemyDamage");
        }

        StartCoroutine(ColorVisualFeedback());

        lifes -= damage;
        if (lifes <= 0)
        {
            DeathManagement();
        }

        if (gameObject.CompareTag("PlayerHitbox"))
        {
            gameObject.GetComponent<Player>().UpdateLifes(lifes);
        }
    }

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
            GameObject killedEnemy = this.gameObject.transform.parent.gameObject;
            killedEnemy.transform.parent.gameObject.GetComponent<Spawner>().EnemyCounter--;
            anim.SetBool("death", true);
            killedEnemy.GetComponent<Enemy>().DestroyEnemy();
        }
    }

    IEnumerator ColorVisualFeedback()
    {
        for (int i = 0; i < colorChangesCounter; i++)
        {
            if (currentColor == 0)
            {
                myImage.color = Color.red;
                currentColor = 1;
            } else if (currentColor == 1)
            {
                myImage.color = Color.white;
                currentColor = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
