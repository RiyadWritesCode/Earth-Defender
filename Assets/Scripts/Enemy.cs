using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem crash;
    [SerializeField] Transform parent;

    void OnParticleCollision(GameObject other)
    {
        var vfx = Instantiate(crash, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(gameObject);
    }
}
