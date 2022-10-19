using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem playerCrash;
    [SerializeField] GameObject leftCollider;
    [SerializeField] GameObject mainCollider;
    [SerializeField] GameObject rightCollider;


    void OnTriggerEnter(Collider other) {
        playerCrash.Play();
        Destroy(leftCollider);
        Destroy(mainCollider);
        Destroy(rightCollider);
        if (gameObject != null) 
        {
        GetComponent<PlayerController>().enabled = false;
        }
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("startCrashSequence", loadDelay);
    }

    void startCrashSequence()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
