using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;

    private void Start()
    {
        // Resource heavy for update... not so for start
        AddRigidBody();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        Debug.Log("Rb is" + rb);
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy(other);
        }
    }

    private void KillEnemy(GameObject other)
    {
        Debug.Log($"{other.gameObject.name} Shoot {name}");
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        hitPoints--;
        scoreBoard.IncreaseScore(scorePerHit);
    }
}
