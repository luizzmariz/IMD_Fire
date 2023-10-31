using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpread : MonoBehaviour
{
    public float maxRadius = 12f;
    public float load_spread = 2f;

    public float max_rate = 20f; // !!! rate will only increase while spreading (aka radius is increasing)
    public float load_rate = 2f;


    private ParticleSystem.ShapeModule shape;
    private ParticleSystem.EmissionModule emission;

    private float radius = 1f;
    private float r_spread = 0f;
    private bool spreading = true;

    private float rate = 10f;
    private float r_rate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        shape = GetComponent<ParticleSystem>().shape;
        emission = GetComponent<ParticleSystem>().emission;
        radius = shape.radius;
        rate = emission.rateOverTime.constant;
    }

    // Update is called once per frame
    void Update()
    {
        if (spreading)
        {
            spreading = (radius < maxRadius);

            // Increasing radius
            if (r_spread >= load_spread)
            {
                SetRadius(radius + 1f);
                r_spread = 0;
            }
            r_spread += Time.deltaTime;


            // Increasing rate
            if (rate < max_rate)
            {
                if (r_rate >= load_rate)
                {
                    SetRate(rate + 1f);
                    r_rate = 0;
                }
                r_rate += Time.deltaTime;
            }
        }
    }

    void SetRadius(float r)
    {
        shape.radius = r;
        radius = r;
    }

    void SetRate(float r)
    {

        emission.rateOverTime = r;
        rate = r;
    }

    public float GetRadius() {return radius;}
    
    public float GetRate() {return rate;}
    

    /*
    void SpreadTo(GameObject victim)
    {
        if (victim.GetComponent<Fire>() == null)
        {
            victim.AddComponent<Fire>();
            Instantiate(FireObj, victim.transform);
        }

        //CopyComponent(GetComponent<ParticleSystem>(), victim);
    }
    */

    /*
    // by user: Shaffe   *adapted
    void CopyComponent(Component original, GameObject destination)
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        // Copied fields can be restricted with BindingFlags
        System.Reflection.FieldInfo[] fields = type.GetFields(); 
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
    }
    */
}
