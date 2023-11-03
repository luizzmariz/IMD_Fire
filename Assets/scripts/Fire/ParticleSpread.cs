using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleSpread : MonoBehaviour
{
    public float maxRadius = 12f;
    public float maxRate = 20f;

    public float radiusIncreaseSpd = 1f; // 1 unit per second
    public float rateIncreaseSpd = 1f;  //

    private ParticleSystem.ShapeModule shape;
    private ParticleSystem.EmissionModule emission;

    private float radius = 1f;
    private float r_radius = 0f;

    private float rate = 10f;
    private float r_rate = 0f;

    [HideInInspector] public bool finishedSpreading = false;

    // Start is called before the first frame update
    void Start()
    {
        shape = this.gameObject.GetComponent<ParticleSystem>().shape;
        emission = this.gameObject.GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
    void Update()
    {

        // function that increases a variable periodically
        void increaseFloat(Func<float, bool> func, float currentValue, ref float counter, float maxValue, float spd)
        {
            if (currentValue < maxValue)
            {
                // Increasing radius
                if (counter >= 1f)
                {
                    func(currentValue + 1f);
                    counter = 0;
                }
                counter += Time.deltaTime * spd;
            }
        }

        increaseFloat(SetRadius, radius, ref r_radius, maxRadius, radiusIncreaseSpd);
        increaseFloat(SetRate, rate, ref r_rate, maxRate, rateIncreaseSpd);

        finishedSpreading = radius >= maxRadius;
    }

    bool SetRadius(float r)
    {
        shape.radius = r;
        radius = r;

        return false; //workaround, please ignore
    }

    bool SetRate(float r)
    {
        emission.rateOverTime = r;
        rate = r;

        return false; //workaround, please ignore
    }

    public float GetRadius() {return radius;}
    
    public float GetRate() {return rate;}
}
