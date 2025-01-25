using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    private void DestroyEnemy()
    {
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
