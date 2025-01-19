using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] public float lifes;
    

    public void TakeDamage(float damage)
    {
        lifes -= damage;
        if (lifes <= 0)
        {
            Destroy(this.gameObject);
        }

        if (gameObject.CompareTag("PlayerHitbox"))
        {
            gameObject.GetComponent<Player>().UpdateLifes(lifes);
        }
    }
}
