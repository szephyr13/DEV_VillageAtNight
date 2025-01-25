using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    private ObjectPool<Enemy> myPool;

    public ObjectPool<Enemy> MyPool { get => myPool; set => myPool = value; }

    public void DestroyEnemy()
    {
        MyPool.Release(this);
    }

}
