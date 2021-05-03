using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected  Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }
        
    public void JumpedOn()
    {
        anim.SetFloat("Ed",2);
    }

    private void Death()
    {
        StartCoroutine(Coroutine1());

    }

    IEnumerator Coroutine1()
    { 
        anim.SetFloat("Ed", 2);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

}
