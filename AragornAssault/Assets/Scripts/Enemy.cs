using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }


}
