using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
        Debug.Log($"{this.name} **Triggerd by** {other.gameObject.name}");
    }

    private void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<PlayerController>().enabled = false;
        foreach (MeshRenderer meshInChild in GetComponentsInChildren<MeshRenderer>())
            meshInChild.enabled = false;

        foreach (Collider colliderInChild in GetComponentsInChildren<Collider>())
            colliderInChild.enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
