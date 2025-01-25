using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> : MonoBehaviour
{
    protected T controller;
    protected Animator anim;

    public virtual void OnEnterState(T controller)
    {
        this.controller = controller;
        anim = this.GetComponent<Animator>();
    }

    public abstract void OnUpdateState();
    public abstract void OnExitState();
}
