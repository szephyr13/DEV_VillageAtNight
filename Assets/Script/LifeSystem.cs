using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private float lifes;

    public void TakeDamage(float damage)
    {
        lifes -= damage;
        if (lifes <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
