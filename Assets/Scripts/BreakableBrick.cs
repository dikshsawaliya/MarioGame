using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBrick : MonoBehaviour
{
    private ParticleSystem particle;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    
    private void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.GetComponent<PlayerController>() && other.contacts[0].normal.y > 0.5f)
        {
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
        {
        audioSource.Play();
        particle.Play();
        sr.enabled = false;

        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}
