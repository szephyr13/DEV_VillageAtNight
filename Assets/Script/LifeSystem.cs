using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] public float lifes;

    private SpriteRenderer myImage;
    private int currentColor;
    private int colorChangesCounter;

    private void Start()
    {
        myImage = this.gameObject.GetComponent<SpriteRenderer>();
        currentColor = 0;
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

    public void DeathManagement()
    {
        if (this.gameObject.CompareTag("PlayerHitbox"))
        {
            FindAnyObjectByType<UIManager>().YouLost();
        } else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ColorVisualFeedback()
    {
        for (int i = 0; i < 6; i++)
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
