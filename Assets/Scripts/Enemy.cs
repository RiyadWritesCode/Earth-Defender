using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem crash;
    [SerializeField] ParticleSystem hit;
    [SerializeField] int hitpoints = 1;
    [SerializeField] int scorePerHit = 15;
    GameObject runtime;
    ScoreBoard sb;
    Rigidbody rb;

    void Start()
    {
        sb = FindObjectOfType<ScoreBoard>();
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        runtime = GameObject.FindWithTag("runtime");

    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitpoints < 0)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitpoints--;
        sb.IncreaseScore(scorePerHit);
        var hitVFX = Instantiate(hit, transform.position, Quaternion.identity);
        hitVFX.transform.parent = runtime.transform;
    }

    void KillEnemy()
    {
        var crashVFX = Instantiate(crash, transform.position, Quaternion.identity);
        crashVFX.transform.parent = runtime.transform;
        Destroy(gameObject);
    }   
}
