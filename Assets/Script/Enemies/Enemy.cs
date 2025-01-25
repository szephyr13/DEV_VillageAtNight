using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    private void DestroyEnemy()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
