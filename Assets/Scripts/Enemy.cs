using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem crash;
    [SerializeField] ParticleSystem hit;
    [SerializeField] Transform parent;
    [SerializeField] int hitpoints = 1;
    ScoreBoard sb;

    void Start()
    {
        sb = FindObjectOfType<ScoreBoard>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        KillEnemy();
    }

    void ProcessHit()
    {
        sb.IncreaseScore(15);
    }

    void KillEnemy()
    {
        hitpoints--;

        var hitVFX = Instantiate(hit, transform.position, Quaternion.identity);
        hitVFX.transform.parent = parent.transform;

        if (hitpoints == 0)
        {
            var crashVFX = Instantiate(crash, transform.position, Quaternion.identity);
            crashVFX.transform.parent = parent.transform;
            Destroy(gameObject);
        }       
    }   
}
