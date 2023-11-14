using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extingsh : MonoBehaviour
{
    private ParticleSystem extingshParticles;

    void Start()
    {
        extingshParticles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (extingshParticles.isEmitting)
        {
            EraseFire();
        }
    }

    void EraseFire()
    {
        GameObject[] lista = GameObject.FindGameObjectsWithTag("FirePoint");

        foreach (GameObject g in lista)
        {
            ParticleSpread pSpread = g.GetComponent<ParticleSpread>();
            float distanceToFire = Vector3.Distance(g.transform.position, transform.position);
            float fireRadius = pSpread.GetRadius();

            if (distanceToFire <= 3f + fireRadius * g.transform.localScale.x * 1.5f)
            {
                pSpread.SetRadius(fireRadius - Time.deltaTime * 2f);
            }
        }
    }
}
