using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //destroys the enemy - triggered by animation
    // calls to parent because all enemies' image & moving points are both in a empty parent
    private void DestroyEnemy()
    {
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
