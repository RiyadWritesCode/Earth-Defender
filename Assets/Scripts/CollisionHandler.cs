using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    // void OnCollisionEnter(Collision other) {
    //     Debug.Log($"{this.name} Collided with {other.gameObject.name}");
    // }

    void OnTriggerEnter(Collider other) {
        GetComponent<PlayerController>().enabled = false;
        Invoke("startCrashSequence", loadDelay);
    }

    void startCrashSequence()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
