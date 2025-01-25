using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //by animation
    private void DestroyEnemy()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
