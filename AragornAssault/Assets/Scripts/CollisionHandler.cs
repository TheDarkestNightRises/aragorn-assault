using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelay = 1f;
    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    
        Debug.Log($"{this.name} **Triggerd by** {other.gameObject.name}");
    }

    private void StartCrashSequence()
    {
        GetComponent<PlayerController>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
