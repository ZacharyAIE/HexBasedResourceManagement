using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Destructable : MonoBehaviour
{
    public ParticleSystem destroyParticles;

    private void Awake()
    {
        if (!GetComponent<CharacterStats>())
        {
            gameObject.AddComponent<CharacterStats>();
        }
    }
    public void Destroy()
    {

    }
}
